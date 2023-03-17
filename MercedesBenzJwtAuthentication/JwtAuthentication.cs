using MercedesBenzLibrary;
using MercedesBenzModel;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace MercedesBenzJwtAuthentication
{
    public class JwtAuthentication
    {

        private readonly string _key;
        private readonly string _issuer;
        private readonly string _audience;

        public JwtAuthentication()
        {
            _key = ApplicationSettings.GetJwtKey();
            _issuer = ApplicationSettings.GetJwtIssuer();
            _audience = ApplicationSettings.GetJwtAudience();
        }

        public AutenticacionResponse GenerarToken(UsuarioResultado Usuario)
        {
            var tokenKey = Encoding.ASCII.GetBytes(_key);
            var signingCredentials = new SigningCredentials(
                new SymmetricSecurityKey(tokenKey)
                , SecurityAlgorithms.HmacSha256Signature
            );

            //DateTime tokenExpires = DateTime.Now.AddDays(JWT_TOKEN_VALIDITY_DAYS);
            DateTime tokenExpires = new(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 23, 59, 59);
            tokenExpires = tokenExpires.AddDays(1);

            var claims = new[] {
               new Claim(ClaimTypes.Name, Usuario.NombreUsuario),
               new Claim(ClaimTypes.Role, Usuario.NombrePerfil)
            };

            var claimsIdentity = new ClaimsIdentity(claims);

            var securityTokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = claimsIdentity,
                SigningCredentials = signingCredentials,
                Issuer = _issuer,
                Audience = _audience,
                Expires = tokenExpires
            };

            var jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
            var securityToken = jwtSecurityTokenHandler.CreateToken(securityTokenDescriptor);
            var token = jwtSecurityTokenHandler.WriteToken(securityToken);

            return new AutenticacionResponse
            {
                NombreUsuario = Usuario.NombreUsuario,
                NombrePerfil = Usuario.NombrePerfil,
                Token = token,
                ExpiresIn = (int)tokenExpires.Subtract(DateTime.Now).TotalSeconds
            };
        }

    }

}
