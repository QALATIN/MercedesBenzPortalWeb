namespace MercedesBenzModel
{
    public class AutenticacionResponse
    {
        public string NombreUsuario { get; set; }
        public string NombrePerfil { get; set; }
        public string Token { get; set; }
        public int ExpiresIn { get; set; }

    }
}
