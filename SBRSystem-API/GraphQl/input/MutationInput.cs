namespace SBRSystem_API.GraphQl.input;

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