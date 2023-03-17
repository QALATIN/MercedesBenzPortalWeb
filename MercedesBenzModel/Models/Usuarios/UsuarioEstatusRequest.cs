
using System;

namespace MercedesBenzModel
{
    public class UsuarioEstatusRequest
    {
        public bool Activar { get; set; }
        public int UsuarioId { get; set; }
        public string Motivo { get; set; }
        public int UsuarioIdActualizo { get; set; }
        public DateTime? FechaRegistro { get; set; }

    }
}
