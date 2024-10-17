using HotChocolate.Authorization;
using SBRSystem_Data.Context;
using SBRSystem_Data.Models;

namespace SBRSystem_API.GraphQl.Query;

[QueryType]
public static class Query
{
    [UseProjection]
    [UseFiltering]
    [UseSorting]
    public static async Task<IQueryable<Riesgo>> GetRiesgos(MySBRDbContext context) => context.Riesgos;

    [UseProjection]
    [UseFiltering]
    [UseSorting]
    public static async Task<IQueryable<BpmCategorium>> GetBpmCategoria(MySBRDbContext context)
        => context.BpmCategoria;

    
    [UseProjection]
    [UseFiltering]
    [UseSorting]
    public static async Task<IQueryable<BpmSubcategorium>> GetBpmSubcategoria(MySBRDbContext context)
        => context.BpmSubcategoria;

    [UseProjection]
    [UseFiltering]
    [UseSorting]
    public static async Task<IQueryable<Documento>> GetDocumentos(MySBRDbContext context)
        => context.Documentos;

    [UseProjection]
    [UseFiltering]
    [UseSorting]
    public static async Task<IQueryable<Entidad>> GetEntidads(MySBRDbContext context)
        => context.Entidads;

    [UseProjection]
    [UseFiltering]
    [UseSorting]
    public static async Task<IQueryable<Establecimiento>> GetEstablecimientos(MySBRDbContext context)
        => context.Establecimientos;

    [UseProjection]
    [UseFiltering]
    [UseSorting]
    public static async Task<IQueryable<EstadoFisico>> GetEstadoFisicos(MySBRDbContext context)
        => context.EstadoFisicos;

    [UseProjection]
    [UseFiltering]
    [UseSorting]
    public static async Task<IQueryable<Factor>> GetFactors(MySBRDbContext context)
        => context.Factors;

    [UseProjection]
    [UseFiltering]
    [UseSorting]
    public static async Task<IQueryable<Ficha>> GetFichas(MySBRDbContext context)
        => context.Fichas;

    [UseProjection]
    [UseFiltering]
    [UseSorting]
    public static async Task<IQueryable<Grupo>> GetGrupos(MySBRDbContext context)
        => context.Grupos;

    [UseProjection]
    [UseFiltering]
    [UseSorting]
    public static async Task<IQueryable<Opcion>> GetOpcions(MySBRDbContext context)
        => context.Opcions;

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

    [UseProjection]
    [UseFiltering]
    [UseSorting]
    public static async Task<IQueryable<ProductoEntidad>> GetProductoEntidads(MySBRDbContext context)
        => context.ProductoEntidads;

    [UseProjection]
    [UseFiltering]
    [UseSorting]
    public static async Task<IQueryable<Registro>> GetRegistros(MySBRDbContext context)
        => context.Registros;

    [UseProjection]
    [UseFiltering]
    [UseSorting]
    public static async Task<IQueryable<Relacion>> GetRelacions(MySBRDbContext context)
        => context.Relacions;

    [UseProjection]
    [UseFiltering]
    [UseSorting]
    public static async Task<IQueryable<Respuestum>> GetRespuesta(MySBRDbContext context)
        => context.Respuesta;
    [UseProjection]
    [UseFiltering]
    [UseSorting]
    public static async Task<IQueryable<RiesgoCategorium>> GetRiesgoCategoria(MySBRDbContext context)
        => context.RiesgoCategoria;

    [UseProjection]
    [UseFiltering]
    [UseSorting]
    public static async Task<IQueryable<RiesgoSubcategorium>> GetRiesgoSubcategoria(MySBRDbContext context)
        => context.RiesgoSubcategoria;

    [UseProjection]
    [UseFiltering]
    [UseSorting]
    public static async Task<IQueryable<Rol>> GetRols(MySBRDbContext context)
        => context.Rols;

    [UseProjection]
    [UseFiltering]
    [UseSorting]
    public static async Task<IQueryable<Solicitud>> GetSolicituds(MySBRDbContext context)
        => context.Solicituds;

    [UseProjection]
    [UseFiltering]
    [UseSorting]
    public static async Task<IQueryable<TipoDocumento>> GetTipoDocumentos(MySBRDbContext context)
        => context.TipoDocumentos;

    [UseProjection]
    [UseFiltering]
    [UseSorting]
    public static async Task<IQueryable<Usuario>> GetUsuarios(MySBRDbContext context)
        => context.Usuarios;

    [UseProjection]
    [UseFiltering]
    [UseSorting]
    public static async Task<IQueryable<Valor>> GetValors(MySBRDbContext context)
        => context.Valors;
}