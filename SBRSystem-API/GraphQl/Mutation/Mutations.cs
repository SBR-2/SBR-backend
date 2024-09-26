using Microsoft.EntityFrameworkCore;
using SBRSystem_API.GraphQl.input;
using SBRSystem_Data.Context;
using SBRSystem_Data.DTO;
using SBRSystem_Data.Models;

namespace SBRSystem_API.GraphQl.Mutation;

[MutationType]
public class Mutations
{

    public async Task<RiesgoDto> AddRiesgoAsync(AddRiesgoInput input, [Service] MySBRDbContext context)
    {

        if (await context.Riesgos.AnyAsync(r => r.Riesgo1 == input.Riesgo1))
        {
            throw new GraphQLException("El riesgo ya existe.");
        }


        var newRiesgo = new Riesgo
        {
            Riesgo1 = input.Riesgo1,
            Estado = input.Estado
        };


        context.Riesgos.Add(newRiesgo);

        try
        {

            await context.SaveChangesAsync();
        }
        catch (DbUpdateException)
        {

            throw new GraphQLException("Ocurrió un error al agregar el riesgo.");
        }


        return new RiesgoDto
        {
            RiesgoId = newRiesgo.RiesgoId,
            Riesgo1 = newRiesgo.Riesgo1,
            Estado = newRiesgo.Estado
        };
    }

    public async Task<RiesgoDto> UpdateRiesgoAsync(int id, UpdateRiesgoInput input, [Service] MySBRDbContext context)
    {
        var existingRiesgo = await context.Riesgos.FindAsync(id);

        if (existingRiesgo == null)
        {
            throw new GraphQLException("El riesgo no existe.");
        }

        //Actualizar los campos del reisgo
        existingRiesgo.Riesgo1 = input.Riesgo1 ?? existingRiesgo.Riesgo1; //Si no se proporciona Riesgo1, el valor se mantiene igual
        existingRiesgo.Estado = input.Estado ?? existingRiesgo.Estado;

        try
        {
            await context.SaveChangesAsync();
        }
        catch (DbUpdateException)
        {
            throw new GraphQLException("Ocurrió un error al actualizar el riesgo.");
        }

        return new RiesgoDto
        {
            RiesgoId = existingRiesgo.RiesgoId,
            Riesgo1 = existingRiesgo.Riesgo1,
            Estado = existingRiesgo.Estado
        };

    }
}