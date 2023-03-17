using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MercedesBenzModel
{
    public class ReporteSemaforoDescarga
    {
        public string Agencia { get; set; }
        public string Folio { get; set; }
        public string Contrato { get; set; }
        public DateTime FechaEnvio { get; set; }
        public TimeSpan HoraEnvio { get; set; }
        public DateTime FechaCaptura { get; set; }
        public TimeSpan HoraCaptura { get; set; }
        public string Huellas { get; set; }
        public string Foto { get; set; }
        public string SemaforoAssure { get; set; }
        public string SemaforoLatin { get; set; }
        public string SemaforoGeneral { get; set; }
        public string SemaforoDeListaNegra { get; set; }
        public string SemaforoDeDocumento { get; set; }
        public string SemaforoDeHuellas { get; set; }
        public string SemaforoDeFolioYNombre { get; set; }
        public string SemaforoDeConsultaWeb { get; set; }
        public string SemaforoDeComparacionFacial { get; set; }
        public string TipoDeIdentificacion { get; set; }
    }
}
