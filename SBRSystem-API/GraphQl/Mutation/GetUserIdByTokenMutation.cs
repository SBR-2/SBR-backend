using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using HotChocolate;
using HotChocolate.Types;
using HotChocolate.Authorization;

namespace SBRSystem_API.GraphQl
{
    [MutationType]
    public class UserMutation
    {
        [AllowAnonymous]
        public GetUserIdResponse GetUserIdByToken(string token)
        {
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();

                // Decodificar el token sin validarlo
                var jwtToken = tokenHandler.ReadJwtToken(token);

                // Extraer el UserId del claim "nameid" o "sub"
                var userIdClaim = jwtToken.Claims.FirstOrDefault(c => c.Type == "nameid");



                if (userIdClaim == null)
                {
                    return new GetUserIdResponse
                    {
                        UserId = null,
                        Message = "El token no contiene el ID de usuario."
                    };
                }

                return new GetUserIdResponse
                {
                    UserId = userIdClaim.Value,
                    Message = "ID de usuario obtenido exitosamente."
                };
            }
            catch (Exception ex)
            {
                // Manejar excepciones
                Console.WriteLine($"Error al extraer el ID de usuario del token: {ex.Message}");
                return new GetUserIdResponse
                {
                    UserId = null,
                    Message = "El token es inválido."
                };
            }
        }
    }
}
