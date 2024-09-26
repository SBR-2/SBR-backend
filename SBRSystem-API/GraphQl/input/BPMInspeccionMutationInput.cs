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


public class AddBpmCategoria: AddInputBPM
{
    public int GrupoId { get; set; }

    
}

public class AddInputBPM
{
    public string Nombre { get; set; }

    public string Descripcion { get; set; }

    public bool Estado { get; set; }
    
}

public class AddSubBpmCategoria: AddInputBPM
{
    public int BpmCategoriaId { get; set; }

}

public class AddPregunta: AddInputBPM
{
    
    public int BpmSubcategoriaId { get; set; }
}