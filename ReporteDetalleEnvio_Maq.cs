using System;

namespace MercedesBenzModel
{
    public class ReporteDetalleEnvio
    {
        public string AgenciaClave { get; set; }
        public DateTime FechaEnvio { get; set; }
        public string ClienteNombre { get; set; }
        public string FolioCD { get; set; }
        public string FolioIM { get; set; }
        public string TipoDocumento { get; set; }
        public string SemaforoIdentificacion { get; set; }
        public string SemaforoFacial { get; set; }
        public string Correo { get; set; }
        public string CorreoResultado { get; set; }
        public string RevisionAnalista { get; set; }

    }
}
