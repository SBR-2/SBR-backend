using System;
using System.Collections.Generic;

namespace SBRSystem_Data.Models;

public partial class Ficha
{
    public int FichaId { get; set; }

    public int? SolicitudId { get; set; }

    public int? EstablecimientoId { get; set; }

    public DateTime? FechaElaboracion { get; set; }

    public bool? Estado { get; set; }

    public DateTime? FechaRevision { get; set; }

    public string? Latitud { get; set; }

    public string? Longitud { get; set; }

    public DateTime? FechaAprobacion { get; set; }

    public float? Calificacion { get; set; }

    public int? InspectorId { get; set; }

    public int? RevisorId { get; set; }

    public int? AprobadorId { get; set; }

    public string? NombreDps { get; set; }

    public string? NombreDigemaps { get; set; }

    public virtual Usuario? Aprobador { get; set; }

    public virtual ICollection<Comentario> Comentarios { get; set; } = new List<Comentario>();

    public virtual Establecimiento? Establecimiento { get; set; }

    public virtual Usuario? Inspector { get; set; }

    public virtual ICollection<Respuestum> Respuesta { get; set; } = new List<Respuestum>();

    public virtual Usuario? Revisor { get; set; }

    public virtual Solicitud? Solicitud { get; set; }
}
