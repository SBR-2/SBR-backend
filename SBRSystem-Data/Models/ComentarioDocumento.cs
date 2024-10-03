using System;
using System.Collections.Generic;

namespace SBRSystem_Data.Models;

public partial class ComentarioDocumento
{
    public int ComentarioDocumentoId { get; set; }

    public int DocumentoId { get; set; }

    public int UsuarioId { get; set; }

    public DateTime? FechaCreacion { get; set; }

    public bool Estado { get; set; }

    public virtual Documento Documento { get; set; } = null!;

    public virtual Usuario Usuario { get; set; } = null!;
}
