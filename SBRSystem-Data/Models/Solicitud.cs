using System;
using System.Collections.Generic;

namespace SBRSystem_Data.Models;

public partial class Solicitud
{
    public int SolicitudId { get; set; }

    public int? ProductoId { get; set; }

    public string? Observaciones { get; set; }

    public DateTime? FechaCreacion { get; set; }

    public string? Estado { get; set; }

    public bool? TitularRepresentacion { get; set; }

    public bool? TitularFabricante { get; set; }

    public bool? AcondicionadorDistinto { get; set; }

    public bool? EsExportado { get; set; }

    public DateTime? FechaRechazo { get; set; }

    public float? RiesgoTotal { get; set; }

    public virtual ICollection<Documento> Documentos { get; set; } = new List<Documento>();

    public virtual ICollection<Ficha> Fichas { get; set; } = new List<Ficha>();

    public virtual Producto? Producto { get; set; }

    public virtual ICollection<Opcion> Opcions { get; set; } = new List<Opcion>();
}
