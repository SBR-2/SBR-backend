using Microsoft.EntityFrameworkCore;
using SBRSystem_API.GraphQl.input;
using SBRSystem_Data.Context;
using SBRSystem_Data.Models;

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
   
   
   
}