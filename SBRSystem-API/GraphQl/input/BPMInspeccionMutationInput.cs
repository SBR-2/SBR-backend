namespace SBRSystem_API.GraphQl.input;

public class AddGrupoInput
{

    [GraphQLNonNullType]
    public string Nombre { get; set; }

    [GraphQLNonNullType]
    public string Descripcion { get; set; }

    [GraphQLNonNullType]
    public bool Estado { get; set; }
}


public class AddBpmCategoria
{
    public int GrupoId { get; set; }

    public string Nombre { get; set; }

    public string Descripcion { get; set; }

    public bool Estado { get; set; }
    
}