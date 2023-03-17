using System;
using System.Collections.Generic;

namespace MercedesBenzModel
{
    public class SolicitudFicha
    {
        public int SolicitanteId { get; set; } = 0;
        public int SolicitanteIdOrigen { get; set; } = 0;
        public string Folio { get; set; }
        public DateTime FechaCaptura { get; set; }
        public DateTime FechaEnvio { get; set; }
        public DateTime FechaUltimaConsulta { get; set; }
        public string SolicitanteNombre { get; set; }
        public string SolicitanteApellidoPaterno { get; set; }
        public string SolicitanteApellidoMaterno { get; set; }
        public string Edad { get; set; }
        public string Curp { get; set; }
        public DateTime? FechaNacimiento { get; set; } = null;
        public string Telefono { get; set; }
        public string CorreoElectronico { get; set; }
        public string TipoCliente { get; set; }
        public string CoordenadasGps { get; set; }
        public string CoordenadasGpsHtml { get; set; }
        public string DireccionCompleta { get; set; }
        public string Estatus { get; set; }
        public int EnroladorId { get; set; }
        public string EnroladorUsuario { get; set; }
        public string EnroladorNombre { get; set; }
        public string EnroladorApellidoPaterno { get; set; }
        public string EnroladorApellidoMaterno { get; set; }
        public int AgenciaId { get; set; }
        public string ClaveAgencia { get; set; }
        public string NombreAgencia { get; set; }
        public string TipoDocumento { get; set; }
        public string NumeroDocumento { get; set; }
        public Guid Guid { get; set; }
        public string Sexo { get; set; }
        public string LugarNacimiento { get; set; }
        public string ScoreComparacionFacial { get; set; }
        public string ResultadoComparacionFacial { get; set; }
        public string Nacionalidad { get; set; }
        public string CodigoPostal { get; set; }
        public string CalleNumero { get; set; }
        public string Colonia { get; set; }
        public string Municipio { get; set; }
        public string Estado { get; set; }
        public string PruebaVida { get; set; }
        public string NombreCompletoSolicitante { get; set; }
        public string HtmlGeoReferencia { get; set; }

        public int AfisId { get; set; }

        public bool ExisteAvisoPrivacidad { get; set; } = true;

        public SolicitudIne Ine { get; set; }
        public List<SolicitanteIdentificacion> Identificaciones { get; set; }
        public List<SolicitanteDocumentoCargado> DocumentosCargados { get; set; }
        public List<SolicitanteListaNegra> ListaNegra { get; set; }
        public SolicitanteResolucion Resolucion { get; set; }
        public SolicitudValidacion Validaciones { get; set; }

        public List<SolicitanteSolidario> Solidarios { get; set; }

    }
}
