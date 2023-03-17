using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MercedesBenzModel
{
    public class SolicitudNotificacion
    {
        public int SolicitanteId { get; set; } = 0;
        public DateTime FechaEnvio { get; set; }

        public string Estatus { get; set; }
        public string NombreCompleto { get; set; }
        public string TiempoRegistro { get; set; }
    }
}
