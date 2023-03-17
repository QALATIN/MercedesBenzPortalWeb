using System.Collections.Generic;

namespace MercedesBenzModel
{
    public class ListaResponse
    {
        public int error { get; set; }
        public string mensaje { get; set; }
        public string semaforo { get; set; }
        public int tiempo_espera { get; set; }
        public List<ValidacionListaResponse> validaciones { get; set; }
        public ListaDataResponse datos_validacion { get; set; }
    }
}
