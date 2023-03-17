using System;

namespace MercedesBenzModel
{
    public class IneData
    {
        public int IneValidacionId { get; set; }
        public Guid Guid { get; set; }
        public DateTime? FechaRegistro { get; set; } = null;
        public bool Activo { get; set; } = true;
        public int IneEstatusId { get; set; }
        public string IneEstatusNombre { get; set; }
        public string IneModelo { get; set; }
        public string IneClaveElector { get; set; } = null;
        public string IneNumeroEmision { get; set; } = null;
        public string IneOcr { get; set; } = null;
        public string IneCic { get; set; } = null;
        public string IneIdentificadorCiudadano { get; set; } = null;
        public string IneAnioRegistro { get; set; } = null;
        public string IneAnioDeEmision { get; set; } = null;
        public string IneFechaConsulta { get; set; } = null;
        public string IneFechaActualizacionInformacion { get; set; } = null;
        public string IneFechaVigencia { get; set; } = null;
        public string IneRespuesta { get; set; } = null;

        public string ClaveElector { get; set; } = null;
        public string NumeroEmision { get; set; } = null;
        public string Cic { get; set; } = null;
        public string Ocr { get; set; } = null;
        public string IdentificadorCiudadano { get; set; } = null;
    }
}
