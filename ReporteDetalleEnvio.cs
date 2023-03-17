using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MercedesBenzModel
{
    public class ReporteDetalleEnvio
    {
        public string AgenciaNombre { get; set; }
        public string ClienteNombre { get; set; }
        public string Contrato { get; set; }
        public DateTime FechaCaptura { get; set; }
        public DateTime FechaEnvio { get; set; }
        public string TipoDocumento { get; set; }
        public string TipoAlerta { get; set; }
        public string IneRespuesta { get; set; }
        public string RevisionAnalista { get; set; }

    }
}
