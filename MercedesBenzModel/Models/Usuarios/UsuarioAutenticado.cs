using System;

namespace MercedesBenzModel
{
    public class UsuarioAutenticado
    {
        public string NombreUsuario { get; set; }
        public string Rol { get; set; }
        public string Token { get; set; }
        public DateTime TokenVigencia { get; set; }

    }
}
