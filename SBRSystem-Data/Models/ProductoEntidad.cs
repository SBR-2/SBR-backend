using System;
using System.Collections.Generic;

namespace SBRSystem_Data.Models;

public partial class ProductoEntidad
{
    public int ProductoId { get; set; }

    public int EntidadId { get; set; }

    public int? RelacionId { get; set; }

    public virtual Entidad Entidad { get; set; } = null!;

    public virtual Producto Producto { get; set; } = null!;

    public virtual Relacion? Relacion { get; set; }
}
