using System.Threading.Tasks;
using HotChocolate;
using HotChocolate.Authorization;
using HotChocolate.Types;
using Microsoft.AspNetCore.Http;
using SBRSystem_Data.Context;

namespace SBRSystem_API.GraphQl
{
    [MutationType]
    public class LogoutMutation
    {
        [Authorize] 
        public async Task<LogoutResponse> Logout([Service] MySBRDbContext context, [Service] IHttpContextAccessor httpContextAccessor)
        {
            try
            {
 
                var authHeader = httpContextAccessor.HttpContext.Request.Headers["Authorization"].ToString();

                if (string.IsNullOrEmpty(authHeader) || !authHeader.StartsWith("Bearer "))
                {
                    throw new GraphQLException("Token no proporcionado.");
                }


                var token = authHeader.Substring("Bearer ".Length).Trim();

                return new LogoutResponse { Success = true, Message = "Logout exitoso" };
            }
            catch (Exception ex)
            {
                return new LogoutResponse { Success = false, Message = "Error durante el proceso de logout: " + ex.Message };
            }
        }
    }

    // Definimos la clase de respuesta de logout
    public class LogoutResponse
    {
        public bool Success { get; set; }
        public string Message { get; set; }
    }
}
