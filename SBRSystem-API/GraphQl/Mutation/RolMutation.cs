using System;
using HotChocolate;
using SBRSystem_Data;
using SBRSystem_Data.DTO;
using SBRSystem_Data.Models;
using Microsoft.EntityFrameworkCore;
using SBRSystem_Data.Context;
using HotChocolate.Types;

namespace SBRSystem_API.GraphQl;

    [MutationType]
    public class RolMUtation
    {
        public async Task<Rol> AddRol(AddRolInput input, MySBRDbContext context)
        {
            // Verifica si ya existe un rol con el mismo ID o nombre
            var existingRol = await context.Rols.FirstOrDefaultAsync(r => r.RolId == input.RolId || r.Rol1 == input.Rol1);
            if (existingRol != null)
            {
                throw new GraphQLException("This role already exists.");
            }

            // Crea el nuevo rol
            var rol = new Rol
            {
                RolId = input.RolId,
                Rol1 = input.Rol1,  // Usamos input.Rol1 aquí
                FechaCreacion = DateTime.Now
            };

            context.Rols.Add(rol);

            try
            {
                await context.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                var innerExceptionMessage = ex.InnerException?.Message;
                throw new Exception($"Error saving changes to database: {innerExceptionMessage}", ex);
            }

            return rol;
        }

         public async Task<bool> DeleteRol(DeleteRolInput input, [Service] MySBRDbContext context)
        {
            var rol = await context.Rols.FindAsync(input.RolId);
            if (rol == null)
            {
                throw new GraphQLException("Role not found.");
            }
    
            // Eliminar el rol
            context.Rols.Remove(rol);
    
            try
            {
                await context.SaveChangesAsync();
                return true;  // Si la eliminación fue exitosa, devolvemos true
            }
            catch (DbUpdateException ex)
            {
                var innerExceptionMessage = ex.InnerException?.Message;
                throw new Exception($"Error deleting the role: {innerExceptionMessage}", ex);
            }
        }

    }
