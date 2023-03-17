
using System.Collections.Generic;

namespace MercedesBenzModel
{
    public class SolicitudComparacionHuellas
    {
        public Resultado Resultado { get; set; }
        public List<SolicitudHuella> Huellas { get; set; }
        public List<SolicitudHuellaCoincidente> HuellasCoincidentes { get; set; }
        public int AfisId { get; set; }
    }
}
