using MercedesBenzModel;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace MercedesBenzServerWeb.Authentication
{
    public class CustomAuthentication : AuthenticationStateProvider
    {
        private readonly ProtectedSessionStorage _sessionStorage;
        private readonly ClaimsPrincipal _anonymous = new(new ClaimsIdentity());

        public CustomAuthentication(ProtectedSessionStorage sessionStorage)
        {
            _sessionStorage = sessionStorage;
        }

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            try
            {
                var sessionStorage = await _sessionStorage.GetAsync<UserCredential>("UsuarioSession");
                var usuarioSession = sessionStorage.Success ? sessionStorage.Value : null;
                if (usuarioSession == null)
                {
                    return await Task.FromResult(new AuthenticationState(_anonymous));
                }
                else
                {
                    ClaimsPrincipal claimsPrincipal = UsuarioClaims(usuarioSession);
                    return await Task.FromResult(new AuthenticationState(claimsPrincipal));
                }
            }
            catch
            {
                return await Task.FromResult(new AuthenticationState(_anonymous));
            }
        }

        public async Task AddAuthenticationStateAsync(UserCredential usuario)
        {
            await _sessionStorage.SetAsync("UsuarioSession", usuario);
            ClaimsPrincipal claimsPrincipal = UsuarioClaims(usuario);
            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(claimsPrincipal)));
        }

        public async Task DeleteAuthenticationStateAsync()
        {
            await _sessionStorage.DeleteAsync("UsuarioSession");
            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(_anonymous)));
        }

        private static ClaimsPrincipal UsuarioClaims(UserCredential usuario)
        {
            return new ClaimsPrincipal(new ClaimsIdentity(new List<Claim>
            {
                new Claim(ClaimTypes.Name, usuario.NombreUsuario),
                new Claim(ClaimTypes.Role, usuario.Rol),
            }, "CustomAuthentication"));
        }

    }
}
