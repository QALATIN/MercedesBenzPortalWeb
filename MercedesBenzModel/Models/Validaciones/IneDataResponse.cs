namespace MercedesBenzModel
{
    public class IneDataResponse
    {
        public string Modelo { get; set; }
        public string IneClaveDeElector { get; set; } = null;
        public string IneNumeroDeEmision { get; set; } = null;
        public string IneOcr { get; set; } = null;
        public string IneCic { get; set; } = null;
        public string IneIdentificadorCiudadano { get; set; } = null;
        public string IneAnioDeRegistro { get; set; } = null;
        public string IneAnioDeEmision { get; set; } = null;
        public string IneFechaDeConsulta { get; set; } = null;
        public string IneFechaDeVigencia { get; set; } = null;
        public string Respuesta { get; set; } = null;
        public string CodigoValidacion { get; set; } = null;
    }
}
