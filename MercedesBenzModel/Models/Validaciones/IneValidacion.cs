using System;

namespace MercedesBenzModel
{
    public class IneValidacion
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
        public Guid Guid { get; set; }
        public string Mrz { get; set; } = null;
    }
}
