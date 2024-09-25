using System;
using System.Collections.Generic;

namespace SBRSystem_Data.Models;

public partial class Registro
{
    public int RegistoId { get; set; }

    public int? ProductoId { get; set; }

    public DateTime? FechaEmision { get; set; }

    public DateTime? FechaExpiracion { get; set; }

    public string? RutaArchivo { get; set; }

    public virtual Producto? Producto { get; set; }
}
