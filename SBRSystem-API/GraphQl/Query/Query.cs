using HotChocolate.Authorization;
using SBRSystem_Data.Context;
using SBRSystem_Data.Models;

namespace SBRSystem_API.GraphQl.Query;

[QueryType]
public static class Query
{
    [UseOffsetPaging]
    [UseProjection]
    [UseFiltering]
    [UseSorting]
    public static async Task<IQueryable<Riesgo>> GetRiesgos(MySBRDbContext context) => context.Riesgos;

    [UseOffsetPaging]
    [UseProjection]
    [UseFiltering]
    [UseSorting]
    public static async Task<IQueryable<BpmCategorium>> GetBpmCategoria(MySBRDbContext context)
        => context.BpmCategoria;

    [UseOffsetPaging]
    [UseProjection]
    [UseFiltering]
    [UseSorting]
    public static async Task<IQueryable<BpmSubcategorium>> GetBpmSubcategoria(MySBRDbContext context)
        => context.BpmSubcategoria;

    [UsePaging]
    [UseProjection]
    [UseFiltering]
    [UseSorting]
    public static async Task<IQueryable<Documento>> GetDocumentos(MySBRDbContext context)
        => context.Documentos;

    [UseOffsetPaging]
    [UseProjection]
    [UseFiltering]
    [UseSorting]
    public static async Task<IQueryable<Entidad>> GetEntidads(MySBRDbContext context)
        => context.Entidads;

    [UseOffsetPaging]
    [UseProjection]
    [UseFiltering]
    [UseSorting]
    public static async Task<IQueryable<Establecimiento>> GetEstablecimientos(MySBRDbContext context)
        => context.Establecimientos;

    [UseOffsetPaging]
    [UseProjection]
    [UseFiltering]
    [UseSorting]
    public static async Task<IQueryable<EstadoFisico>> GetEstadoFisicos(MySBRDbContext context)
        => context.EstadoFisicos;

    [UseOffsetPaging]
    [UseProjection]
    [UseFiltering]
    [UseSorting]
    public static async Task<IQueryable<Factor>> GetFactors(MySBRDbContext context)
        => context.Factors;

    [UseOffsetPaging]
    [UseProjection]
    [UseFiltering]
    [UseSorting]
    public static async Task<IQueryable<Ficha>> GetFichas(MySBRDbContext context)
        => context.Fichas;

    [UseOffsetPaging]
    [UseProjection]
    [UseFiltering]
    [UseSorting]
    public static async Task<IQueryable<Grupo>> GetGrupos(MySBRDbContext context)
        => context.Grupos;

    [UseOffsetPaging]
    [UseProjection]
    [UseFiltering]
    [UseSorting]
    public static async Task<IQueryable<Opcion>> GetOpcions(MySBRDbContext context)
        => context.Opcions;

    [UseOffsetPaging]
    [UseProjection]
    [UseFiltering]
    [UseSorting]
    public static async Task<IQueryable<Preguntum>> GetPregunta(MySBRDbContext context)
        => context.Pregunta;

    [UseProjection]
    [UseFiltering]
    [UseSorting]
    public static async Task<IQueryable<Producto>> GetProductos(MySBRDbContext context)
        => context.Productos;

    [UseOffsetPaging]
    [UseProjection]
    [UseFiltering]
    [UseSorting]
    public static async Task<IQueryable<ProductoEntidad>> GetProductoEntidads(MySBRDbContext context)
        => context.ProductoEntidads;

    [UseOffsetPaging]
    [UseProjection]
    [UseFiltering]
    [UseSorting]
    public static async Task<IQueryable<Registro>> GetRegistros(MySBRDbContext context)
        => context.Registros;

    [UseOffsetPaging]
    [UseProjection]
    [UseFiltering]
    [UseSorting]
    public static async Task<IQueryable<Relacion>> GetRelacions(MySBRDbContext context)
        => context.Relacions;

    [UseOffsetPaging]
    [UseProjection]
    [UseFiltering]
    [UseSorting]
    public static async Task<IQueryable<Respuestum>> GetRespuesta(MySBRDbContext context)
        => context.Respuesta;
    [UseOffsetPaging]
    [UseProjection]
    [UseFiltering]
    [UseSorting]
    public static async Task<IQueryable<RiesgoCategorium>> GetRiesgoCategoria(MySBRDbContext context)
        => context.RiesgoCategoria;

    [UseOffsetPaging]
    [UseProjection]
    [UseFiltering]
    [UseSorting]
    public static async Task<IQueryable<RiesgoSubcategorium>> GetRiesgoSubcategoria(MySBRDbContext context)
        => context.RiesgoSubcategoria;

    [UseOffsetPaging]
    [UseProjection]
    [UseFiltering]
    [UseSorting]
    public static async Task<IQueryable<Rol>> GetRols(MySBRDbContext context)
        => context.Rols;

    [UseOffsetPaging]
    [UseProjection]
    [UseFiltering]
    [UseSorting]
    public static async Task<IQueryable<Solicitud>> GetSolicituds(MySBRDbContext context)
        => context.Solicituds;

    [UseOffsetPaging]
    [UseProjection]
    [UseFiltering]
    [UseSorting]
    public static async Task<IQueryable<TipoDocumento>> GetTipoDocumentos(MySBRDbContext context)
        => context.TipoDocumentos;

    [UseOffsetPaging]
    [UseProjection]
    [UseFiltering]
    [UseSorting]
    public static async Task<IQueryable<Usuario>> GetUsuarios(MySBRDbContext context)
        => context.Usuarios;

    [UseOffsetPaging]
    [UseProjection]
    [UseFiltering]
    [UseSorting]
    public static async Task<IQueryable<Valor>> GetValors(MySBRDbContext context)
        => context.Valors;
}