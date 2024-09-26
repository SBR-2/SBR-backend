using SBRSystem_Data.Context;
using SBRSystem_Data.DTO;
using SBRSystem_Data.Models;

namespace SBRSystem_API.GraphQl;

[QueryType]
public static class Query
{
    public static async Task<IQueryable<Riesgo>> GetRiesgos(MySBRDbContext context) => context.Riesgos;

    public static async Task<IQueryable<BpmCategorium>> GetBpmCategoria(MySBRDbContext context)
        => context.BpmCategoria;

    public static async Task<IQueryable<BpmSubcategorium>> GetBpmSubcategoria(MySBRDbContext context)
        => context.BpmSubcategoria;

    public static async Task<IQueryable<Documento>> GetDocumentos(MySBRDbContext context)
        => context.Documentos;

    public static async Task<IQueryable<Entidad>> GetEntidads(MySBRDbContext context)
        => context.Entidads;

    public static async Task<IQueryable<Establecimiento>> GetEstablecimientos(MySBRDbContext context)
        => context.Establecimientos;

    public static async Task<IQueryable<EstadoFisico>> GetEstadoFisicos(MySBRDbContext context)
        => context.EstadoFisicos;

    public static async Task<IQueryable<Factor>> GetFactors(MySBRDbContext context)
        => context.Factors;

    public static async Task<IQueryable<Ficha>> GetFichas(MySBRDbContext context)
        => context.Fichas;

    public static async Task<IQueryable<Grupo>> GetGrupos(MySBRDbContext context)
        => context.Grupos;

    public static async Task<IQueryable<Opcion>> GetOpcions(MySBRDbContext context)
        => context.Opcions;

    public static async Task<IQueryable<Preguntum>> GetPregunta(MySBRDbContext context)
        => context.Pregunta;

    public static async Task<IQueryable<Producto>> GetProductos(MySBRDbContext context)
        => context.Productos;

    public static async Task<IQueryable<ProductoEntidad>> GetProductoEntidads(MySBRDbContext context)
        => context.ProductoEntidads;

    public static async Task<IQueryable<Registro>> GetRegistros(MySBRDbContext context)
        => context.Registros;

    public static async Task<IQueryable<Relacion>> GetRelacions(MySBRDbContext context)
        => context.Relacions;

    public static async Task<IQueryable<Respuestum>> GetRespuesta(MySBRDbContext context)
        => context.Respuesta;

    public static async Task<IQueryable<RiesgoCategorium>> GetRiesgoCategoria(MySBRDbContext context)
        => context.RiesgoCategoria;

    public static async Task<IQueryable<RiesgoSubcategorium>> GetRiesgoSubcategoria(MySBRDbContext context)
        => context.RiesgoSubcategoria;

    public static async Task<IQueryable<Rol>> GetRols(MySBRDbContext context)
        => context.Rols;

    public static async Task<IQueryable<Solicitud>> GetSolicituds(MySBRDbContext context)
        => context.Solicituds;

    public static async Task<IQueryable<TipoDocumento>> GetTipoDocumentos(MySBRDbContext context)
        => context.TipoDocumentos;

    [UseOffsetPaging]
    [UseProjection]
    [UseFiltering]
    [UseSorting]
    public static async Task<IQueryable<Usuario>> GetUsuarios(MySBRDbContext context)
        => context.Usuarios;

    public static async Task<IQueryable<Valor>> GetValors(MySBRDbContext context)
        => context.Valors;
}