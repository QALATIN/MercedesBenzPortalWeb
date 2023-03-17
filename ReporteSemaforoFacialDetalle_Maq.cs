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
		public string Qr { get; set; }
		public string Curp { get; set; }
		public string ClienteNombre { get; set; }
		public string ClienteApellidoPaterno { get; set; }
		public string ClienteApellidoMaterno { get; set; }
		public DateTime FechaCaptura { get; set; }
		public DateTime FechaEnvio { get; set; }
		public string AgenciaClave { get; set; }
		public string AgenciaNombre { get; set; }
		public string UsuarioEnrolador { get; set; }

	}
}
