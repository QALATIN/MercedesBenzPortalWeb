using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MercedesBenzModel
{
    public class AvisoPrivacidadRequest
    {
        public int Id { get; set; }
        public int SolicitanteId { get; set; }
        public int UsuarioId { get; set; }
        public string Referencia { get; set; }
        public byte[] DocumentoBase64 { get; set; }
        public DateTime FechaDeRegistro { get; set; }

    }
}
