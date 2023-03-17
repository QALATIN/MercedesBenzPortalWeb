using System;

namespace MercedesBenzModel
{
    public class ValidationResult
    {
        public int ValidacionId { get; set; }
        public string Semaforo { get; set; }
        public string Resultado { get; set; }
        public DateTime Fecha { get; set; }
        public TipoSemaforo TipoValidacion { get; set; }
    }
}
