using System;
using System.ComponentModel.DataAnnotations;

namespace MercedesBenzModel
{
    public class SolicitanteRequest
    {

        public int SolicitanteId { get; set; } = 0;
        [Required(ErrorMessage = "La Fecha de Registro es obligatorio")]
        public DateTime FechaDeRegistro { get; set; }
        public string Nombre { get; set; }
        public string Paterno { get; set; }
        public string Materno { get; set; }
        public string FechaDeNacimiento { get; set; } = null;
        public string Curp { get; set; }
        public string CorreoElectronico { get; set; }
        public string TipoCliente { get; set; }
        public string Estatus { get; set; }
        public string Folio { get; set; }

        [Required(ErrorMessage = "El Id de Usuario es obligatorio")]
        public int UsuarioId { get; set; }
        public string TipoDeDocumento { get; set; }
        public string NumeroDeDocumento { get; set; }
        public Guid Guid { get; set; }
        public string Telefono { get; set; }
        public string Sexo { get; set; }
        public string ResultadoGeneral { get; set; }
        public string LugarDeNacimiento { get; set; }
        public string CoordenadasGps { get; set; }
        public string ScoreDeLaComparacionFacial { get; set; }
        public string ResultadoDeLaComparacionFacial { get; set; }
        public string Nacionalidad { get; set; }
        public string DireccionCompleta { get; set; }
        public string CodigoPostal { get; set; }
        public string CalleNumero { get; set; }
        public string Colonia { get; set; }
        public string Municipio { get; set; }
        public string Estado { get; set; }
        public string PruebaDeVida { get; set; }
        public byte[] SelfieBase64 { get; set; }
        public string Edad { get; set; }
        public string NombreCompletoDelSolicitante { get; set; }

        public string Serie { get; set; }
        public string NumeroEmision { get; set; }
        public string CIC { get; set; }
        public string OCR { get; set; }
        public string ClaveElector { get; set; }
        public string IdentificadorCiudadano { get; set; }
        public string Vigencia { get; set; }
        public string AnioRegistro { get; set; }
        public string Emision { get; set; }
        public string Mrz { get; set; }
        public string TipoIne { get; set; }
        public byte[] DocumentoPdfBase64 { get; set; }

        public byte[] CapturaFrenteEnBase64 { get; set; }   // CapturaDeIdentificacion 1
        public byte[] CapturaReversoEnBase64 { get; set; }  //  CapturaDeIdentificacion 4
        public byte[] FotoEnBase64 { get; set; }    // Foto 1
        public byte[] FotoDeIdentificacionEnBase64 { get; set; }    // Foto 2

    }
}
