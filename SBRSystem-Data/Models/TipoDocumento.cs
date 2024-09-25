using System;
using System.Collections.Generic;

namespace SBRSystem_Data.Models;

public partial class TipoDocumento
{
    public int TipoDocumentoId { get; set; }

    public string? TipoDocumento1 { get; set; }

    public bool? Estado { get; set; }

    public virtual ICollection<Documento> Documentos { get; set; } = new List<Documento>();
}
