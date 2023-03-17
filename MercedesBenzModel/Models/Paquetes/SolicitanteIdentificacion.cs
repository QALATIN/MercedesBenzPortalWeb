using System;

namespace MercedesBenzModel
{
    public class SolicitanteIdentificacion
    {
		public int IdentificacionId { get; set; }
		public int SolicitanteId { get; set; }
		public string Serie { get; set; }
		public string NumeroEmision { get; set; }
		public string Cic { get; set; }
		public string Ocr { get; set; }
		public string ClaveElector { get; set; }
		public string IdentificadorCiudadano { get; set; }
		public string Vigencia { get; set; }
		public string AnioRegistro { get; set; }
		public string Emision { get; set; }
		public string MRZ { get; set; }
		public DateTime FechaEnvio { get; set; }
		public DateTime FechaCaptura { get; set; }

	}
}
