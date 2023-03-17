using System;

namespace MercedesBenzModel
{
    public class Solicitante
    {
        public int SolicitanteId { get; set; } = 0;
        public int SolicitanteIdOrigen { get; set; } = 0;
        public int UsuarioId { get; set; }
        public int AgenciaId { get; set; }
        public string Nombre { get; set; }
        public string ApellidoPaterno { get; set; }
        public string ApellidoMaterno { get; set; }
        public DateTime? FechaNacimiento { get; set; } = null;
        public string Curp { get; set; }
        public string CorreoElectronico { get; set; }
        public string TipoCliente { get; set; }
        public string Estatus { get; set; }
        public string Folio { get; set; }
        public string TipoDocumento { get; set; }
        public string NumeroDocumento { get; set; }
        public Guid Guid { get; set; }
        public string Telefono { get; set; }
        public string Sexo { get; set; }
        public string ResultadoGeneral { get; set; }
        public string LugarNacimiento { get; set; }
        public string CoordenadasGps { get; set; }
        public string ScoreComparacionFacial { get; set; }
        public string ResultadoComparacionFacial { get; set; }
        public string Nacionalidad { get; set; }
        public string DireccionCompleta { get; set; }
        public string CodigoPostal { get; set; }
        public string CalleNumero { get; set; }
        public string Colonia { get; set; }
        public string Municipio { get; set; }
        public string Estado { get; set; }
        public string PruebaVida { get; set; }
        public string Edad { get; set; }
        public string NombreCompletoSolicitante { get; set; }
        public byte[] DocumentoPdf { get; set; }

        public DateTime FechaEnvio { get; set; }
        public DateTime FechaRegistro { get; set; }
        public DateTime? FechaBaja { get; set; } = null;
        public bool Activo { get; set; } = true;

    }
}
