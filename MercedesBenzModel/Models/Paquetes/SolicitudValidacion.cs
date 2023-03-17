using System;

namespace MercedesBenzModel
{
    public class SolicitudValidacion
    {
        public int ValidacionId { get; set; } = 0;
        public int SolicitanteId { get; set; } = 0;
        public int UsuarioConsultaId { get; set; }
        public string UsuarioConsultaNombre { get; set; }
        public string UsuarioConsultaApellidoPaterno { get; set; }
        public string UsuarioConsultaApellidoMaterno { get; set; }
        public DateTime? FechaConsulta { get; set; } = null;

        public string SemaforoIBMS { get; set; } = "gris";
        public string ResultadoIBMS { get; set; } = "";
        public DateTime? FechaIBMS { get; set; } = null;

        public string SemaforoListaNegra { get; set; } = "gris";
        public string ResultadoListaNegra { get; set; } = "";
        public DateTime? FechaListaNegra { get; set; } = null;
        public bool ValidarListaNegra { get; set; } = false;

        public string SemaforoIdentificacion { get; set; } = "gris";
        public string ResultadoIdentificacion { get; set; } = "";
        public DateTime? FechaIdentificacion { get; set; } = null;
        public bool ValidarIdentificacion { get; set; } = false;

        public string SemaforoFacial { get; set; } = "gris";
        public string ResultadoFacial { get; set; } = "";
        public DateTime? FechaFacial { get; set; } = null;
        public bool ValidarFacial { get; set; } = false;

        public string SemaforoCorreo { get; set; } = "gris";
        public string ResultadoCorreo { get; set; } = "";
        public DateTime? FechaCorreo { get; set; } = null;
        public bool ValidarCorreo { get; set; } = false;
        public int ScoreCorreo { get; set; } = -1;

        public string SemaforoTelefono { get; set; } = "gris";
        public string ResultadoTelefono { get; set; } = "";
        public DateTime? FechaTelefono { get; set; } = null;
        public bool ValidarTelefono { get; set; } = false;

        public string SemaforoCurp { get; set; } = "gris";
        public string ResultadoCurp { get; set; } = "";
        public DateTime? FechaCurp { get; set; } = null;
        public bool ValidarCurp { get; set; } = false;

        public string SemaforoIne { get; set; } = "gris";
        public string ResultadoIne { get; set; } = "";
        public DateTime? FechaIne { get; set; } = null;
        public bool ValidarIne { get; set; } = false;

        public string SemaforoComprobanteDomicilio { get; set; } = "gris";
        public string ResultadoComprobanteDomicilio { get; set; } = "";
        public DateTime? FechaComprobanteDomicilio { get; set; } = null;
        public bool ValidarComprobanteDomicilio { get; set; } = false;

        public string SemaforoComprobanteIngresos { get; set; } = "gris";
        public string ResultadoComprobanteIngresos { get; set; } = "";
        public DateTime? FechaComprobanteIngresos { get; set; } = null;
        public bool ValidarComprobanteIngresos { get; set; } = false;

        public string SemaforoComprobanteBancario { get; set; } = "gris";
        public string ResultadoComprobanteBancario { get; set; } = "";
        public DateTime? FechaComprobanteBancario { get; set; } = null;
        public bool ValidarComprobanteBancario { get; set; } = false;

        public string SemaforoListaAml { get; set; } = "gris";
        public string ResultadoListaAml { get; set; } = "";
        public DateTime? FechaListaAml { get; set; } = null;
        public bool ValidarListaAml { get; set; } = false;

        public string SemaforoAfis { get; set; } = "gris";
        public string ResultadoAfis { get; set; } = "";
        public DateTime? FechaAfis { get; set; } = null;
        public bool ValidarAfis { get; set; } = false;

    }
}
