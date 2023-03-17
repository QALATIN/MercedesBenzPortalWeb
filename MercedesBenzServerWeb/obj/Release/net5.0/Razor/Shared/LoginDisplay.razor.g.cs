#pragma checksum "D:\Proyectos\Net\MercedesBenzWeb\MercedesBenzServerWeb\Shared\LoginDisplay.razor" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "64d4789676b23986815ad9e023ca751a803b79d1"
// <auto-generated/>
#pragma warning disable 1591
namespace MercedesBenzServerWeb.Shared
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Components;
#nullable restore
#line 1 "D:\Proyectos\Net\MercedesBenzWeb\MercedesBenzServerWeb\_Imports.razor"
using System.Net.Http;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "D:\Proyectos\Net\MercedesBenzWeb\MercedesBenzServerWeb\_Imports.razor"
using Microsoft.AspNetCore.Authorization;

#line default
#line hidden
#nullable disable
#nullable restore
#line 3 "D:\Proyectos\Net\MercedesBenzWeb\MercedesBenzServerWeb\_Imports.razor"
using Microsoft.AspNetCore.Components.Authorization;

#line default
#line hidden
#nullable disable
#nullable restore
#line 4 "D:\Proyectos\Net\MercedesBenzWeb\MercedesBenzServerWeb\_Imports.razor"
using Microsoft.AspNetCore.Components.Forms;

#line default
#line hidden
#nullable disable
#nullable restore
#line 5 "D:\Proyectos\Net\MercedesBenzWeb\MercedesBenzServerWeb\_Imports.razor"
using Microsoft.AspNetCore.Components.Routing;

#line default
#line hidden
#nullable disable
#nullable restore
#line 6 "D:\Proyectos\Net\MercedesBenzWeb\MercedesBenzServerWeb\_Imports.razor"
using Microsoft.AspNetCore.Components.Web;

#line default
#line hidden
#nullable disable
#nullable restore
#line 7 "D:\Proyectos\Net\MercedesBenzWeb\MercedesBenzServerWeb\_Imports.razor"
using Microsoft.AspNetCore.Components.Web.Virtualization;

#line default
#line hidden
#nullable disable
#nullable restore
#line 8 "D:\Proyectos\Net\MercedesBenzWeb\MercedesBenzServerWeb\_Imports.razor"
using Microsoft.JSInterop;

#line default
#line hidden
#nullable disable
#nullable restore
#line 9 "D:\Proyectos\Net\MercedesBenzWeb\MercedesBenzServerWeb\_Imports.razor"
using MercedesBenzServerWeb;

#line default
#line hidden
#nullable disable
#nullable restore
#line 10 "D:\Proyectos\Net\MercedesBenzWeb\MercedesBenzServerWeb\_Imports.razor"
using MercedesBenzServerWeb.Shared;

#line default
#line hidden
#nullable disable
#nullable restore
#line 11 "D:\Proyectos\Net\MercedesBenzWeb\MercedesBenzServerWeb\_Imports.razor"
using MercedesBenzModel;

#line default
#line hidden
#nullable disable
#nullable restore
#line 12 "D:\Proyectos\Net\MercedesBenzWeb\MercedesBenzServerWeb\_Imports.razor"
using Helpers;

#line default
#line hidden
#nullable disable
#nullable restore
#line 13 "D:\Proyectos\Net\MercedesBenzWeb\MercedesBenzServerWeb\_Imports.razor"
using MercedesBenzServerWeb.Authentication;

#line default
#line hidden
#nullable disable
#nullable restore
#line 14 "D:\Proyectos\Net\MercedesBenzWeb\MercedesBenzServerWeb\_Imports.razor"
using Newtonsoft.Json;

#line default
#line hidden
#nullable disable
#nullable restore
#line 15 "D:\Proyectos\Net\MercedesBenzWeb\MercedesBenzServerWeb\_Imports.razor"
using BlazorPro.Spinkit;

#line default
#line hidden
#nullable disable
    public partial class LoginDisplay : Microsoft.AspNetCore.Components.ComponentBase, IDisposable
    {
        #pragma warning disable 1998
        protected override void BuildRenderTree(Microsoft.AspNetCore.Components.Rendering.RenderTreeBuilder __builder)
        {
            __builder.OpenComponent<Microsoft.AspNetCore.Components.Authorization.AuthorizeView>(0);
            __builder.AddAttribute(1, "Roles", "Administrador, Análista");
            __builder.AddAttribute(2, "Authorized", (Microsoft.AspNetCore.Components.RenderFragment<Microsoft.AspNetCore.Components.Authorization.AuthenticationState>)((context) => (__builder2) => {
                __builder2.OpenElement(3, "nav");
                __builder2.AddAttribute(4, "id", "menu-login");
                __builder2.OpenElement(5, "ul");
                __builder2.OpenElement(6, "li");
                __builder2.AddAttribute(7, "class", "mbz-nav");
                __builder2.OpenElement(8, "div");
                __builder2.AddAttribute(9, "class", "d-flex");
                __builder2.AddAttribute(10, "style", "align-items:center");
                __builder2.OpenElement(11, "a");
                __builder2.AddAttribute(12, "class");
                __builder2.AddAttribute(13, "href", "javascript: void(0);");
                __builder2.OpenElement(14, "div");
                __builder2.AddAttribute(15, "class", "mbz-ctr-header-notify");
                __builder2.OpenElement(16, "div");
                __builder2.AddAttribute(17, "style", (
#nullable restore
#line 15 "D:\Proyectos\Net\MercedesBenzWeb\MercedesBenzServerWeb\Shared\LoginDisplay.razor"
                                              totalNotificaciones == 0 ? "" : "position: relative; top: 14px;"

#line default
#line hidden
#nullable disable
                ) + " ");
                __builder2.AddMarkupContent(18, "<img src=\"../image/Notificaciones.png\">");
                __builder2.CloseElement();
                __builder2.AddMarkupContent(19, "\r\n                                ");
                __builder2.OpenElement(20, "div");
                __builder2.AddAttribute(21, "style", "position:" + " relative;" + " top:" + " -28px;" + " left:" + " 16px;" + " " + (
#nullable restore
#line 18 "D:\Proyectos\Net\MercedesBenzWeb\MercedesBenzServerWeb\Shared\LoginDisplay.razor"
                                                                                          totalNotificaciones == 0 ? "display: none" : string.Empty

#line default
#line hidden
#nullable disable
                ));
                __builder2.OpenElement(22, "span");
                __builder2.AddAttribute(23, "class", "mbz-span-sem-rojo");
                __builder2.AddAttribute(24, "style", "width: 14px; height: 14px; font-size: 8px; text-align: center; padding-top: 2px; ");
                __builder2.AddContent(25, 
#nullable restore
#line 19 "D:\Proyectos\Net\MercedesBenzWeb\MercedesBenzServerWeb\Shared\LoginDisplay.razor"
                                                                                                                                                               totalNotificaciones

#line default
#line hidden
#nullable disable
                );
                __builder2.CloseElement();
                __builder2.CloseElement();
                __builder2.CloseElement();
                __builder2.CloseElement();
                __builder2.AddMarkupContent(26, "\r\n                        ");
                __builder2.AddMarkupContent(27, "<div class=\"mbz-ctr-header-image\"><img src=\"../image/UsuarioAdministrativo.png\"></div>\r\n                        ");
                __builder2.OpenElement(28, "div");
                __builder2.AddAttribute(29, "class", "mbz-ctr-header-name mbz-text-white");
#nullable restore
#line 27 "D:\Proyectos\Net\MercedesBenzWeb\MercedesBenzServerWeb\Shared\LoginDisplay.razor"
                             if (Credential.NombreCompleto != null)
                            {
                                

#line default
#line hidden
#nullable disable
#nullable restore
#line 29 "D:\Proyectos\Net\MercedesBenzWeb\MercedesBenzServerWeb\Shared\LoginDisplay.razor"
                                 if (Credential.NombreCompleto.Length > 30)
                                {

#line default
#line hidden
#nullable disable
                __builder2.OpenElement(30, "span");
                __builder2.AddContent(31, 
#nullable restore
#line 31 "D:\Proyectos\Net\MercedesBenzWeb\MercedesBenzServerWeb\Shared\LoginDisplay.razor"
                                           Credential.NombreCompleto.Substring(0, 30)

#line default
#line hidden
#nullable disable
                );
                __builder2.CloseElement();
#nullable restore
#line 31 "D:\Proyectos\Net\MercedesBenzWeb\MercedesBenzServerWeb\Shared\LoginDisplay.razor"
                                                                                             }
                                else
                                {

#line default
#line hidden
#nullable disable
                __builder2.OpenElement(32, "span");
                __builder2.AddContent(33, 
#nullable restore
#line 34 "D:\Proyectos\Net\MercedesBenzWeb\MercedesBenzServerWeb\Shared\LoginDisplay.razor"
                                           Credential.NombreCompleto

#line default
#line hidden
#nullable disable
                );
                __builder2.CloseElement();
#nullable restore
#line 35 "D:\Proyectos\Net\MercedesBenzWeb\MercedesBenzServerWeb\Shared\LoginDisplay.razor"
                                }

#line default
#line hidden
#nullable disable
#nullable restore
#line 35 "D:\Proyectos\Net\MercedesBenzWeb\MercedesBenzServerWeb\Shared\LoginDisplay.razor"
                                 
                            }

#line default
#line hidden
#nullable disable
                __builder2.CloseElement();
                __builder2.CloseElement();
                __builder2.AddMarkupContent(34, "\r\n                    ");
                __builder2.OpenComponent<MercedesBenzServerWeb.Pages.Notificaciones>(35);
                __builder2.AddAttribute(36, "Solicitudes", global::Microsoft.AspNetCore.Components.CompilerServices.RuntimeHelpers.TypeCheck<System.Collections.Generic.List<MercedesBenzModel.SolicitudNotificacion>>(
#nullable restore
#line 39 "D:\Proyectos\Net\MercedesBenzWeb\MercedesBenzServerWeb\Shared\LoginDisplay.razor"
                                                                              notificaciones

#line default
#line hidden
#nullable disable
                ));
                __builder2.CloseComponent();
                __builder2.CloseElement();
                __builder2.CloseElement();
                __builder2.CloseElement();
#nullable restore
#line 44 "D:\Proyectos\Net\MercedesBenzWeb\MercedesBenzServerWeb\Shared\LoginDisplay.razor"
         if (AppState.PopupLogout)
        {

#line default
#line hidden
#nullable disable
                __builder2.OpenComponent<MercedesBenzServerWeb.Pages.Popup.PopupPregunta>(37);
                __builder2.AddAttribute(38, "Texto", global::Microsoft.AspNetCore.Components.CompilerServices.RuntimeHelpers.TypeCheck<System.String>(
#nullable restore
#line 46 "D:\Proyectos\Net\MercedesBenzWeb\MercedesBenzServerWeb\Shared\LoginDisplay.razor"
                                                                    mensajePopup

#line default
#line hidden
#nullable disable
                ));
                __builder2.AddAttribute(39, "OnClose", global::Microsoft.AspNetCore.Components.CompilerServices.RuntimeHelpers.TypeCheck<Microsoft.AspNetCore.Components.EventCallback<System.Boolean>>(Microsoft.AspNetCore.Components.EventCallback.Factory.Create<System.Boolean>(this, 
#nullable restore
#line 46 "D:\Proyectos\Net\MercedesBenzWeb\MercedesBenzServerWeb\Shared\LoginDisplay.razor"
                                                                                           OnPopupClose

#line default
#line hidden
#nullable disable
                )));
                __builder2.CloseComponent();
#nullable restore
#line 47 "D:\Proyectos\Net\MercedesBenzWeb\MercedesBenzServerWeb\Shared\LoginDisplay.razor"
        }

#line default
#line hidden
#nullable disable
            }
            ));
            __builder.AddAttribute(40, "NotAuthorized", (Microsoft.AspNetCore.Components.RenderFragment<Microsoft.AspNetCore.Components.Authorization.AuthenticationState>)((context) => (__builder2) => {
                __builder2.OpenElement(41, "span");
                __builder2.AddContent(42, 
#nullable restore
#line 51 "D:\Proyectos\Net\MercedesBenzWeb\MercedesBenzServerWeb\Shared\LoginDisplay.razor"
               Credential.NombreCompleto

#line default
#line hidden
#nullable disable
                );
                __builder2.AddContent(43, " - ");
                __builder2.AddContent(44, 
#nullable restore
#line 51 "D:\Proyectos\Net\MercedesBenzWeb\MercedesBenzServerWeb\Shared\LoginDisplay.razor"
                                            Credential.Rol

#line default
#line hidden
#nullable disable
                );
                __builder2.CloseElement();
            }
            ));
            __builder.CloseComponent();
        }
        #pragma warning restore 1998
#nullable restore
#line 55 "D:\Proyectos\Net\MercedesBenzWeb\MercedesBenzServerWeb\Shared\LoginDisplay.razor"
      
    private System.Threading.Timer? timer;
    private string mensaje { get; set; }
    private List<SolicitudNotificacion> notificaciones = null;
    private int totalNotificaciones = 0;

    private string mensajePopup = "¿Deseas cerrar sesión?";

    protected override async Task OnInitializedAsync()
    {

        if (string.IsNullOrEmpty(Credential.NombreUsuario))
        {
            var credential = await LocalStorage.GetCredentialAsync("credential");
            if (credential != null)
                Credential = credential;
        }

        AppState.OnChange += StateHasChanged;
        AppState.OnUpdateNotify += RefrescarNotificaciones;

        timer = new System.Threading.Timer(async (object? stateInfo) =>
        {
            await ConsultarDatos();
            await InvokeAsync(StateHasChanged);
        }, new System.Threading.AutoResetEvent(false), 1000, 29000);

        var inactividad = Task.Run(async () => { await JS.InvokeVoidAsync("TimerSesion", DotNetObjectReference.Create(this)); });

    }

    private void RefrescarNotificaciones()
    {
        Task.Run(async () =>
        {
            await ConsultarDatos();
            await InvokeAsync(StateHasChanged);
        });
    }

    private async Task ConsultarDatos()
    {
        totalNotificaciones = 0;
        (string mensajeResponse, RespuestaPaginada respuestaResponse) = await Service.GetSolicitudesNotificacionesAsync();
        if (respuestaResponse != null)
        {
            notificaciones = JsonConvert.DeserializeObject<IEnumerable<SolicitudNotificacion>>(respuestaResponse.Data.ToString()).ToList();
            if (notificaciones != null)
                totalNotificaciones = notificaciones.Count;
        }
        else
            notificaciones = null;
    }

    private async void OnPopupClose(bool respuesta)
    {
        AppState.ShowPopup(false);
        if (respuesta)
        {
            try
            {
                await Logout();
            }
            catch { }

            NavigationManager.NavigateTo("/login");
        }
    }

    void IDisposable.Dispose()
    {
        AppState.OnChange -= StateHasChanged;
        AppState.OnUpdateNotify -= RefrescarNotificaciones;
        timer?.Dispose();
        timer = null;
    }

    [JSInvokable]
    public async Task Logout()
    {
        try
        {
            await JS.InvokeVoidAsync("TimerSesionDesactivar");
            Busqueda.NumeroPagina = 0;
            var customAuthentication = (CustomAuthentication)AuthenticationStateProvider;
            await customAuthentication.DeleteAuthenticationStateAsync();
            await ServiceUsuario.LogOutAsync(Credential.NombreUsuario);
        }
        catch { }

        NavigationManager.NavigateTo("/login");
    }


#line default
#line hidden
#nullable disable
        [global::Microsoft.AspNetCore.Components.InjectAttribute] private LocalStorage LocalStorage { get; set; }
        [global::Microsoft.AspNetCore.Components.InjectAttribute] private AuthenticationStateProvider AuthenticationStateProvider { get; set; }
        [global::Microsoft.AspNetCore.Components.InjectAttribute] private Services.IUsuarioService ServiceUsuario { get; set; }
        [global::Microsoft.AspNetCore.Components.InjectAttribute] private Services.IPaqueteService Service { get; set; }
        [global::Microsoft.AspNetCore.Components.InjectAttribute] private BusquedaRequest Busqueda { get; set; }
        [global::Microsoft.AspNetCore.Components.InjectAttribute] private UserCredential Credential { get; set; }
        [global::Microsoft.AspNetCore.Components.InjectAttribute] private AppState AppState { get; set; }
        [global::Microsoft.AspNetCore.Components.InjectAttribute] private IJSRuntime JS { get; set; }
        [global::Microsoft.AspNetCore.Components.InjectAttribute] private NavigationManager NavigationManager { get; set; }
    }
}
#pragma warning restore 1591
