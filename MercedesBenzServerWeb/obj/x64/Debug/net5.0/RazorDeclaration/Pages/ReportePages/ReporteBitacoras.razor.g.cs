// <auto-generated/>
#pragma warning disable 1591
#pragma warning disable 0414
#pragma warning disable 0649
#pragma warning disable 0169

namespace MercedesBenzServerWeb.Pages.ReportePages
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
    [Microsoft.AspNetCore.Components.RouteAttribute("/reporteBitacoras")]
    public partial class ReporteBitacoras : Microsoft.AspNetCore.Components.ComponentBase
    {
        #pragma warning disable 1998
        protected override void BuildRenderTree(Microsoft.AspNetCore.Components.Rendering.RenderTreeBuilder __builder)
        {
        }
        #pragma warning restore 1998
#nullable restore
#line 67 "D:\Proyectos\Net\MercedesBenzWeb\MercedesBenzServerWeb\Pages\ReportePages\ReporteBitacoras.razor"
       
    [CascadingParameter(Name = "RegistrosPagina")]
    protected int registrosPagina { get; set; }

    FechaRequest model = new();
    PaginacionRequest paginacion = new();
    RespuestaPaginada respuesta = null;
    private string mensaje = String.Empty;
    private bool isLoadingData = false;

    private bool descargar = false;

    private int totalRegistros = 0;
    private int paginaActual = 1;
    private int paginasTotal = 0;

    protected override void OnInitialized()
    {
        model.FechaInicial = DateTime.Now.AddDays(-(DateTime.Now.Day - 1));
        model.FechaFinal = DateTime.Now;
        paginacion.Pagina = paginaActual;
        paginacion.RegistrosPagina = registrosPagina;
    }

    private async Task OnSubmit()
    {
        bool IsData = false;

        if (!string.IsNullOrEmpty(model.FechaInicial.ToString()))
            IsData = true;
        if (!string.IsNullOrEmpty(model.FechaFinal.ToString()))
            IsData = true;
        if (IsData)
        {
            paginaActual = 1;
            await PaginaSeleccionada(paginaActual);
        }
        else
        {
            mensaje = "Debe ingresar la fecha inicial y la fecha final";
            respuesta = null;
        }
        return;
    }

    private async Task OnDownload()
    {
        mensaje = "Descargando reporte ...";
        model.FechaInicial = new DateTime(model.FechaInicial.Year, model.FechaInicial.Month, model.FechaInicial.Day, 00, 00, 00);
        model.FechaFinal = new DateTime(model.FechaFinal.Year, model.FechaFinal.Month, model.FechaFinal.Day, 23, 59, 59);
        (string mensajeResponse, byte[] respuestaResponse) = await Service.DescargarReportePostAsync("reportes/bitacoraDescargar", model);
        if (respuestaResponse == null)
        {
            mensaje = mensajeResponse;
        }
        else
        {
            var nombreArchivo = $"Reporte_Bitacoras_{DateTime.Now.ToString("yyyy-MM-dd")}.xlsx";
            await JS.InvokeAsync<object>("saveAsFile", nombreArchivo, Convert.ToBase64String(respuestaResponse));
            mensaje = "Descarga Finalizada";
        }
    }

    private async Task PaginaSeleccionada(int Pagina)
    {
        paginaActual = Pagina;
        isLoadingData = true;
        this.StateHasChanged();
        await ConsultarDatos(paginaActual);
        isLoadingData = false;
        this.StateHasChanged();
    }

    private async Task ConsultarDatos(int Pagina = 1)
    {
        descargar = false;
        mensaje = "";
        model.FechaInicial = new DateTime(model.FechaInicial.Year, model.FechaInicial.Month, model.FechaInicial.Day, 00, 00, 00);
        model.FechaFinal = new DateTime(model.FechaFinal.Year, model.FechaFinal.Month, model.FechaFinal.Day, 23, 59, 59);
        paginacion.Pagina = Pagina;
        (string mensajeResponse, RespuestaPaginada respuestaResponse) = await Service.ConsultarReportePostAsync("reportes/bitacora", model, paginacion);
        //respuesta = respuestaResponse;
        respuesta = null;
        if (respuesta == null)
        {
            if (string.IsNullOrEmpty(mensajeResponse) || mensajeResponse == "True")
                mensaje = "No se encontraron datos del período ingresado";
            else
                mensaje = mensajeResponse;
        }
        else
        {
            totalRegistros = respuesta.TotalRegistros;
            paginasTotal = respuesta.TotalPaginas;
            mensaje = "";
            descargar = true;
        }
    }


#line default
#line hidden
#nullable disable
        [global::Microsoft.AspNetCore.Components.InjectAttribute] private Services.IReporteService Service { get; set; }
        [global::Microsoft.AspNetCore.Components.InjectAttribute] private BusquedaRequest Busqueda { get; set; }
        [global::Microsoft.AspNetCore.Components.InjectAttribute] private UsuarioCredencial Credencial { get; set; }
        [global::Microsoft.AspNetCore.Components.InjectAttribute] private IJSRuntime JS { get; set; }
        [global::Microsoft.AspNetCore.Components.InjectAttribute] private NavigationManager NavigationManager { get; set; }
        [global::Microsoft.AspNetCore.Components.InjectAttribute] private AuthenticationStateProvider AuthenticationStateProvider { get; set; }
        [global::Microsoft.AspNetCore.Components.InjectAttribute] private AppState AppState { get; set; }
    }
}
#pragma warning restore 1591
