using System.Security.Cryptography.X509Certificates;
using Microsoft.EntityFrameworkCore;
using SBRSystem_Data.Context;
using SBRSystem_Data.Models;

namespace SBRSystem_API.GraphQl;

public class EntidadMutation
{

    public async Task<Entidad> AddEntidad(AddEntidadInput input, [Service] MySBRDbContext context)
    {
        var existingEntidad = await context.Entidads.FirstOrDefaultAsync(e => e.Cedula == input.Cedula);

        if (existingEntidad != null)
        {
            throw new GraphQLException("This RNC Already Exists");
        }

        var entidades = new Entidad
        {
            Cedula = input.Cedula,
            Nombre = input.Nombre,
            Direccion = input.Direccion,
            Telefono = input.Telefono,
            Correo = input.Correo,
            Rnc = input.RNC
        };
        
        context.Entidads.Add(entidades);

        try
        {
            await context.SaveChangesAsync();
        }
        catch (DbUpdateException exception)
        {
            var innerException = exception.InnerException.Message;
            throw new Exception($"An exception occurred while adding Entidad: {innerException}");
        }
        return entidades;
    }

    public async Task<int> deleteEntidad(DeleteEntidadInput input, [Service] MySBRDbContext context)
    {
        try
        {
            var entidad = await context.Entidads.FindAsync(input.EntidadId);
            if (entidad == null)
            {
                GraphQLException graphQlException = new GraphQLException($"No se encontró la entidad con id {input.EntidadId} para eliminar");
                return 0;
            }
            
            context.Entidads.Remove(entidad);
            await context.SaveChangesAsync();
            // agregar log que se agregó correctamente
            return 1;

        }
        catch (Exception ex)
        {
            //agregar logging de fallo
            var innerException = ex.Message;
            throw new Exception($"An exception occurred while deleting Entidad: {innerException}");
        }
    }

}