using HotChocolate;
using SBRSystem_Data;
using SBRSystem_Data.DTO;
using SBRSystem_Data.Models;
using Microsoft.EntityFrameworkCore;
using SBRSystem_Data.Context;
using HotChocolate.Types;

namespace SBRSystem_API.GraphQl;

public class AddRolInput
{
    public string RolId { get; set; }
    public string Rol1 { get; set; }
    //public DateTime? FechaCreacion { get; set; }
}

