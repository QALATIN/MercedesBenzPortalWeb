using System;

namespace MercedesBenzModel
{
    public class ReporteListaNegra
    {
        public byte[] ClienteImagen { get; set; }
        public string ClienteNombre { get; set; }
        public string Folio { get; set; }
        public string Contrato { get; set; }
        public string Motivo { get; set; }
        public DateTime FechaIngreso { get; set; }
        public string AgenciaNombre { get; set; }
    }
}
