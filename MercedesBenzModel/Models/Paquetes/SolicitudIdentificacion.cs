using System.Collections.Generic;

namespace MercedesBenzModel
{
    public class SolicitudIdentificacion
    {
        public Resultado Resultado { get; set; }
        public List<SolicitanteCapturaIdentificacion> Identificaciones { get; set; }
        public byte[] DocumentoPdf { get; set; }
    }
}
