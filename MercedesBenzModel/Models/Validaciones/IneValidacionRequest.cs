using System;

namespace MercedesBenzModel
{
    public class IneValidacionRequest 
    {
        public int IneValidacionId { get; set; }
        public Guid Guid { get; set; }
        public DateTime? FechaRegistro { get; set; } = null;
        public bool Activo { get; set; } = true;
        public int IneEstatusId { get; set; }
        public string Modelo { get; set; }
        public string IneClaveDeElector { get; set; } = null;
        public string IneNumeroDeEmision { get; set; } = null;
        public string IneOcr { get; set; } = null;
        public string IneCic { get; set; } = null;
        public string IneIdentificadorCiudadano { get; set; } = null;
        public string AnioDeRegistro { get; set; } = null;
        public string AnioDeEmision { get; set; } = null;
        public string FechaDeConsulta { get; set; } = null;
        public string FechaDeActualizacionDeInformacion { get; set; } = null;
        public string FechaDeVigencia { get; set; } = null;
        public string Respuesta { get; set; } = null;
    }
}
