// <auto-generated/>
#pragma warning disable 1591
#pragma warning disable 0414
#pragma warning disable 0649
#pragma warning disable 0169

namespace MercedesBenzServerWeb.Pages.AgenciaPages
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
    [Microsoft.AspNetCore.Components.RouteAttribute("/agenciaLista")]
    public partial class AgenciaLista : Microsoft.AspNetCore.Components.ComponentBase
    {
        #pragma warning disable 1998
        protected override void BuildRenderTree(Microsoft.AspNetCore.Components.Rendering.RenderTreeBuilder __builder)
        {
        }
        #pragma warning restore 1998
#nullable restore
#line 87 "D:\Proyectos\Net\MercedesBenzWeb\MercedesBenzServerWeb\Pages\AgenciaPages\AgenciaLista.razor"
       
    [CascadingParameter(Name = "RegistrosPagina")]
    protected int registrosPagina { get; set; }

    PaginacionRequest paginacion = new();
    RespuestaPaginada respuesta = null;
    private string mensaje = String.Empty;

    private bool isLoadingData = true;

    private int id = 0;
    private string NombreAgencia = "";
    private bool activarAgencia = false;
    private TipoAccion tipoAccion { get; set; }

    private int totalRegistros = 0;
    private int paginaActual = 1;
    private int paginasTotal = 0;

    private bool popupOpen { get; set; } = false;
    private string mensajePopup = "";

    private bool popupOpenComent { get; set; } = false;
    private string mensajePopupComent = "";

    protected override void OnInitialized()
    {
        paginacion.Pagina = paginaActual;
        paginacion.RegistrosPagina = registrosPagina;
    }

    protected async override Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            await Task.Run(async () =>
            {
                await ConsultarDatos();
                isLoadingData = false;
                await InvokeAsync(StateHasChanged);
            });
        }
    }

    private async Task PaginaSeleccionada(int pagina)
    {
        paginaActual = pagina;
        isLoadingData = true;
        this.StateHasChanged();
        await ConsultarDatos(paginaActual);
        isLoadingData = false;
        this.StateHasChanged();
    }

    private async Task ConsultarDatos(int pagina = 1)
    {
        paginacion.Pagina = pagina;
        (string mensajeResponse, RespuestaPaginada respuestaResponse) = await Service.GetAllAsync(paginacion);
        if (respuestaResponse == null)
        {
            if (string.IsNullOrEmpty(mensajeResponse))
                mensaje = "No se encontraron registros";
            else
                mensaje = mensajeResponse;
        }
        else
        {
            respuesta = respuestaResponse;
            totalRegistros = respuesta.TotalRegistros;
            paginasTotal = respuesta.TotalPaginas;
            mensaje = "";
        }
    }

    private void OnEditar(int selectId, string nombre)
    {
        id = selectId;
        NombreAgencia = nombre;
        mensajePopup = $"¿Estas seguro de editar la agencia {nombre}?";
        tipoAccion = TipoAccion.Editar;
        popupOpen = true;
    }

    private void OnCambiarEstatus(bool estatus, int selectId, string nombre)
    {
        activarAgencia = estatus;
        id = selectId;
        NombreAgencia = nombre;
        string mensajeEstatus = "desactivar";
        if (activarAgencia)
            mensajeEstatus = "activar";

        mensajePopup = $"¿Estas seguro de {mensajeEstatus} la agencia {nombre}?";
        tipoAccion = TipoAccion.Eliminar;
        popupOpen = true;
    }

    private void OnPopupClose(bool respuesta)
    {
        popupOpen = false;
        if (respuesta)
        {
            if (tipoAccion == TipoAccion.Editar)
            {
                NavigationManager.NavigateTo($"agenciaEditar/{id}");
            }
            else
            {
                mensajePopupComent = "Agregar comentario";
                popupOpenComent = true;
            }
        }
    }

    private async Task OnPopupComentClose(string respuesta)
    {
        popupOpenComent = false;
        if (!string.IsNullOrEmpty(respuesta))
        {
            mensaje = "";
            AgenciaEstatusRequest request = new()
            {
                Activar = activarAgencia,
                AgenciaId = id,
                Motivo = respuesta,
                UsuarioIdActualizo = Credential.UsuarioId,
            };
            (string mensajeResponse, bool resultado) = await Service.UpdateEstatuAsync(request);
            if (resultado)
                await PaginaSeleccionada(paginaActual);
            else
                mensaje = mensajeResponse;
        }
    }


#line default
#line hidden
#nullable disable
        [global::Microsoft.AspNetCore.Components.InjectAttribute] private Services.IAgenciaService Service { get; set; }
        [global::Microsoft.AspNetCore.Components.InjectAttribute] private BusquedaRequest Busqueda { get; set; }
        [global::Microsoft.AspNetCore.Components.InjectAttribute] private UserCredential Credential { get; set; }
        [global::Microsoft.AspNetCore.Components.InjectAttribute] private AppState AppState { get; set; }
        [global::Microsoft.AspNetCore.Components.InjectAttribute] private IJSRuntime JS { get; set; }
        [global::Microsoft.AspNetCore.Components.InjectAttribute] private NavigationManager NavigationManager { get; set; }
    }
}
#pragma warning restore 1591
