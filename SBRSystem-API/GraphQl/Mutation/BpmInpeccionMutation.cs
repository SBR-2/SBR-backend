using Microsoft.EntityFrameworkCore;
using SBRSystem_API.GraphQl.input;
using SBRSystem_Data.Context;
using SBRSystem_Data.Models;
using Serilog;
using GraphQLException = HotChocolate.GraphQLException;

namespace SBRSystem_API.GraphQl.Mutation;

[MutationType]
public class BpmInpeccionMutation
{
    public async Task<Grupo> AddGrupo(AddGrupoInput grupoInput, [Service] MySBRDbContext context)
    {
        if (await context.Grupos.AnyAsync(r => r.Nombre == grupoInput.Nombre))
        {
            throw new GraphQLException("El grupo ya existe.");
        }

        Grupo newGrupo = new Grupo
        {
            Nombre = grupoInput.Nombre,
            Descripcion = grupoInput.Descripcion,
            Estado = grupoInput.Estado,
        };

        try
        {
            context.Grupos.Add(newGrupo);
            context.SaveChanges();
            Log.Information($"Categoria: -{newGrupo.Nombre}- agregado correctamente");
        }
        catch (Exception e)
        {
            throw new GraphQLException("Ocurrió un error al agregar el grupo.");
        }

        return context.Grupos.First(x => x.Nombre == grupoInput.Nombre);
    }

    public async Task<BpmCategorium> AddbpmCategoria(AddBpmCategoria bpmCategoriaInput,
        [Service] MySBRDbContext context)
    {
        if (await context.BpmCategoria.AnyAsync(r => r.Nombre == bpmCategoriaInput.Nombre))
        {
            throw new GraphQLException("La categoria ya existe.");
        }

        BpmCategorium newBpmcategoria = new BpmCategorium()
        {
            Nombre = bpmCategoriaInput.Nombre,
            Descripcion = bpmCategoriaInput.Descripcion,
            Estado = bpmCategoriaInput.Estado,
            GrupoId = bpmCategoriaInput.GrupoId
        };

        try
        {
            context.BpmCategoria.Add(newBpmcategoria);
            context.SaveChanges();
            Log.Information($"Categoria: -{newBpmcategoria.Nombre}- agregado correctamente");
        }
        catch (Exception e)
        {
            throw new GraphQLException("Ocurrió un error al agregar la categoria.");
        }

        return context.BpmCategoria.First(x => x.Nombre == bpmCategoriaInput.Nombre);
    }

    public async Task<BpmSubcategorium> AddbpmSubCategoria(AddSubBpmCategoria bpmSubCategoriaInput,
        [Service] MySBRDbContext context)
    {
        if (await context.BpmSubcategoria.AnyAsync(r => r.Nombre == bpmSubCategoriaInput.Nombre))
        {
            throw new GraphQLException("La subcategoria ya existe.");
        }

        BpmSubcategorium newBpmSubcategoria = new BpmSubcategorium()
        {
            Nombre = bpmSubCategoriaInput.Nombre,
            Descripcion = bpmSubCategoriaInput.Descripcion,
            Estado = bpmSubCategoriaInput.Estado,
            BpmCategoriaId = bpmSubCategoriaInput.BpmCategoriaId
        };

        try
        {
            context.BpmSubcategoria.Add(newBpmSubcategoria);
            context.SaveChanges();
            Log.Information($"Categoria: -{newBpmSubcategoria.Nombre}- agregado correctamente");
        }
        catch (Exception e)
        {
            throw new GraphQLException("Ocurrió un error al agregar la Subcategoria.");
        }

        return context.BpmSubcategoria.First(x => x.Nombre == bpmSubCategoriaInput.Nombre);
    }

    public async Task<Preguntum> AddPregunta(AddPregunta preguntaInput, [Service] MySBRDbContext context)
    {
        if (await context.Pregunta.AnyAsync(r => r.Nombre == preguntaInput.Nombre))
        {
            throw new GraphQLException("La pregunta ya existe.");
        }

        Preguntum newPregunta = new Preguntum()
        {
            Nombre = preguntaInput.Nombre,
            Descripcion = preguntaInput.Descripcion,
            Estado = preguntaInput.Estado,
            BpmSubcategoriaId = preguntaInput.BpmSubcategoriaId
        };

        try
        {
            context.Pregunta.Add(newPregunta);
            context.SaveChanges();
            Log.Information($"Categoria: -{newPregunta.Nombre}- agregado correctamente");
        }
        catch (Exception e)
        {
            throw new GraphQLException("Ocurrió un error al agregar la pregutna.");
        }

        return context.Pregunta.First(x => x.Nombre == preguntaInput.Nombre);
    }

    public async Task<Respuestum> AddRespuesta(AddRespuestaInput input, [Service] MySBRDbContext context)
    {
        Respuestum newRespuesta = new Respuestum
        {
            PreguntaId = input.PreguntaId,
            FichaId = input.FichaId,
            ValorId = input.ValorId,
            Descripcion = input.Descripcion,
            Estado = input.Estado,
        };

        try
        {
            context.Respuesta.Add(newRespuesta);
            context.SaveChanges();
        }
        catch (Exception e)
        {
            throw new GraphQLException("Ocurrió un error al agregar la pregutna.");
        }

        return context.Respuesta
            .First(x => x.PreguntaId == input.PreguntaId && x.FichaId == input.FichaId);
    }

    public async Task<Ficha> AddFicha(AddFichaInput input, [Service] MySBRDbContext context)
    {
        Ficha newFicha = new Ficha
        {
            SolicitudId = input.SolicitudId,
            EstablecimientoId = input.EstablecimientoId,
            Estado = input.Estado,
            FechaElaboracion = DateTime.Now,
            Latitud = "-19.3434",
            Longitud = "12.324",
            NombreDps = input.NombreDps,
            NombreDigemaps = input.NombreDigemaps,

            FechaRevision = null,
            FechaAprobacion = null,
            Calificacion = null,
            MatizRiesgo = null,

            InspectorId = null,
            RevisorId = null,
            AprobadorId = null,
            EvaluadorId = null,
        };

        try
        {
            context.Fichas.Add(newFicha);
            context.SaveChanges();
        }
        catch (Exception e)
        {
            throw new GraphQLException("Ocurrio un error al agregar una ficha");
        }

        return context.Fichas.First(x =>
            x.SolicitudId == input.SolicitudId && x.EstablecimientoId == input.EstablecimientoId);
    }


    public async Task<Ficha> UpdateFicha(UpdateFichaInput input, [Service] MySBRDbContext context)
    {
        Ficha? objFicha = context.Fichas.FirstOrDefault(x => x.FichaId == input.FichaId);
        if (objFicha == null)
        {
            Log.Error($"Error update en ficha-{input.FichaId}");
            throw new Exception($"Error update en ficha-{input.FichaId}");
        }

        objFicha.FechaRevision = input.FechaRevision;
        objFicha.FechaAprobacion = input.FechaAprobacion;
        objFicha.Calificacion = input.Calificacion;
        objFicha.InspectorId = input.InspectorId;
        objFicha.RevisorId = input.RevisorId;
        objFicha.AprobadorId = input.AprobadorId;
        objFicha.MatizRiesgo = input.MatizRiesgo;
        objFicha.EvaluadorId = input.EvaluadorId;

        context.SaveChanges();
        Log.Information("Ficha-{P1} actualizada!", objFicha.FichaId);

        return objFicha;
    }


    public async Task<Valor> AddValor(AddValorInput input, [Service] MySBRDbContext context)
    {
        Valor newValor = new Valor
        {
            NomenclaturaValor = input.NomenclaturaValor,
            Puntos = input.Puntos,
            Descripcion = input.Descripcion,
            Estado = input.Estado,
        };

        try
        {
            context.Valors.Add(newValor);
            context.SaveChanges();
        }
        catch (Exception e)
        {
            throw new GraphQLException("Ocurrio un error al agregar un valor");
        }

        return context.Valors.First(x =>
            x.NomenclaturaValor == input.NomenclaturaValor);
    }

    public async Task<Establecimiento> AddEstablecimiento(AddEstablecimientoInput input,
        [Service] MySBRDbContext context)
    {
        Establecimiento newEstablecimiento = new Establecimiento
        {
            Nombre = input.Nombre,
            Numero = input.Numero,
            Municipio = input.Municipio,
            Telefono = input.Telefono,
            InicioOperaciones = input.InicioOperaciones,
            VencimientoSanitario = input.VencimientoSanitario,
            Rnc = input.Rnc,
            NumProductosElaborados = input.NumProductosElaborados,
            ProduccionAnual = input.ProduccionAnual,
            Comercializacion = input.Comercializacion,
            MercadoObjetivoId = input.MercadoObjetivo_id,
            NumEmpleados = input.NumEmpleados,
            UltimaInspeccion = input.UltimaInspeccion,
            CalUltimaInspeccion = input.CalUltimaInspeccion,
            NombreDps = input.NombreDps,
            NombreDigemaps = input.NombreDigemaps,
            NoSanitario = input.NoSanitario,
        };

        try
        {
            context.Establecimientos.Add(newEstablecimiento);
            context.SaveChanges();
            Log.Information($"Categoria: -{newEstablecimiento.Nombre}- agregado correctamente");
        }
        catch (Exception e)
        {
            throw new GraphQLException("Ocurrio un error al agregar un establecimiento");
        }

        return context.Establecimientos.First(x =>
            x.Rnc == input.Rnc);
    }
}