using Microsoft.EntityFrameworkCore;
using SBRSystem_API.GraphQl.input;
using SBRSystem_Data.Context;
using SBRSystem_Data.Models;
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
   
   
   /*
    * Agregar respuesta
    * Agregar Ficha
    * Agregar establecimiento
    *
    */
   
   
   
}