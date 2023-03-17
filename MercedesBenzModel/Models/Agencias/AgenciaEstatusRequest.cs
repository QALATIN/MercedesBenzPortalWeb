
using System;

namespace MercedesBenzModel
{
    public class AgenciaEstatusRequest
    {
        public bool Activar { get; set; }
        public int AgenciaId { get; set; }
        public string Motivo { get; set; }
        public int UsuarioIdActualizo { get; set; }
        public DateTime? FechaRegistro { get; set; }

    }
}
