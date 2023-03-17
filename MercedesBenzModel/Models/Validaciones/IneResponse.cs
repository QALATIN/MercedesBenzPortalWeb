using System.Collections.Generic;

namespace MercedesBenzModel
{
    public class IneResponse
    {
        public int error { get; set; }
        public string mensaje { get; set; }
        public string semaforo { get; set; }
        public int tiempo_espera { get; set; }
        public List<ValidacionResponse> validaciones { get; set; }
        public IneDataResponse datos_validacion { get; set; }
    }
}
