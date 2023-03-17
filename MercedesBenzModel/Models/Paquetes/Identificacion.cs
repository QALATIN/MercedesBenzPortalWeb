using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MercedesBenzModel
{
    public class Identificacion
    {
        public int IdentificacionId { get; set; } = 0;
        public int SolicitanteId { get; set; } = 0;
        public string Serie { get; set; }
        public string NumeroEmision { get; set; }
        public string Cic { get; set; }
        public string Ocr { get; set; }
        public string ClaveElector { get; set; }
        public string IdentificadorCiudadano { get; set; }
        public string Vigencia { get; set; }
        public string AnioRegistro { get; set; }
        public string Emision { get; set; }
        public string Mrz { get; set; }
        public DateTime FechaEnvio { get; set; }
        public DateTime FechaRegistro { get; set; }
        public DateTime? FechaBaja { get; set; } = null;
        public bool Activo { get; set; } = true;
        public string TipoIne { get; set; }

    }
}
