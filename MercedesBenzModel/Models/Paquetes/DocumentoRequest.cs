using System;

namespace MercedesBenzModel
{
    public class DocumentoRequest
    {
        public int Id { get; set; }
        public int SolicitanteId { get; set; }
        public int UsuarioId { get; set; }
        public string TipoDocumento { get; set; }
        public string NombreDocumento { get; set; }
        public byte[] DocumentoBase64 { get; set; }
        public DateTime FechaDeRegistro { get; set; }
    }
}
