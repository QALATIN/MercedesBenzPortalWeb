using System;

namespace MercedesBenzModel
{
    public class ReporteDetalleEnvio : ReporteDetalleEnvioResponse
    {
        public readonly int SolicitanteId;
        public readonly int ValidacionId;
        public readonly DateTime? FechaIne;

    }
}
