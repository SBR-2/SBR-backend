using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using SBRSystem_Data.Context;
using HotChocolate.Types;
using HotChocolate.Authorization;
using SBRSystem_API.Services;
using System.Security.Cryptography;

namespace SBRSystem_API.GraphQl
{
    [MutationType]
    public class LoginMutation
    {
        [AllowAnonymous]
        public LoginResponse Login(LoginInput input, [Service] MySBRDbContext context)
        {
            try
            {
                // Buscar el usuario por su correo electrónico
                var user = context.Usuarios.FirstOrDefault(u => u.Correo == input.Email);

                if (user == null)
                {
                    return new LoginResponse { Token = null, Message = "Credenciales inválidas" }; // El token es nulo si falla el login
                }

                var passwordService = new PasswordService();
                var hash = passwordService.ComputeHash(input.Password, user.Salt);


                if (user.Hash != hash)
                {
                    return new LoginResponse { Token = null, Message = "Credenciales inválidas" }; 
                }


                var validRoles = new Dictionary<string, string>
                {
                    { "1", "Admin" },
                    { "2", "User" },
                    { "3", "Inspector" },
                    { "4", "Evaluator" },
                    { "5", "Applicant" }
                };

                // Validar el RolId del usuario
                if (!validRoles.TryGetValue(user.RolId.ToString(), out var role))
                {
                    return new LoginResponse { Token = null, Message = "El rol del usuario no es válido." };
                }

                // Generar el token JWT si las credenciales y el rol son válidos
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.UTF8.GetBytes("aVeryLongSecretKeyForJWTTokenThatIsAtLeast32Chars");

                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new[]
                    {
                        new Claim(ClaimTypes.NameIdentifier, user.UsuarioId.ToString()),
                        new Claim(ClaimTypes.Role, role)
                    }),
                    Expires = DateTime.UtcNow.AddDays(7),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
                    Issuer = "SBRSystem",
                    Audience = "APIUsers"
                };

                var token = tokenHandler.CreateToken(tokenDescriptor);
                return new LoginResponse { Token = tokenHandler.WriteToken(token), Message = "Login exitoso" }; // Retorna token y mensaje de éxito
            }
            catch (Exception ex)
            {
                // Manejar cualquier error inesperado
                Console.WriteLine($"Error durante el login: {ex.Message}");
                return new LoginResponse { Token = null, Message = "Ocurrió un error en el proceso de login." }; // El token es nulo en caso de error
            }
        }
    }
}
