using System;

namespace MercedesBenzModel
{
    public class ReporteSemaforo
    {
        public string Agencia { get; set; }
        public string Folio { get; set; }
        public DateTime FechaEnvio { get; set; }
        public string SemaforoListaNegra { get; set; }
        public string SemaforoIdentificacion { get; set; }
        public string SemaforoComparacionFacial { get; set; }
        public string SemaforoHuellas { get; set; }
        public string TipoIdentificacion { get; set; }
    }
}
