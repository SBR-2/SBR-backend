
namespace SBRSystem_API.GraphQl;

public class AddUsuarioInput
{
    public string Nombre { get; set; }
    public string Password { get; set;}
    public string RolId { get; set; }
    public int EntidadId { get; set; }
    // public string Estado { get; set; }
    //public DateTime FechaCreacion { get; set; }

    public string Correo { get; set; }
}

public class UpdateUsuarioInput
{
    public string Nombre { get; set; }
    // public string Hash { get; set; }
    // public string Salt { get; set; }

    public string Password { get; set; }
    public string RolId { get; set; }
    public string Estado { get; set; }
    //public DateTime FechaCreacion { get; set; }
    public string Correo { get; set; }
}

public class DeleteUsuarioInput
{
    [GraphQLNonNullType]  
    public int UsuarioId { get; set; }
}


