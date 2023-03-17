using System;

namespace MercedesBenzModel
{
    public class SolicitanteListaNegra
    {
        public int ListaNegraId { get; set; }
        public int SolicitanteId { get; set; }
        public string Motivo { get; set; }
        public int TipoMovimientoId { get; set; }
        public int AnalistaId { get; set; }
        public string AnalistaUsuario { get; set; }
        public string AnalistaNombre { get; set; }
        public string AnalistaApellidoPaterno { get; set; }
        public string AnalistaApellidoMaterno { get; set; }
        public DateTime FechaCaptura { get; set; }

    }
}
