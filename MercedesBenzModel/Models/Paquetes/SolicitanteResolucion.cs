using System;

namespace MercedesBenzModel
{
    public class SolicitanteResolucion
    {
        public int ResolucionId { get; set; } = 0;
        public int SolicitanteId { get; set; } = 0;
        public string Comentario { get; set; }
        public int TipoResolucionId { get; set; } = 0;
        public string TipoResolucionNombre { get; set; }
        public int AnalistaId { get; set; }
        public string AnalistaUsuario { get; set; }
        public string AnalistaNombre { get; set; }
        public string AnalistaApellidoPaterno { get; set; }
        public string AnalistaApellidoMaterno { get; set; }
        public DateTime? FechaCaptura { get; set; } = null;

    }
}
