using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MercedesBenzModel
{
    public class HuellaRequest
    {
        public int Id { get; set; }
        public int SolicitanteId { get; set; }
        public int UsuarioId { get; set; }
        public int DedoId { get; set; }
        public int OmisionId { get; set; }
        public byte[] HuellaBase64 { get; set; }
        public DateTime FechaDeRegistro { get; set; }

    }
}
