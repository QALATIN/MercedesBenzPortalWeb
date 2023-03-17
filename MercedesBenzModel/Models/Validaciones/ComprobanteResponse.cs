using System.Collections.Generic;

namespace MercedesBenzModel
{
    public class ComprobanteResponse
    {
        public int error { get; set; }
        public string mensaje { get; set; }
        public string semaforo { get; set; }
        public int tiempo_espera { get; set; }
        public List<ValidacionResponse> validaciones { get; set; }
        public ComprobanteDataResponse datos_validacion { get; set; }
        public string documento_tipo { get; set; }
    }
}
