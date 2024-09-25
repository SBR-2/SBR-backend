using System;
using System.Collections.Generic;

namespace SBRSystem_Data.Models;

public partial class Ficha
{
    public int FichaId { get; set; }

    public int? SolicitudId { get; set; }

    public int? EstablecimientoId { get; set; }

    public DateTime? Fecha { get; set; }

    public bool? Estado { get; set; }

    public virtual Establecimiento? Establecimiento { get; set; }

    public virtual ICollection<Respuestum> Respuesta { get; set; } = new List<Respuestum>();

    public virtual Solicitud? Solicitud { get; set; }
}
