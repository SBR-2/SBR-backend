using System;
using System.Collections.Generic;

namespace SBRSystem_Data.Models;

public partial class EstadoFisico
{
    public int EstadoFisicoId { get; set; }

    public string? EstadoFisico1 { get; set; }

    public DateTime? FechaCreacion { get; set; }

    public virtual ICollection<Producto> Productos { get; set; } = new List<Producto>();
}
