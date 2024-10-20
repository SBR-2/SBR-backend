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

    public int EntidadId { get; set; }

    public virtual ICollection<ComentarioDocumento> ComentarioDocumentos { get; set; } = new List<ComentarioDocumento>();

    public virtual Entidad Entidad { get; set; } = null!;

    public virtual ICollection<Ficha> FichaAprobadors { get; set; } = new List<Ficha>();

    public virtual ICollection<Ficha> FichaEvaluadors { get; set; } = new List<Ficha>();

    public virtual ICollection<Ficha> FichaInspectors { get; set; } = new List<Ficha>();

    public virtual ICollection<Ficha> FichaRevisors { get; set; } = new List<Ficha>();

    public virtual ICollection<Producto> Productos { get; set; } = new List<Producto>();

    public virtual Rol? Rol { get; set; }

    public virtual ICollection<Solicitud> Solicituds { get; set; } = new List<Solicitud>();
}
