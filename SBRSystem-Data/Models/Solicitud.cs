using System;
using System.Collections;
using System.Collections.Generic;

namespace SBRSystem_Data.Models;

public partial class Solicitud
{
    public int SolicitudId { get; set; }

    public int? ProductoId { get; set; }

    public string? Observaciones { get; set; }

    public DateTime? FechaCreacion { get; set; }

    public BitArray? TitularRepresentacion { get; set; }

    public BitArray? TitularFabricante { get; set; }

    public BitArray? AcondicionadorDistinto { get; set; }

    public BitArray? EsExportado { get; set; }

    public string? Estado { get; set; }

    public virtual ICollection<Documento> Documentos { get; set; } = new List<Documento>();

    public virtual ICollection<Ficha> Fichas { get; set; } = new List<Ficha>();

    public virtual Producto? Producto { get; set; }

    public virtual ICollection<Opcion> Opcions { get; set; } = new List<Opcion>();
}
