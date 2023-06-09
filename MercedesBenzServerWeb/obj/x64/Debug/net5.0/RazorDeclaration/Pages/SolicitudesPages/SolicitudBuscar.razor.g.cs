// <auto-generated/>
#pragma warning disable 1591
#pragma warning disable 0414
#pragma warning disable 0649
#pragma warning disable 0169

namespace MercedesBenzServerWeb.Pages.SolicitudesPages
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
#nullable restore
#line 4 "D:\Proyectos\Net\MercedesBenzWeb\MercedesBenzServerWeb\Pages\SolicitudesPages\SolicitudBuscar.razor"
using BlazorDateRangePicker;

#line default
#line hidden
#nullable disable
#nullable restore
#line 5 "D:\Proyectos\Net\MercedesBenzWeb\MercedesBenzServerWeb\Pages\SolicitudesPages\SolicitudBuscar.razor"
using System.Text;

#line default
#line hidden
#nullable disable
    [Microsoft.AspNetCore.Components.RouteAttribute("/solicitudBuscar")]
    public partial class SolicitudBuscar : Microsoft.AspNetCore.Components.ComponentBase
    {
        #pragma warning disable 1998
        protected override void BuildRenderTree(Microsoft.AspNetCore.Components.Rendering.RenderTreeBuilder __builder)
        {
        }
        #pragma warning restore 1998
#nullable restore
#line 137 "D:\Proyectos\Net\MercedesBenzWeb\MercedesBenzServerWeb\Pages\SolicitudesPages\SolicitudBuscar.razor"
       
    [CascadingParameter(Name = "RegistrosPagina")]
    protected int registrosPagina { get; set; }

    [Parameter] public EventCallback<string> TextChanged { get; set; }

    BusquedaRequest model = new();
    PaginacionRequest paginacion = new();
    RespuestaPaginada respuesta = null;
    private string mensaje = String.Empty;
    private bool isLoadingData = false;

    private int totalRegistros = 0;
    private int paginaActual = 1;
    private int paginasTotal = 0;

    private ElementReference InputNombre;
    private ElementReference InputPaterno;
    private ElementReference InputMaterno;

    private ElementReference InputFolio;
    private string folio = "";

    DateRangePicker Picker;
    DateTime MinDate { get; set; } = new DateTime(2022, 01, 1);
    DateTime MaxDate { get; set; } = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).AddMonths(1).AddDays(-1);
    private bool FechaCapturada { get; set; } = false;
    private bool ValidarFecha { get; set; } = false;

    protected override async Task OnInitializedAsync()
    {
        paginacion.RegistrosPagina = registrosPagina;
        if (Busqueda.NumeroPagina == 0)
        {
            paginacion.Pagina = paginaActual;
        }
        else
        {
            paginaActual = Busqueda.NumeroPagina;
            Busqueda.NumeroPagina = 0;
            model = Busqueda;

            if (Busqueda.FechaInicial != null)
                FechaCapturada = true;
            await PaginaSeleccionada(paginaActual);
        }
    }

    private async Task OnSubmit()
    {
        bool IsData = false;

        if (!string.IsNullOrEmpty(model.Nombre))
            IsData = true;
        if (!string.IsNullOrEmpty(model.ApellidoPaterno))
            IsData = true;
        if (!string.IsNullOrEmpty(model.ApellidoMaterno))
            IsData = true;
        if (!string.IsNullOrEmpty(model.Folio))
            IsData = true;
        if (FechaCapturada)
        {
            model.FechaInicial = new DateTime(Picker.StartDate.Value.Year, Picker.StartDate.Value.Month, Picker.StartDate.Value.Day, 0, 0, 0);
            model.FechaFinal = new DateTime(Picker.EndDate.Value.Year, Picker.EndDate.Value.Month, Picker.EndDate.Value.Day, 23, 59, 59);
            IsData = true;
        }
        else
        {
            model.FechaInicial = null;
            model.FechaFinal = null;
        }
        if (IsData)
        {
            paginaActual = 1;
            await PaginaSeleccionada(paginaActual);
        }
        else
        {
            mensaje = "Debe ingresar un campo de búsqueda";
            respuesta = null;
        }
        return;
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
        mensaje = "";
        paginacion.Pagina = Pagina;
        (string mensajeResponse, RespuestaPaginada respuestaResponse) = await Service.BusquedasPostAsync(model, paginacion);
        respuesta = respuestaResponse;
        if (respuestaResponse == null)
        {
            if (string.IsNullOrEmpty(mensajeResponse) || mensajeResponse == "True")
                mensaje = "No se encontraron datos con los criterios de búsqueda ingresados";
            else
                mensaje = mensajeResponse;
        }
        else
        {
            totalRegistros = respuesta.TotalRegistros;
            paginasTotal = respuesta.TotalPaginas;
            mensaje = "";
        }
    }

    private void ConsultarSolicitud(int Id)
    {
        Busqueda.Nombre = model.Nombre;
        Busqueda.ApellidoPaterno = model.ApellidoPaterno;
        Busqueda.ApellidoMaterno = model.ApellidoMaterno;
        Busqueda.Folio = model.Folio;
        if (FechaCapturada)
        {
            Busqueda.FechaInicial = model.FechaInicial;
            Busqueda.FechaFinal = model.FechaFinal;
        }
        else
        {
            Busqueda.FechaInicial = null;
            Busqueda.FechaFinal = null;
        }
        Busqueda.NumeroPagina = paginaActual;
        NavigationManager.NavigateTo($"/solicitudDetalle/{Id}/solicitudBuscar/-1");
    }

    private async Task DescargarHuellas(int Id, string Folio)
    {
        mensaje = "Descargando huellas ...";
        (string mensajeResponse, byte[] respuestaResponse) = await ServicePaquete.SolicitudHuellaGetAsync(Id);
        if (respuestaResponse == null)
        {
            mensaje = mensajeResponse;
        }
        else
        {
            var nombreArchivo = $"Huellas{Folio}.zip";
            await JS.InvokeAsync<object>("saveAsFile", nombreArchivo, Convert.ToBase64String(respuestaResponse));
            mensaje = "Descarga de huellas Finalizada";
        }
    }

    private void OnDatePickerOpened()
    {
        ValidarFecha = true;
    }

    private void OnDatePickerClosed()
    {
        if (ValidarFecha)
        {
            if (!string.IsNullOrEmpty(Picker.FormattedRange))
                FechaCapturada = true;
        }
    }

    private void OnRangeSelect(DateRange range)
    {
        model.FechaInicial = new DateTime(range.Start.Year, range.Start.Month, range.Start.Day, 0, 0, 0);
        model.FechaFinal = new DateTime(range.End.Year, range.End.Month, range.End.Day, 23, 59, 59);
    }

    private void OnRangeChange(string value)
    {
        FechaCapturada = false;
        if (value == Picker.FormattedRange)
            FechaCapturada = true;
        ValidarFecha = false;
    }

    private void OnKeyboardNumeric1(KeyboardEventArgs args)
    {
        string[] stringNumerico = { "0", "1", "2", "3", "4", "5", "6", "7", "8", "9" };
        if (stringNumerico.Contains(args.Key))
            folio += args.Key;
        else
        {
            args.Key = string.Empty;
            args.Code = string.Empty;
        }
        model.Folio = folio;
    }

    private async Task OnKeyboardNumeric(KeyboardEventArgs args)
    {
        string[] stringNumerico = { "0", "1", "2", "3", "4", "5", "6", "7", "8", "9", "ArrowUp", "ArrowRight", "ArrowDown", "ArrowLeft", "Backspace", "Delete", "Tab", "Shift", "Dead", "Insert", "Shift", "Home", "End", "PageUp", "PageDown", "NumLock", "CapsLock", "Alt", "AltGraph", "Control", "Meta", "Escape", "F1", "F2", "F3", "F4", "F5", "F6", "F7", "F8", "F9", "F10", "F11", "F12" };
        bool depurar = true;
        foreach (string item in stringNumerico)
        {
            if(item == args.Key)
            {
                depurar = false;
                break;
            }
        }
        if (depurar)
        {
            await JS.InvokeVoidAsync("InputDeleteChar", InputFolio, args.Key);
        }
    }

    private async Task OnKeyboardText(KeyboardEventArgs args, ElementReference input)
    {
        string[] stringNumerico = { "0", "1", "2", "3", "4", "5", "6", "7", "8", "9"};
        bool depurar = false;
        foreach (string item in stringNumerico)
        {
            if (item == args.Key)
            {
                depurar = true;
                break;
            }
        }
        if (depurar)
        {
            await JS.InvokeVoidAsync("InputDeleteChar", input, args.Key);
        }
    }


#line default
#line hidden
#nullable disable
        [global::Microsoft.AspNetCore.Components.InjectAttribute] private Services.IPaqueteService ServicePaquete { get; set; }
        [global::Microsoft.AspNetCore.Components.InjectAttribute] private Services.IBusquedaService Service { get; set; }
        [global::Microsoft.AspNetCore.Components.InjectAttribute] private BusquedaRequest Busqueda { get; set; }
        [global::Microsoft.AspNetCore.Components.InjectAttribute] private UsuarioCredencial Credencial { get; set; }
        [global::Microsoft.AspNetCore.Components.InjectAttribute] private IJSRuntime JS { get; set; }
        [global::Microsoft.AspNetCore.Components.InjectAttribute] private NavigationManager NavigationManager { get; set; }
        [global::Microsoft.AspNetCore.Components.InjectAttribute] private AuthenticationStateProvider AuthenticationStateProvider { get; set; }
        [global::Microsoft.AspNetCore.Components.InjectAttribute] private AppState AppState { get; set; }
    }
}
#pragma warning restore 1591
