using HotChocolate;
using SBRSystem_Data;
using SBRSystem_Data.DTO;
using SBRSystem_Data.Models;
using Microsoft.EntityFrameworkCore;
using SBRSystem_Data.Context;
using HotChocolate.Types;

namespace SBRSystem_API.GraphQl;

public class AddUsuarioInput
{
    public string Nombre { get; set; }
    public string Hash { get; set; }
    public string RolId { get; set; }
    public string Estado { get; set; }
    //public DateTime FechaCreacion { get; set; }
    public string Salt { get; set; }

    public string Correo { get; set; }
}

public class UpdateUsuarioInput
{
    public string Nombre { get; set; }
    public string Hash { get; set; }
    public string RolId { get; set; }
    public string Estado { get; set; }
    //public DateTime FechaCreacion { get; set; }
    public string Salt { get; set; }
    public string Correo { get; set; }
}

public class DeleteUsuarioInput
{
    [GraphQLNonNullType]  
    public int UsuarioId { get; set; }
}


