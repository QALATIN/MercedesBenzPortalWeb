using System;

namespace MercedesBenzModel
{
    public class ReporteSemaforoFacialDetalleResponse 
    {
        public string TipoAlerta { get; set; }
		public string ScoreFacial { get; set; }
		public string ClienteNombre { get; set; }
		public string Folio { get; set; }
		public DateTime FechaCaptura { get; set; }
		public DateTime FechaEnvio { get; set; }
		public string NombreAgencia { get; set; }

	}
}
