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
   public async Task<Grupo> AddGrupo(AddGrupoInput grupoInput, [Service] MySBRDbContext context){
     
      
        if (await context.Grupos.AnyAsync(r => r.Nombre== grupoInput.Nombre))
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

        }
        catch (Exception e)
        {
            throw new GraphQLException("Ocurrió un error al agregar el grupo.");
        }

        return context.Grupos.First(x => x.Nombre == grupoInput.Nombre);
   }
   
   public async Task<BpmCategorium> AddbpmCategoria(AddBpmCategoria bpmCategoriaInput, [Service] MySBRDbContext context){
     
      
        if (await context.BpmCategoria.AnyAsync(r => r.Nombre== bpmCategoriaInput.Nombre))
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

        return context.BpmCategoria.First(x => x.Nombre ==bpmCategoriaInput.Nombre);
   }
   
   public async Task<BpmSubcategorium> AddbpmSubCategoria(AddSubBpmCategoria bpmSubCategoriaInput, [Service] MySBRDbContext context){
     
      
        if (await context.BpmSubcategoria.AnyAsync(r => r.Nombre== bpmSubCategoriaInput.Nombre))
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

        }
        catch (Exception e)
        {
            throw new GraphQLException("Ocurrió un error al agregar la Subcategoria.");
        }

        return context.BpmSubcategoria.First(x => x.Nombre ==bpmSubCategoriaInput.Nombre);
   }
   
   public async Task<Preguntum> AddPregunta(AddPregunta preguntaInput, [Service] MySBRDbContext context){
     
      
        if (await context.Pregunta.AnyAsync(r => r.Nombre== preguntaInput.Nombre))
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

        }
        catch (Exception e)
        {
            throw new GraphQLException("Ocurrió un error al agregar la pregutna.");
        }

        return context.Pregunta.First(x => x.Nombre ==preguntaInput.Nombre);
   }

   public async Task<Respuestum> AddRespuesta(AddRespuestaInput input , [Service] MySBRDbContext context)
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
           Fecha = input.Fecha,
           Estado = input.Estado,
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
           x.NomenclaturaValor== input.NomenclaturaValor);

   }
   
   public async Task<Establecimiento> AddEstablecimiento(AddEstablecimientoInput input, [Service] MySBRDbContext context)
   {
       Establecimiento newEstablecimiento = new Establecimiento
       {
           Nombre = input.Nombre,
           Numero = input.Numero,
           Municipio = input.Municipio,
           Provincia = input.Provincia,
           Telefono = input.Telefono,
           InicioOperaciones = input.InicioOperaciones,
           VencimientoSanitario = input.VencimientoSanitario,
           Rnc = input.Rnc,
           NumProductosElaborados = input.NumProductosElaborados,
           ProduccionAnual = input.ProduccionAnual,
           Comercializacion = input.Comercializacion,
           MercadoObjetivo = input.MercadoObjetivo,
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

       }
       catch (Exception e)
       {
           throw new GraphQLException("Ocurrio un error al agregar un establecimiento");
       }

       return context.Establecimientos.First(x =>
           x.Rnc== input.Rnc);

   }
   
   
   
   
   
}