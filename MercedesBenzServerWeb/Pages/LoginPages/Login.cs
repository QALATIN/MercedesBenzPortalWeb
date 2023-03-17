using MercedesBenzModel;
using MercedesBenzServerWeb.Authentication;
using Microsoft.JSInterop;
using System;
using System.Threading.Tasks;

namespace MercedesBenzServerWeb.Pages.LoginPages
{
    public partial class Login
    {
        private UsuarioLogin usuario = new();
        private string mensaje = "";
        private bool IsSubmit = false;
        private bool isLoadingData = false;
        private bool popupOpen = false;
        private bool popupOpenNotify = false;
        private string mensajePopup = "";
        private bool recordarme = false;
        private string version = "";

        protected override async Task OnInitializedAsync()
        {
            var authentication = await AuthenticationStateProvider.GetAuthenticationStateAsync();
            var userAuth = authentication.User;
            if (userAuth.Identity.IsAuthenticated)
            {
                NavigationManager.NavigateTo("/index");
            } 
            else
            {
                var assemblyVersion = typeof(Program).Assembly.GetName().Version;
                version = $"Versión: {assemblyVersion.Major}.{assemblyVersion.Minor}.{assemblyVersion.Build}";
                var user = await LocalStorage.GetUserAsync("user");
                if (user != null)
                {
                    recordarme = true;
                    usuario = user;
                }
            }
        }

        private async Task OnValidSubmit()
        {
            mensaje = "";
            IsSubmit = true;
            isLoadingData = true;
            try
            {
                if (recordarme)
                    await LocalStorage.SetUserAsync("user", usuario);
                else
                    await LocalStorage.DeleteLocalStorageAsync("user");

                (string mensajeAutenticacion, AutenticacionResponse autenticacion) = await ServiceAutenticacion.GetValidacionAutenticacionAsync(usuario);
                if (string.IsNullOrEmpty(mensajeAutenticacion))
                {
                    if (autenticacion.NombrePerfil == "Administrador" || autenticacion.NombrePerfil == "Análista")
                    {
                        Credential.NombreUsuario = autenticacion.NombreUsuario;
                        Credential.Rol = autenticacion.NombrePerfil;
                        Credential.Token = autenticacion.Token;

                        (string mensajeCredencial, UsuarioCuenta credencial) = await ServiceUsuario.GetCredencialAsync();
                        if (string.IsNullOrEmpty(mensajeCredencial))
                        {
                            Credential.UsuarioId = credencial.UsuarioId;
                            Credential.NombreCompleto = credencial.NombreCompleto;
                            Credential.NombreAgencia = credencial.NombreAgencia;

                            var authentication = (CustomAuthentication)AuthenticationStateProvider;
                            await authentication.AddAuthenticationStateAsync(Credential);
                            await LocalStorage.SetCredentialAsync("credential", Credential);
                            await JS.InvokeVoidAsync("Blazor._internal.navigationManager.navigateTo", "/index", false, true);
                        }
                        else
                        {
                            mensaje = mensajeCredencial;
                        }
                    }
                    else
                    {
                        mensaje = "Perfil no autorizado para usar el portal";
                    }
                }
                else
                {
                    mensaje = mensajeAutenticacion;
                }
            }
            catch (Exception ex)
            {
                mensaje = ex.Message;
            }
            if (!string.IsNullOrEmpty(mensaje))
                await JS.InvokeVoidAsync("FocusInputLogin", "LoginInputUsuario", "LoginInputPassword");
            IsSubmit = false;
            isLoadingData = false;
        }

        private void SendMail()
        {
            mensajePopup = "Coloca tu correo electrónico para restablecer tu contraseña";
            popupOpen = true;
        }

        private void OnPopupClose(bool respuesta)
        {
            popupOpen = false;
            if (respuesta)
            {
                popupOpenNotify = true;
            }
        }

        private void OnPopupNotifyClose(bool respuesta)
        {
            popupOpenNotify = false;
        }

    }
}
