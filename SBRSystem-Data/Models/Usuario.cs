using System;
using System.Collections.Generic;

namespace SBRSystem_Data.Models;

public partial class Usuario
{
    public int UsuarioId { get; set; }

    public string? Nombre { get; set; }

    public string? Hash { get; set; }

    public string? RolId { get; set; }

    public string? Estado { get; set; }

    public DateTime? FechaCreacion { get; set; }

    public string? Salt { get; set; }

    public string Correo { get; set; } = null!;

    public virtual ICollection<Producto> Productos { get; set; } = new List<Producto>();

    public virtual Rol? Rol { get; set; }
}
