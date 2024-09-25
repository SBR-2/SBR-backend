using System;
using System.Collections.Generic;

namespace SBRSystem_Data.Models;

public partial class Entidad
{
    public int EntidadId { get; set; }

    public string? Nombre { get; set; }

    public string? Direccion { get; set; }

    public string? Telefono { get; set; }

    public string? Correo { get; set; }

    public string? Cedula { get; set; }

    public string? Rnc { get; set; }

    public virtual ICollection<ProductoEntidad> ProductoEntidads { get; set; } = new List<ProductoEntidad>();
}
