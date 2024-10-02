using HotChocolate;
using SBRSystem_Data;
using SBRSystem_Data.DTO;
using SBRSystem_Data.Models;
using Microsoft.EntityFrameworkCore;
using SBRSystem_Data.Context;
using HotChocolate.Types;
using HotChocolate.Authorization;
using SBRSystem_API.Services;

namespace SBRSystem_API.GraphQl;

    [MutationType]
    public class UsuarioMutation
    {
       
        public async Task<Usuario> AddUsuario(AddUsuarioInput input, [Service] MySBRDbContext context)
        {
            // Validar si ya existe un usuario con ese correo
            var existingUsuario = await context.Usuarios.FirstOrDefaultAsync(u => u.Correo == input.Correo);

            if (existingUsuario != null)
            {
                throw new GraphQLException("Este correo ya ha sido utilizado");
            }

            // Crear el hash y el salt usando el PasswordService
            var passwordService = new PasswordService();
            var (hash, salt) = passwordService.HashPassword(input.Password); // Genera hash y salt

            // Crear el objeto usuario y asignar los valores
            var usuario = new Usuario
            {
                Nombre = input.Nombre,
                Hash = hash,  
                Salt = salt,  
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
                throw new Exception($"Error al guardar cambios en la base de datos: {innerExceptionMessage}", ex);
            }

            return usuario;
        }

        [Authorize(Roles = new[] {"Admin"})]
        public async Task<Usuario> UpdateUsuario(int usuarioId, UpdateUsuarioInput input, [Service] MySBRDbContext context)
        {
            var usuario = await context.Usuarios.FindAsync(usuarioId);
            if (usuario == null)
            {
                throw new GraphQLException("Usuario no encontrado.");
            }

            var existingUsuario = await context.Usuarios
                .FirstOrDefaultAsync(u => u.Correo == input.Correo && u.UsuarioId != usuarioId);

            if (existingUsuario != null)
            {
                throw new GraphQLException("Este correo ya existe.");
            }

            

            // Si se proporciona una nueva contraseña, generar un nuevo hash y salt
            if (!string.IsNullOrEmpty(input.Password))
            {
                var passwordService = new PasswordService();
                var (newHash, newSalt) = passwordService.HashPassword(input.Password);

                usuario.Hash = newHash;
                usuario.Salt = newSalt;
            }

            usuario.Nombre = input.Nombre ?? usuario.Nombre;
            usuario.Correo = input.Correo ?? usuario.Correo;
            usuario.RolId = input.RolId ?? usuario.RolId;
            usuario.Estado = input.Estado ?? usuario.Estado;
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


        [Authorize(Roles = new[] {"Admin"})]
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

