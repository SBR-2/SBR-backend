using System;
using System.Collections.Generic;

namespace SBRSystem_Data.Models;

public partial class RiesgoCategorium
{
    public int RiesgoCategoriaId { get; set; }

    public string? RiesgoCategoria { get; set; }

    public bool? Estado { get; set; }

    public virtual ICollection<RiesgoSubcategorium> RiesgoSubcategoria { get; set; } = new List<RiesgoSubcategorium>();
}
