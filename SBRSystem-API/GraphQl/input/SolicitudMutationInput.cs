using SBRSystem_Data.Models;

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
}
