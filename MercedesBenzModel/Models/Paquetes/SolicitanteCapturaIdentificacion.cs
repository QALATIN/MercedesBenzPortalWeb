using System;

namespace MercedesBenzModel
{
    public class SolicitanteCapturaIdentificacion
    {
		public int CapturaIdentificacionId { get; set; }
		public int SolicitanteId { get; set; }
		public byte[] Imagen { get; set; }
		public int CapturaNombreId { get; set; }
		public string CapturaNombre { get; set; }
		public DateTime FechaEnvio { get; set; }
		public DateTime FechaCaptura { get; set; }
	}
}
