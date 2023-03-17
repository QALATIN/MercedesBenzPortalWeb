using System;

namespace MercedesBenzModel
{
    public class BusquedaResultado
    {
        public int SolicitanteId { get; set; }
        public string Folio { get; set; }
        public string NombreCompleto { get; set; }
        public string TipoCliente { get; set; }
        public string TipoDocumento { get; set; }
        public string CorreoElectronico { get; set; }
        public string ListaNegra { get; set; }
        public DateTime FechaEnvio { get; set; }
        public string EstadoSolicitud { get; set; }

    }
}
