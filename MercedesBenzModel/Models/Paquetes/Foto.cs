using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MercedesBenzModel
{
    public class Foto
    {
        public int FotoId { get; set; } = 0;
        public int SolicitanteId { get; set; } = 0;
        public byte[] Imagen { get; set; }
        public int FotoOrigenId { get; set; }
        public string Guid { get; set; }
        public int UsuarioId { get; set; }
        public DateTime FechaEnvio { get; set; }
        public DateTime FechaRegistro { get; set; }
        public DateTime? FechaBaja { get; set; } = null;
        public bool Activo { get; set; } = true;

    }
}
