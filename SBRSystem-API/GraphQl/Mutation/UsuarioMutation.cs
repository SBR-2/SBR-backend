using HotChocolate;
using SBRSystem_Data;
using SBRSystem_Data.DTO;
using SBRSystem_Data.Models;
using Microsoft.EntityFrameworkCore;
using SBRSystem_Data.Context;
using HotChocolate.Types;
using HotChocolate.Authorization;
using SBRSystem_API.Services;
using System.Security.Claims;

namespace SBRSystem_API.GraphQl;

    [MutationType]
    public class UsuarioMutation
    {
       
        public async Task<Usuario> AddUsuario(AddUsuarioInput input, [Service] MySBRDbContext context, ClaimsPrincipal user)
        {
            // Validar si ya existe un usuario con ese correo
            var existingUsuario = await context.Usuarios.FirstOrDefaultAsync(u => u.Correo == input.Correo);

            if (existingUsuario != null)
            {
                throw new GraphQLException("Este correo ya ha sido utilizado");
            }

            
            var passwordService = new PasswordService();
            var (hash, salt) = passwordService.HashPassword(input.Password);

            string rolId = input.RolId; 

            if (!user.Identity.IsAuthenticated) 
            {
                // Si el usuario no está autenticado, asignar rol de "Solicitante"
                rolId = "5"; 
            }
            else
            {
                
                var userRole = user.FindFirst("role")?.Value;

                if (userRole == "Admin")
                {
                    
                    rolId = input.RolId ?? "2";
                }
                else
                {
                    
                    if (!string.IsNullOrEmpty(input.RolId))
                    {
                        throw new GraphQLException("No tienes autorización para asignar rol.");
                    }
                    // Si no está intentando asignar un rol, le damos el rol por defecto "Solicitante"
                    rolId = "5";
                }
            }

            
            var usuario = new Usuario
            {
                Nombre = input.Nombre,
                Hash = hash,
                Salt = salt,
                RolId = rolId, 
                Estado = "true", // Estado siempre "true" hasta que el user haya sido eliminado
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


        
        [Authorize]
        public async Task<Usuario> UpdateUsuario(int usuarioId, UpdateUsuarioInput input, [Service] MySBRDbContext context, ClaimsPrincipal user)
        {
            // Buscar el usuario que se desea actualizar
            var usuario = await context.Usuarios.FindAsync(usuarioId);
            if (usuario == null)
            {
                throw new GraphQLException("Usuario no encontrado.");
            }

            // Si el usuario autenticado no es administrador, verificar que está actualizando su propia cuenta
            var userRole = user.FindFirst("role")?.Value;
            var authenticatedUserId = user.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (userRole != "Admin" && authenticatedUserId != usuario.UsuarioId.ToString())
            {
                throw new GraphQLException("No tienes autorización para actualizar este usuario.");
            }

            // Validar si el correo ya está siendo utilizado por otro usuario
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

            // Actualizar los datos del usuario
            usuario.Nombre = input.Nombre ?? usuario.Nombre;
            usuario.Correo = input.Correo ?? usuario.Correo;

            // Solo permitir que el administrador cambie el rol
            if (userRole == "Admin" && input.RolId != null)
            {
                usuario.RolId = input.RolId;
            }

            usuario.Estado = "true";
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



        [Authorize] // Requiere que el usuario esté autenticado
        public async Task<Usuario> DeleteUsuario(DeleteUsuarioInput input, [Service] MySBRDbContext context, ClaimsPrincipal user)
        {
            // Buscar el usuario que se desea eliminar
            var usuario = await context.Usuarios.FindAsync(input.UsuarioId);

            if (usuario == null)
            {
                throw new GraphQLException("Usuario no encontrado.");
            }

            if (usuario.Estado == "false")
            {
                throw new GraphQLException("El usuario ya está eliminado.");
            }

            // Obtener los detalles del usuario autenticado
            var authenticatedUserId = user.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var userRole = user.FindFirst(ClaimTypes.Role)?.Value;

            // Solo los administradores pueden eliminar cualquier usuario o los usuarios pueden eliminar su propia cuenta
            if (userRole != "Admin" && authenticatedUserId != usuario.UsuarioId.ToString())
            {
                throw new GraphQLException("No tienes autorización para eliminar este usuario.");
            }

            // Cambiar el estado del usuario a "false" (soft delete)
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

