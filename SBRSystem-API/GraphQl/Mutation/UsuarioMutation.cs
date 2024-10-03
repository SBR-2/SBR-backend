using HotChocolate;
using SBRSystem_Data;
using SBRSystem_Data.DTO;
using SBRSystem_Data.Models;
using Microsoft.EntityFrameworkCore;
using SBRSystem_Data.Context;
using HotChocolate.Types;

namespace SBRSystem_API.GraphQl;

    [MutationType]
    public class UsuarioMutation
    {
        public async Task<Usuario> AddUsuario(AddUsuarioInput input, [Service] MySBRDbContext context)
        {
            // Validar si ya existe un usuario con ese nombre
            var existingUsuario = await context.Usuarios.FirstOrDefaultAsync(u => u.Correo == input.Correo);

            if(existingUsuario != null)
            {
                throw new GraphQLException("This email has been used");
            }
            
            var usuario = new Usuario
            {
                Nombre = input.Nombre,
                Hash = input.Hash,
                Salt = input.Salt,
                RolId = input.RolId,
                Estado = input.Estado,
                Correo = input.Correo,
                FechaCreacion = DateTime.Now
            };

            context.Usuarios.Add(usuario);
            
            try
            {
                
                await context.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                
                var innerExceptionMessage = ex.InnerException?.Message;
                throw new Exception($"Error saving changes to database: {innerExceptionMessage}", ex);
            }


            return usuario;
        }

        public async Task<Usuario> UpdateUsuario(int usuarioId, UpdateUsuarioInput input, [Service] MySBRDbContext context)
        {
            var usuario = await context.Usuarios.FindAsync(usuarioId);
            if (usuario == null)
            {
                throw new GraphQLException("User not found.");
            }

            var existingUsuario = await context.Usuarios
                .FirstOrDefaultAsync(u => u.Correo == input.Correo && u.UsuarioId != usuarioId);

            if(existingUsuario != null)
            {
                throw new GraphQLException("This email already exist");
            }

            usuario.Nombre = input.Nombre;
            usuario.Hash = input.Hash;
            usuario.RolId = input.RolId;
            usuario.Estado = input.Estado;
            usuario.Salt = input.Salt;
            usuario.Correo = input.Correo;
            usuario.FechaCreacion = DateTime.Now;


            context.Usuarios.Update(usuario);

            try
            {
                await context.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                var innerExceptionMessage = ex.InnerException?.Message;
                throw new Exception($"Error al guardar cambios en la base de datos: {innerExceptionMessage}", ex);
            }

            return usuario;
        }


        public async Task<Usuario> DeleteUsuario(DeleteUsuarioInput input, [Service] MySBRDbContext context)
        {
            var usuario = await context.Usuarios.FindAsync(input.UsuarioId);

            if (usuario == null)
            {
                throw new GraphQLException("Usuario no encontrado.");
            }

            if (usuario.Estado == "false")
            {
                throw new GraphQLException("El usuario no existe");
            }

            
            usuario.Estado = "false";

            try
            {
                
                await context.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                var innerExceptionMessage = ex.InnerException?.Message;
                throw new Exception($"Error al guardar cambios en la base de datos: {innerExceptionMessage}", ex);
            }

            
            return usuario;
        }




    }

