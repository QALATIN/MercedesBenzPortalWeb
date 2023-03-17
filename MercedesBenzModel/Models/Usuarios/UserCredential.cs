
namespace MercedesBenzModel
{
    public class UserCredential : UserIpAddress
    {
        public int UsuarioId { get; set; }
        public string NombreUsuario { get; set; }
        public string NombreCompleto { get; set; }
        public string Rol { get; set; }
        public string NombreAgencia { get; set; }
        public string Token { get; set; }
    }
}
