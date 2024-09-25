using System;
using System.Collections.Generic;

namespace SBRSystem_Data.Models;

public partial class RiesgoSubcategorium
{
    public int RiesgoSubcategoriaId { get; set; }

    public string? RiesgoSubcategoria { get; set; }

    public int? RiesgoId { get; set; }

    public int? RiesgoCategoriaId { get; set; }

    public bool? Estado { get; set; }

    public virtual ICollection<Producto> Productos { get; set; } = new List<Producto>();

    public virtual Riesgo? Riesgo { get; set; }

    public virtual RiesgoCategorium? RiesgoCategoria { get; set; }
}
