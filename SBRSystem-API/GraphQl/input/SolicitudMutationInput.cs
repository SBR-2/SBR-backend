using SBRSystem_Data.Models;
using System.ComponentModel.DataAnnotations;

namespace SBRSystem_API.GraphQl.input
{
    public class AddSolicitudInput
    {
        public int? ProductoId { get; set; }

        public string? Observaciones { get; set; }

        public string? Estado { get; set; }

        public bool? TitularRepresentacion { get; set; }

        public bool? TitularFabricante { get; set; }

        public bool? AcondicionadorDistinto { get; set; }

        public bool? EsExportado { get; set; }
    }
    public class UpdateSolicitudInput
    {
        public int SolicitudId { get; set; }

        public int? ProductoId { get; set; }

        public string? Observaciones { get; set; }

        public string? Estado { get; set; }

        public bool? TitularRepresentacion { get; set; }

        public bool? TitularFabricante { get; set; }

        public bool? AcondicionadorDistinto { get; set; }

        public bool? EsExportado { get; set; }
    }

    public class RechazarSolicitudInput
    {
        public int SolicitudId { get; set; }

        public string? Observaciones { get; set; }
    }

    public class AsignarOpcionesASolicitudInput
    {
        public int SolicitudId { get; set; }

        [Length(6, 6, ErrorMessage = "Se deben de elegir 6 opciones para una solicitud valida")]
        public ICollection<int> OpcionIds { get; set; }
    }
}
