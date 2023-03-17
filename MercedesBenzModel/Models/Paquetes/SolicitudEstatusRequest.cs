using System;

namespace MercedesBenzModel
{
    public class SolicitudEstatusRequest
    {
        public DateTime FechaInicial { get; set; }
        public DateTime FechaFinal { get; set; }
        public SolicitudEstatus Estatus { get; set; }

    }
}
