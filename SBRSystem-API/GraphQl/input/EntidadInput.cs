namespace SBRSystem_API.GraphQl;

public class AddEntidadInput
{
    [GraphQLNonNullType]
    public string? Nombre { get; set; }
    [GraphQLNonNullType]
    public string? Direccion { get; set; }
    [GraphQLNonNullType]
    public string? Telefono { get; set; }
    [GraphQLNonNullType]
    public string? Correo { get; set; }
    [GraphQLNonNullType]
    public string? Cedula { get; set; }
    public string? RNC { get; set; }
}


public class DeleteEntidadInput
{
    [GraphQLNonNullType]  
    public int EntidadId { get; set; }
}
