using System;
using System.Collections.Generic;

namespace SBRSystem_Data.Models;

public partial class Documento
{
    public int DocumentoId { get; set; }

    public int? TipoDocumentoId { get; set; }

    public int? SolicitudId { get; set; }

    public string? Ruta { get; set; }

    public string? Estado { get; set; }

    public virtual Solicitud? Solicitud { get; set; }

    public virtual TipoDocumento? TipoDocumento { get; set; }
}
