using System;

namespace MercedesBenzModel
{
    public class Huella
    {
        public int HuellaId { get; set; }
        public int SolicitanteId { get; set; }
        public int DedoIndiceId { get; set; }
        public int DedoEstatusId { get; set; }
        public byte[] Imagen { get; set; }
        public int UsuarioId { get; set; }
        public DateTime FechaEnvio { get; set; }
        public DateTime FechaRegistro { get; set; }
        public DateTime? FechaBaja { get; set; } = null;
        public bool Activo { get; set; } = true;

    }
}
