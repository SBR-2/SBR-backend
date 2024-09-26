using HotChocolate;
using SBRSystem_Data;
using SBRSystem_Data.DTO;
using SBRSystem_Data.Models;
using Microsoft.EntityFrameworkCore;
using SBRSystem_Data.Context;
using HotChocolate.Types;

namespace SBRSystem_API.GraphQl;

public class AddRiesgoInput
{
    [GraphQLNonNullType]
    public string Riesgo1 { get; set; }

    [GraphQLNonNullType]
    public bool Estado { get; set; }
}

public class UpdateRiesgoInput
{
    public string? Riesgo1 { get; set; }
    public bool? Estado { get; set; }
}