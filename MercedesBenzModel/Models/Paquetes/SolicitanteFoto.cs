using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MercedesBenzModel
{
    public class SolicitanteFoto
    {
		public int FotoId { get; set; }
		public int SolicitanteId { get; set; }
		public byte[] Imagen { get; set; }
		public int FotoOrigenId { get; set; }
		public string FotoOrigenNombre { get; set; }
		public DateTime FechaEnvio { get; set; }
		public DateTime FechaCaptura { get; set; }
	}
}
