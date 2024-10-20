
namespace SBRSystem_API.GraphQl;

public class LoginInput
{
    public string Email {get; set;}
    public string Password { get; set; }

}

public class LoginResponse
{
    public string? Token { get; set; } 
    public string Message { get; set; }
    public string UserId {get; set; }
    public string RolId { get; set; }
}

public class GetUserIdResponse
{
    public string? Token { get; set; } 
    public string Message { get; set; }
    public string UserId {get; set; }
}