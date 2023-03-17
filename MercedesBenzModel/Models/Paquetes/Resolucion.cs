using System;

namespace MercedesBenzModel
{
    public class Resolucion
    {
        public int ResolucionId { get; set; }
        public int SolicitanteId { get; set; }
        public string Comentario { get; set; }
        public int TipoResolucionId { get; set; }
        public int UsuarioId { get; set; }
        public DateTime FechaRegistro { get; set; }
    }
}
