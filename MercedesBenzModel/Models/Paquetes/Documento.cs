using System;

namespace MercedesBenzModel
{
    public class Documento
    {
        public int DocumentoId { get; set; }
        public int SolicitanteId { get; set; }
        public string TipoDocumento { get; set; }
        public string NombreDocumento { get; set; }
        public byte[] Imagen { get; set; }
        public int UsuarioId { get; set; }
        public DateTime FechaEnvio { get; set; }
        public DateTime FechaRegistro { get; set; }
        public DateTime? FechaBaja { get; set; } = null;
        public bool Activo { get; set; } = true;

    }
}
