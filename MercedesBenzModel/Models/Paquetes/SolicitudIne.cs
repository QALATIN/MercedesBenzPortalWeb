using System.Collections.Generic;

namespace MercedesBenzModel
{
    public class SolicitudIne
    {
        public int SolicitanteId { get; set; }
        public string TipoDocumento { get; set; }
        public string Serie { get; set; }
        public string Modelo { get; set; }
        public string Cic { get; set; } = null;
        public string IdentificadorCiudadano { get; set; } = null;
        public string Ocr { get; set; } = null;
        public string ClaveElector { get; set; } = null;
        public string NumeroEmision { get; set; } = null;
        public string Mrz { get; set; } = null;
    }
}
