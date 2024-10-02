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
            // Buscar al usuario por su nombre
            var user = context.Usuarios.FirstOrDefault(u => u.Nombre == input.Username);

            if (user == null)
            {
                return new LoginResponse { Token = null, Message = "Credenciales inválidas" }; // El token es nulo si falla el login
            }

            // Generar el hash de la contraseña ingresada usando el salt almacenado
            var passwordService = new PasswordService();
            var hash = passwordService.ComputeHash(input.Password, user.Salt);

            // Comparar el Hash
            if (user.Hash != hash)
            {
                return new LoginResponse { Token = null, Message = "Credenciales inválidas" }; // El token es nulo si falla el login
            }

            var role = user.RolId switch
            {
                "1" => "Admin",
                "2" => "User",
                "3" => "Inspector",
                "4" => "Evaluator",
                "5" => "Solicitante",
                _ => "Unknown"
            };

            // Generar el token JWT si las credenciales son válidas
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes("aVeryLongSecretKeyForJWTTokenThatIsAtLeast32Chars");
            var securityToken = new JwtSecurityToken(
                issuer: "SBRSystem",
                audience: "APIUsers",
                claims: [
                    new Claim(ClaimTypes.Role, "Admin")
                ],
                signingCredentials: new SigningCredentials(new SymmetricSecurityKey(key), "HS256"),
                expires: DateTime.Now.AddDays(10)
            );
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.NameIdentifier, user.UsuarioId.ToString()),
                    new Claim(ClaimTypes.Role, role)
                }),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), "HS256"),
                Issuer = "SBRSystem",
                Audience = "APIUsers"
            };

            var token = tokenHandler.WriteToken(securityToken);
            return new LoginResponse { Token = tokenHandler.WriteToken(securityToken), Message = "Login exitoso" }; // Retorna token y mensaje de éxito
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
