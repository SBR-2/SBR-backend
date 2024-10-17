namespace SBRSystem_API.GraphQl;

public class AddEntidadInput
{
    public string? Nombre { get; set; }
    public string? Direccion { get; set; }
    public string? Telefono { get; set; }
    public string? Correo { get; set; }
    public string? Cedula { get; set; }
    public string? RNC { get; set; }
}

public class DeleteEntidadInput
{
    [GraphQLNonNullType] public int EntidadId { get; set; }
}
