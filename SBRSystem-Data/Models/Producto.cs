using System;
using System.Collections.Generic;

namespace SBRSystem_Data.Models;

public partial class Producto
{
    public int ProductoId { get; set; }

    public string? Nombre { get; set; }

    public string? Marca { get; set; }

    public int? RiesgoSubcategoriaId { get; set; }

    public int? UsuarioId { get; set; }

    public string? Origen { get; set; }

    public string? Estado { get; set; }

    public string? Presentaciones { get; set; }

    public int? EstadoFisicoId { get; set; }

    public string? EnvasePrimario { get; set; }

    public string? MaterialEmpaque { get; set; }

    public bool? Nacional { get; set; }

    public bool? UnIngrediente { get; set; }

    public virtual EstadoFisico? EstadoFisico { get; set; }

    public virtual ICollection<ProductoEntidad> ProductoEntidads { get; set; } = new List<ProductoEntidad>();

    public virtual ICollection<Registro> Registros { get; set; } = new List<Registro>();

    public virtual RiesgoSubcategorium? RiesgoSubcategoria { get; set; }

    public virtual ICollection<Solicitud> Solicituds { get; set; } = new List<Solicitud>();

    public virtual Usuario? Usuario { get; set; }
}
