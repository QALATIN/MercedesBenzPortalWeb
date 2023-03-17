using System;

namespace MercedesBenzModel
{
    public class ReporteDetalleEnvioResponse
    {
        public string AgenciaNombre { get; set; }
        public string ClienteNombre { get; set; }
        public string Folio { get; set; }
        public DateTime FechaCaptura { get; set; }
        public DateTime FechaEnvio { get; set; }
        public string TipoDocumento { get; set; }
        public string TipoAlerta { get; set; }
        public string IneRespuesta { get; set; }
        public string RevisionAnalista { get; set; }

    }
}
