using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MercedesBenzModel
{
    public class AvisoPrivacidad
    {
        public int AvisoPrivacidadId { get; set; }
        public int SolicitanteId { get; set; }
        public string Referencia { get; set; }
        public byte[] Imagen { get; set; }
        public int UsuarioId { get; set; }
        public DateTime FechaEnvio { get; set; }
        public DateTime FechaRegistro { get; set; }
        public DateTime? FechaBaja { get; set; } = null;
        public bool Activo { get; set; } = true;

    }
}
