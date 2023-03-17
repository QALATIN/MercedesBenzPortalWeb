using System;

namespace MercedesBenzModel
{
    public class BusquedaRequest
    {
        public string Nombre { get; set; }
        public string ApellidoPaterno { get; set; }
        public string ApellidoMaterno { get; set; }
        public string Folio { get; set; }
        public DateTime? FechaInicial { get; set; } = null;
        public DateTime? FechaFinal { get; set; } = null;
        public int NumeroPagina { get; set; } = 0;

    }
}
