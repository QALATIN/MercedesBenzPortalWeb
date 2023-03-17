using System;

namespace MercedesBenzModel
{
    public class ResolucionRequest
    {
        public int ResolucionId { get; set; }
        public int SolicitanteId { get; set; }
        public int UsuarioId { get; set; } = -1;
        public string Comentario { get; set; }
        public int TipoResolucionId { get; set; } = 1;

        public string SemaforoIBMS { get; set; }
        public string ResultadoIBMS { get; set; }
        public string SemaforoListaNegra { get; set; } = "gris";
        public string ResultadoListaNegra { get; set; } = "";
        public DateTime? FechaListaNegra { get; set; } = null;

    }
}
