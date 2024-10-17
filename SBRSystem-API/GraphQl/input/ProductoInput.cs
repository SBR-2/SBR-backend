using SBRSystem_Data.Models;

namespace SBRSystem_API.GraphQl.input;

public class AddProductoInput
{
    [GraphQLNonNullType] public string? Nombre { get; set; }
    [GraphQLNonNullType] public string? Marca { get; set; }
    [GraphQLNonNullType] public int? RiesgoSubcategoriaId { get; set; }

    [GraphQLNonNullType] public int? UsuarioId { get; set; }
    [GraphQLNonNullType] public string? Origen { get; set; }
    [GraphQLNonNullType] public string? Estado { get; set; }
    [GraphQLNonNullType] public string? Presentaciones { get; set; }
    [GraphQLNonNullType] public int? EstadoFisicoId { get; set; }
    [GraphQLNonNullType] public string? EnvasePrimario { get; set; }
    [GraphQLNonNullType] public string? MaterialEmpaque { get; set; }
    [GraphQLNonNullType] public bool? Nacional { get; set; }
    [GraphQLNonNullType] public bool? UnIngrediente { get; set; }

    public ICollection<ProductoEntidadDTO> ProductoEntidades { get; set; }
}

public class ProductoEntidadDTO
{
    public int ProductoId { get; set; }

    public int EntidadId { get; set; }

    public int? RelacionId { get; set; }
}

public class DeleteProductoInput
{
    [GraphQLNonNullType] public int? ProductoId { get; set; }
    public string Estado { get; set; }
}