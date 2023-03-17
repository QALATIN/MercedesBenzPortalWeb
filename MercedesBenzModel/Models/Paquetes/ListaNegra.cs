using System;

namespace MercedesBenzModel
{
    public class ListaNegra
    {
        public int ListaNegraId { get; set; }
        public int SolicitanteId { get; set; }
        public string Motivo { get; set; }
        public int TipoMovimientoId { get; set; }
        public int UsuarioId { get; set; }
        public DateTime FechaRegistro { get; set; }
        public DateTime? FechaBaja { get; set; } = null;
        public bool Activo { get; set; } = true;
    }
}
