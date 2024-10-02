using HotChocolate;
using SBRSystem_Data;
using SBRSystem_Data.DTO;
using SBRSystem_Data.Models;
using Microsoft.EntityFrameworkCore;
using SBRSystem_Data.Context;
using HotChocolate.Types;

namespace SBRSystem_API.GraphQl;

public class LoginInput
{
    public string Username {get; set;}
    public string Password { get; set; }

}

    public class LoginResponse
{
    public string? Token { get; set; } 
    public string Message { get; set; }
}