using BlazorDateRangePicker;
using MercedesBenzModel;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.JSInterop;
using System;
using System.Threading.Tasks;

namespace MercedesBenzServerWeb.Pages.SolicitudesPages
{
    public partial class SolicitudBuscar
    {
        [CascadingParameter(Name = "RegistrosPagina")]
        protected int RegistrosPagina { get; set; }

        [Parameter] public EventCallback<string> TextChanged { get; set; }

        BusquedaRequest model = new();
        PaginacionRequest paginacion = new();
        RespuestaPaginada respuesta = null;
        private string mensaje = "";
        private bool isLoadingData = false;

        private int paginaActual = 1;
        private int paginasTotal = 0;

        private ElementReference InputNombre;
        private ElementReference InputPaterno;
        private ElementReference InputMaterno;

        private ElementReference InputFolio;
        private int pointsEventPaste = 0;

        DateRangePicker Picker;
        DateTime MinDate { get; set; } = new DateTime(2022, 01, 1);
        DateTime MaxDate { get; set; } = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).AddMonths(1).AddDays(-1);
        private bool FechaCapturada { get; set; } = false;
        private bool ValidarFecha { get; set; } = false;

        protected override async Task OnInitializedAsync()
        {
            paginacion.RegistrosPagina = RegistrosPagina;
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
            mensaje = "";
            paginacion.Pagina = pagina;
            (string mensajeResponse, RespuestaPaginada respuestaResponse) = await Service.GetBusquedaAsync(model, paginacion);
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
                paginasTotal = respuesta.TotalPaginas;
                mensaje = "";
            }
        }

        private void ConsultarSolicitud(int id)
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
            NavigationManager.NavigateTo($"/solicitudDetalle/{id}/solicitudBuscar/-1");
        }

        private async Task DescargarHuellas(int id, string folio)
        {
            mensaje = "Descargando huellas ...";
            (string mensajeResponse, byte[] respuestaResponse) = await ServicePaquete.GetSolicitudHuellaAsync(id);
            if (respuestaResponse != null)
            {
                var nombreArchivo = $"Huellas{folio}.zip";
                await JS.InvokeAsync<object>("saveAsFile", nombreArchivo, Convert.ToBase64String(respuestaResponse));
                mensaje = "Descarga de huellas Finalizada";
            }
            else
                mensaje = mensajeResponse;
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

        private async Task OnKeyboardText(KeyboardEventArgs args, ElementReference input)
        {
            if (pointsEventPaste >= 1)
                pointsEventPaste -= 1;
            else
            {
                string[] stringNumerico = { "0", "1", "2", "3", "4", "5", "6", "7", "8", "9" };
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
                    await JS.InvokeVoidAsync("InputDeleteChar", input);
            }
        }

        private async Task OnPasteText(ElementReference input)
        {
            pointsEventPaste = 2;
            await JS.InvokeVoidAsync("InputValidateText", input);
        }

        private async Task OnKeyboardNumeric(KeyboardEventArgs args)
        {
            if (pointsEventPaste >= 1)
                pointsEventPaste -= 1;
            else
            {
                bool depurar = true;
                string[] stringNumerico = { "0", "1", "2", "3", "4", "5", "6", "7", "8", "9", "ArrowUp", "ArrowRight", "ArrowDown", "ArrowLeft", "Backspace", "Delete", "Tab", "Shift", "Dead", "Insert", "Shift", "Home", "End", "PageUp", "PageDown", "NumLock", "CapsLock", "Alt", "AltGraph", "Control", "Meta", "Escape", "F1", "F2", "F3", "F4", "F5", "F6", "F7", "F8", "F9", "F10", "F11", "F12", "Control" };
                foreach (string item in stringNumerico)
                {
                    if (item == args.Key)
                    {
                        depurar = false;
                        break;
                    }
                }
                if (depurar)
                    await JS.InvokeVoidAsync("InputDeleteChar", InputFolio);
            }
        }

        private async Task OnPasteNumeric(ElementReference input)
        {
            pointsEventPaste = 2;
            await JS.InvokeVoidAsync("InputValidateNumbers", input);
        }

    }
}
