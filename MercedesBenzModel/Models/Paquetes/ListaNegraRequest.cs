using System;

namespace MercedesBenzModel
{
    public class ListaNegraRequest
    {
        public int ListaNegraId { get; set; }
        public int SolicitanteId { get; set; }
        public int UsuarioId { get; set; } = -1;
        public string Motivo { get; set; }
        public int TipoMovimientoId { get; set; } = 1;
    }
}
