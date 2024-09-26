using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema; 

namespace SBRSystem_Data.Models;

public partial class Rol
{
    public string RolId { get; set; } = null!;

    [Column("rol")]
    public string? Rol1 { get; set; }

    public DateTime? FechaCreacion { get; set; }

    public virtual ICollection<Usuario> Usuarios { get; set; } = new List<Usuario>();
}
