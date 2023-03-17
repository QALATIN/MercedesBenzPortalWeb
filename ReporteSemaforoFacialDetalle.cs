using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MercedesBenzModel
{
    public class ReporteSemaforoFacialDetalle 
    {
        public string TipoAlerta { get; set; }
		public float ScoreFacial { get; set; }
		public string ClienteNombre { get; set; }
		public string Folio { get; set; }
		public string Contrato { get; set; }
		public DateTime FechaCaptura { get; set; }
		public DateTime FechaEnvio { get; set; }
		public string NombreAgencia { get; set; }

	}
}
