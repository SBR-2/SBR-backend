using System;
using System.Collections.Generic;

namespace SBRSystem_Data.Models;

public partial class Relacion
{
    public int RelacionId { get; set; }

    public string? RelacionTipo { get; set; }

    public virtual ICollection<ProductoEntidad> ProductoEntidads { get; set; } = new List<ProductoEntidad>();
}
