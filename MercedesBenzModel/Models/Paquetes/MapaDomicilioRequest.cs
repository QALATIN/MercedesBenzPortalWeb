using System;

namespace MercedesBenzModel
{
    public class MapaDomicilioRequest
    {
        public int SolicitanteId { get; set; }
        public string Html { get; set; }
        public DateTime FechaRegistro { get; set; }
    }
}
