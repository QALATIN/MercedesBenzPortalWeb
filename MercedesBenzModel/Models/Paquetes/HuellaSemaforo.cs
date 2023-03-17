using System;

namespace MercedesBenzModel
{
	public class HuellaSemaforo
	{
		public int ValidacionId { get; set; }
		public int SolicitanteId { get; set; }
		public string ClienteNombreCompleto { get; set; }
		public int AfisId { get; set; }
		public DateTime? AfisFecha { get; set; }
		public string SemaforoAfis { get; set; }
		public string ResultadoAfis { get; set; }
		public DateTime? FechaAfis { get; set; }


	}
}
