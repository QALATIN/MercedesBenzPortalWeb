using System;

namespace MercedesBenzModel
{
    public class SolicitanteDocumento
    {
		public int DocumentoId { get; set; }
		public int SolicitanteId { get; set; }
		public string TipoDocumentoId { get; set; }
		public string FormatoDocumento { get; set; }
		public byte[] Imagen { get; set; }
		public DateTime FechaEnvio { get; set; }
		public DateTime FechaCaptura { get; set; }
	}
}
