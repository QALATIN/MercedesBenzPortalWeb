using MercedesBenzLibrary;
using MercedesBenzModel;
using MercedesBenzServerWeb.Helpers;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace MercedesBenzServerWeb.Pages.SolicitudesPages
{
    public partial class SolicitudDetalle
    {

        [Parameter] public int Id { get; set; }
        [Parameter] public string UrlOrigen { get; set; } = "solicitudAdmin";
        [Parameter] public int Estatus { get; set; } = -1;

        private string mensaje = "";

        private bool recargarPagina = true;
        private bool renderizarMapa = true;
        private bool existeFotoCamara = false;

        private bool isLoadingModel = true;
        private SolicitudFicha model = null;

        private string resultadoIBMS = "";

        private const string _TituloListaNegra = "Lista negra";
        private bool agregarListaNegra = false;
        private bool mostrarListaNegra = false;

        private bool agregarResolucion = false;
        private int tipoResolucionId = 0;
        private bool mostrarResolucion = false;

        private const string _TituloScoreIBMS = "Score IBMS";

        private const string _TituloIdentificacion = "Semáforo de identificación";
        private bool isLoadingIdentificacion = true;
        private SolicitudIdentificacion validacionIdentificacion = null;
        private bool mostrarIdentificacion = false;

        private const string _TituloAvisoPrivacidad = "Aviso de privacidad";
        private bool isLoadingPrivacidad = true;
        private SolicitudAvisoPrivacidad validacionPrivacidad = null;
        private bool mostrarPrivacidad = false;

        private const string _TituloComparacionFacial = "Semáforo de verificación facial";
        private bool isLoadingComparacionFacial = true;
        private SolicitudComparacionFacial validacionComparacionFacial = null;
        private bool mostrarComparacionFacial = false;

        private const string _TituloHuellas = "Huellas";
        private bool isLoadingComparacionHuellas = true;
        private SolicitudComparacionHuellas validacionComparacionHuellas = null;
        private bool mostrarHuellas = false;

        private const string _TituloIne = "Portal del INE";
        private bool isLoadingIne = true;
        private IneResponse validacionIne = null;
        private bool mostrarIne = false;

        private const string _TituloCorreo = "Semáforo de correo electrónico";
        private bool isLoadingCorreo = true;
        private CorreoResponse validacionCorreo = null;
        private bool mostrarCorreo = false;

        private const string _TituloTelefono = "Semáforo de número telefónico";
        private bool isLoadingTelefono = true;
        private TelefonoResponse validacionTelefono = null;
        private bool mostrarTelefono = false;

        private const string _TituloCurp = "Semáforo de CURP";
        private bool isLoadingCurp = true;
        private CurpResponse validacionCurp = null;
        private bool mostrarCurp = false;

        private const string _TituloListas = "Listas de interes";
        private bool isLoadingListas = true;
        private ListaResponse validacionListas = null;
        private bool mostrarListas = false;

        private const string _TituloComprobanteIngresos = "Semáforo de comprobante de ingresos";
        private const string _tipoDocumentoIngresos = "1";
        private bool isLoadingComprobanteIngresos = true;
        private ComprobanteResponse validacionComprobanteIngresos = null;
        private bool mostrarComprobanteIngresos = false;
        private SolicitudDocumento documentoComprobanteIngresos = null;
        private int _documentoIdComprobanteIngresos = -1;
        private string formatoComprobanteIngresos = "";

        private const string _TituloComprobanteDomicilio = "Semáforo de comprobante de domicilio";
        private const string _tipoDocumentoDomicilio = "2";
        private bool isLoadingComprobanteDomicilio = true;
        private ComprobanteResponse validacionComprobanteDomicilio = null;
        private bool mostrarComprobanteDomicilio = false;
        private SolicitudDocumento documentoComprobanteDomicilio = null;
        private int _documentoIdComprobanteDomicilio = -1;
        private string formatoComprobanteDomicilio = "";

        private const string _TituloComprobanteBancario = "Semáforo de comprobante bancario";
        private const string _tipoDocumentoBancario = "3";
        private bool isLoadingComprobanteBancario = true;
        private ComprobanteResponse validacionComprobanteBancario = null;
        private bool mostrarComprobanteBancario = false;
        private SolicitudDocumento documentoComprobanteBancario = null;
        private int _documentoIdComprobanteBancario = -1;
        private string formatoComprobanteBancario = "";

        private const string _tituloResolucion = "Pre aprobación";
        private const string _tituloOrigen = "Co-acreditado / Aval";

        private bool popupOpen = false;
        private string mensajePopup = "";

        private bool popupOpenComent = false;
        private string mensajePopupComent = "";

        private readonly int origenId = 1;
        private readonly int tipoLogIdError = 1;

        ElementReference divProgressScore;

        protected override async Task OnInitializedAsync()
        {
            model = null;
            if (string.IsNullOrEmpty(Credential.NombreUsuario))
            {
                var credential = await LocalStorage.GetCredentialAsync("credential");
                if (credential != null)
                    Credential = credential;
            }

        }

        protected async override Task OnAfterRenderAsync(bool firstRender)
        {
            if (recargarPagina)
            {
                recargarPagina = false;
                await Task.Run(async () =>
                {
                    await ConsultarDatos(Id);
                    if (UrlOrigen == "solicitudAdmin")
                        AppState.RefreshNotify();
                    isLoadingModel = false;
                    await InvokeAsync(StateHasChanged);
                });
            }
            else
            {
                if (model != null && renderizarMapa)
                {
                    renderizarMapa = false;
                    await JS.InvokeVoidAsync("MostrarScore", divProgressScore, 100, model.Validaciones.ResultadoIBMS, 60, "#EAEAEA", model.Validaciones.SemaforoIBMS, "%", true);
                    //string html = await JS.InvokeAsync<string>("initMap", model.DireccionCompleta);
                }
            }
        }

        private async Task ConsultarDatos(int id)
        {
            var request = new SolicitudRequest() { SolicitanteId = id, UsuarioId = Credential.UsuarioId };
            (string mensajeResponse, SolicitudFicha respuestaResponse) = await Service.GetSolicitudFichaAsync(request);
            model = respuestaResponse;
            if (model == null)
            {
                if (string.IsNullOrEmpty(mensajeResponse) || mensajeResponse == "True")
                    mensaje = "No se encontro la solicitud";
                else
                    mensaje = mensajeResponse;
            }
            else
            {
                mensaje = "";
                resultadoIBMS = model.Validaciones.ResultadoIBMS;
                ValidarIBMS(false);

                IniciarConsultaGeoreferencia();
                if (model.ExisteAvisoPrivacidad)
                    IniciarSemaforoPrivacidad(true);
                IniciarSemaforoIdentificacion(true);
                IniciarSemaforoComparacionFacial(true);
                IniciarSemaforoHuellas(true);

                IniciarSemaforoCurp(true);  
                IniciarSemaforoCorreo(true);  
                IniciarSemaforoTelefono(true);
                IniciarSemaforoListasInteres(true);
                if (model.Validaciones.ValidarIne)
                    IniciarSemaforoIne(true, false);
                var validarDocumentosCargado = Task.Run(async () => {
                    await ConsultarComprobantesCargados();
                });
            }
        }

        private async Task ConsultarAvisoPrivacidad()
        {
            var request = new SolicitudValidacionRequest() { SolicitanteId = model.SolicitanteId, Validar = "0" };
            (string mensajeResponse, SolicitudAvisoPrivacidad privacidadResponse, string objectSerialize) = await Service.GetSolicitudAvisoPrivacidadAsync(request);
            if (!string.IsNullOrEmpty(mensajeResponse))
            {
                var bitacora = new BitacoraRequest() { OrigenId = origenId, TipoLogId = tipoLogIdError, UsuarioId = Credential.UsuarioId, Mensaje = mensajeResponse, Referencia = "ValidacionAvisoPrivacidad|" + objectSerialize };
                await ServiceGeneral.AddBitacoraAsync(bitacora);
            }
            validacionPrivacidad = privacidadResponse;
        }

        private async Task ConsultarIdentificacion()
        {
            var request = new SolicitudValidacionRequest() { SolicitanteId = model.SolicitanteId, Validar = "0" };
            (string mensajeResponse, SolicitudIdentificacion identificacionResponse, string objectSerialize) = await Service.GetSolicitudIdentificacionAsync(request);
            if (!string.IsNullOrEmpty(mensajeResponse))
            {
                var bitacora = new BitacoraRequest() { OrigenId = origenId, TipoLogId = tipoLogIdError, UsuarioId = Credential.UsuarioId, Mensaje = mensajeResponse, Referencia = "ValidacionIdentificacion|" + objectSerialize };
                await ServiceGeneral.AddBitacoraAsync(bitacora);
            }
            else
            {
                if (identificacionResponse != null)
                {
                    model.Validaciones.SemaforoIdentificacion = identificacionResponse.Resultado.Semaforo;
                    model.Validaciones.ResultadoIdentificacion = identificacionResponse.Resultado.Mensaje;
                    ValidarIBMS(true);
                }
            }
            validacionIdentificacion = identificacionResponse;
        }

        private async Task ConsultarComparacionFacial()
        {
            existeFotoCamara = false;
            var request = new SolicitudValidacionRequest() { SolicitanteId = model.SolicitanteId, Validar = "0" };
            (string mensajeResponse, SolicitudComparacionFacial facialResponse, string objectSerialize) = await Service.GetSolicitudComparacionFacialAsync(request);
            if (!string.IsNullOrEmpty(mensajeResponse))
            {
                var bitacora = new BitacoraRequest() { OrigenId = origenId, TipoLogId = tipoLogIdError, UsuarioId = Credential.UsuarioId, Mensaje = mensajeResponse, Referencia = "ValidacionComparacionFacial|" + objectSerialize };
                await ServiceGeneral.AddBitacoraAsync(bitacora);
            }
            else
            {
                if (facialResponse != null)
                {
                    model.Validaciones.SemaforoFacial = facialResponse.Resultado.Semaforo;
                    model.Validaciones.ResultadoFacial = facialResponse.Resultado.Mensaje;
                    foreach (var item in facialResponse.Fotos)
                    {
                        if (item.FotoOrigenId == 1)
                        {
                            existeFotoCamara = true;
                        }
                    }
                    ValidarIBMS(true);
                }
            }
            validacionComparacionFacial = facialResponse;
        }

        private async Task ConsultarComparacionHuellas()
        {
            var request = new SolicitudValidacionRequest() { SolicitanteId = model.SolicitanteId, Validar = "0" };
            (string mensajeResponse, SolicitudComparacionHuellas huellasResponse, string objectSerialize) = await Service.GetSolicitudComparacionHuellasAsync(request);
            if (!string.IsNullOrEmpty(mensajeResponse))
            {
                var bitacora = new BitacoraRequest() { OrigenId = origenId, TipoLogId = tipoLogIdError, UsuarioId = Credential.UsuarioId, Mensaje = mensajeResponse, Referencia = "ValidacionComparacionHuellas|" + objectSerialize };
                await ServiceGeneral.AddBitacoraAsync(bitacora);
            }
            else
            {
                if (huellasResponse != null)
                {
                    model.Validaciones.SemaforoAfis = huellasResponse.Resultado.Semaforo;
                    model.Validaciones.ResultadoAfis = huellasResponse.Resultado.Mensaje;
                    ValidarIBMS(true);
                }
            }
            validacionComparacionHuellas = huellasResponse;
        }

        private async Task ConsultarGeoreferencia()
        {
            var request = new GeoreferenciaRequest()
            {
                ValidacionId = model.Validaciones.ValidacionId,
                ResultadoGeoreferencia = "0"
            };
            string objectSerialize = JsonConvert.SerializeObject(request);
            (string mensajeResponse, GeoreferenciaResponse georeferenciaResponse, ValidationResult validationResult) validacion;
            validacion = await ServiceValidacion.GetValidacionGeoreferenciaAsync(objectSerialize);
            if (!string.IsNullOrEmpty(validacion.mensajeResponse))
            {
                var bitacora = new BitacoraRequest()
                {
                    OrigenId = origenId,
                    TipoLogId = tipoLogIdError,
                    UsuarioId = Credential.UsuarioId,
                    Mensaje = validacion.mensajeResponse,
                    Referencia = $"ValidacionGeoreferencia|{model.SolicitanteId}|" + objectSerialize
                };
                await ServiceGeneral.AddBitacoraAsync(bitacora);
            }

            if (validacion.georeferenciaResponse != null)
            {
                model.HtmlGeoReferencia = validacion.georeferenciaResponse.Map;
            }
        }

        private async Task ConsultarCurp()
        {
            var request = new CurpRequest()
            {
                ValidacionId = model.Validaciones.ValidacionId,
                ResultadoCurp = "0"
            };
            string objectSerialize = JsonConvert.SerializeObject(request);
            (string mensajeResponse, CurpResponse curpResponse, ValidationResult validationResult) validacion;
            validacion = await ServiceValidacion.GetValidacionCurpAsync(objectSerialize);
            if (!string.IsNullOrEmpty(validacion.mensajeResponse))
            {
                var bitacora = new BitacoraRequest()
                {
                    OrigenId = origenId,
                    TipoLogId = tipoLogIdError,
                    UsuarioId = Credential.UsuarioId,
                    Mensaje = validacion.mensajeResponse,
                    Referencia = $"ValidacionCurp|{model.SolicitanteId}|" + objectSerialize
                };
                await ServiceGeneral.AddBitacoraAsync(bitacora);

                if (validacion.validationResult != null)
                {
                    await ServiceGeneral.UpdateValidacionAsync(validacion.validationResult);
                    validacion = await ServiceValidacion.GetValidacionCurpAsync(objectSerialize);
                    if (!string.IsNullOrEmpty(validacion.mensajeResponse))
                    {
                        bitacora = new BitacoraRequest()
                        {
                            OrigenId = origenId,
                            TipoLogId = tipoLogIdError,
                            UsuarioId = Credential.UsuarioId,
                            Mensaje = validacion.mensajeResponse,
                            Referencia = $"ValidacionCurp|{model.SolicitanteId}|" + objectSerialize
                        };
                        await ServiceGeneral.AddBitacoraAsync(bitacora);
                    }
                }
            }

            if (validacion.curpResponse != null)
            {
                model.Validaciones.SemaforoCurp = validacion.curpResponse.semaforo;
                ValidarIBMS(true);
            }

            validacionCurp = validacion.curpResponse;
        }

        private async Task ConsultarCorreo()
        {
            var request = new CorreoRequest()
            {
                ValidacionId = model.Validaciones.ValidacionId,
                ResultadoCorreo = "0"
            };
            string objectSerialize = JsonConvert.SerializeObject(request);
            (string mensajeResponse, CorreoResponse correoResponse, ValidationResult validationResult) validacion;
            validacion = await ServiceValidacion.GetValidacionCorreoAsync(objectSerialize);
            if (!string.IsNullOrEmpty(validacion.mensajeResponse))
            {
                var bitacora = new BitacoraRequest() { OrigenId = origenId, TipoLogId = tipoLogIdError, UsuarioId = Credential.UsuarioId, Mensaje = validacion.mensajeResponse, Referencia = $"ValidacionCorreo|{model.SolicitanteId}|" + objectSerialize };
                await ServiceGeneral.AddBitacoraAsync(bitacora);

                if (validacion.validationResult != null)
                {
                    await ServiceGeneral.UpdateValidacionAsync(validacion.validationResult);
                    validacion = await ServiceValidacion.GetValidacionCorreoAsync(objectSerialize);
                    if (!string.IsNullOrEmpty(validacion.mensajeResponse))
                    {
                        bitacora = new BitacoraRequest() { OrigenId = origenId, TipoLogId = tipoLogIdError, UsuarioId = Credential.UsuarioId, Mensaje = validacion.mensajeResponse, Referencia = $"ValidacionCorreo|{model.SolicitanteId}|" + objectSerialize };
                        await ServiceGeneral.AddBitacoraAsync(bitacora);
                    }
                }
            }

            if (validacion.correoResponse != null)
            {
                model.Validaciones.SemaforoCorreo = validacion.correoResponse.semaforo;
                model.Validaciones.ScoreCorreo = validacion.correoResponse.datos_validacion.score;
                ValidarIBMS(true);
            }
            validacionCorreo = validacion.correoResponse;
        }

        private async Task ConsultarTelefono()
        {
            var request = new TelefonoRequest()
            {
                ValidacionId = model.Validaciones.ValidacionId,
                ResultadoTelefono = "0"
            };
            string objectSerialize = JsonConvert.SerializeObject(request);
            (string mensajeResponse, TelefonoResponse telefonoResponse, ValidationResult validationResult) validacion;
            validacion = await ServiceValidacion.GetValidacionTelefonoAsync(objectSerialize);
            if (!string.IsNullOrEmpty(validacion.mensajeResponse))
            {
                var bitacora = new BitacoraRequest() { OrigenId = origenId, TipoLogId = tipoLogIdError, UsuarioId = Credential.UsuarioId, Mensaje = validacion.mensajeResponse, Referencia = $"ValidacionTelefono|{model.SolicitanteId}|" + objectSerialize };
                await ServiceGeneral.AddBitacoraAsync(bitacora);

                if (validacion.validationResult != null)
                {
                    await ServiceGeneral.UpdateValidacionAsync(validacion.validationResult);
                    validacion = await ServiceValidacion.GetValidacionTelefonoAsync(objectSerialize);
                    if (!string.IsNullOrEmpty(validacion.mensajeResponse))
                    {
                        bitacora = new BitacoraRequest() { OrigenId = origenId, TipoLogId = tipoLogIdError, UsuarioId = Credential.UsuarioId, Mensaje = validacion.mensajeResponse, Referencia = $"ValidacionTelefono|{model.SolicitanteId}|" + objectSerialize };
                        await ServiceGeneral.AddBitacoraAsync(bitacora);
                    }
                }
            }

            if (validacion.telefonoResponse != null)
            {
                model.Validaciones.SemaforoTelefono = validacion.telefonoResponse.semaforo;
                ValidarIBMS(true);
            }
            validacionTelefono = validacion.telefonoResponse;
        }

        private async Task ConsultarListasInteres()
        {
            var request = new ListaRequest()
            {
                ValidacionId = model.Validaciones.ValidacionId,
                ResultadoListaAml = "0"
            };
            string objectSerialize = JsonConvert.SerializeObject(request);
            (string mensajeResponse, ListaResponse listaResponse, ValidationResult validationResult) validacion;
            validacion = await ServiceValidacion.GetValidacionListasAsync(objectSerialize);
            if (!string.IsNullOrEmpty(validacion.mensajeResponse))
            {
                var bitacora = new BitacoraRequest() { OrigenId = origenId, TipoLogId = tipoLogIdError, UsuarioId = Credential.UsuarioId, Mensaje = validacion.mensajeResponse, Referencia = $"ValidacionListasInteres|{model.SolicitanteId}|" + objectSerialize };
                await ServiceGeneral.AddBitacoraAsync(bitacora);

                if (validacion.validationResult != null)
                {
                    await ServiceGeneral.UpdateValidacionAsync(validacion.validationResult);
                    validacion = await ServiceValidacion.GetValidacionListasAsync(objectSerialize);
                    if (!string.IsNullOrEmpty(validacion.mensajeResponse))
                    {
                        bitacora = new BitacoraRequest() { OrigenId = origenId, TipoLogId = tipoLogIdError, UsuarioId = Credential.UsuarioId, Mensaje = validacion.mensajeResponse, Referencia = $"ValidacionListasInteres|{model.SolicitanteId}|" + objectSerialize };
                        await ServiceGeneral.AddBitacoraAsync(bitacora);
                    }
                }
            }

            if (validacion.listaResponse != null)
            {
                model.Validaciones.SemaforoListaAml = validacion.listaResponse.semaforo;
                ValidarIBMS(true);
            }
            validacionListas = validacion.listaResponse;
        }

        private async Task ConsultarIne()
        {
            var request = new IneRequest()
            {
                ValidacionId = model.Validaciones.ValidacionId,
                ResultadoIne = "0"
            };
            string objectSerialize = JsonConvert.SerializeObject(request);
            (string mensajeResponse, IneResponse ineResponse, ValidationResult validationResult) validacion;
            validacion = await ServiceValidacion.GetValidacionIneAsync(objectSerialize);
            if (!string.IsNullOrEmpty(validacion.mensajeResponse))
            {
                var bitacora = new BitacoraRequest() { OrigenId = origenId, TipoLogId = tipoLogIdError, UsuarioId = Credential.UsuarioId, Mensaje = validacion.mensajeResponse, Referencia = $"ValidacionIne|{model.SolicitanteId}|" + objectSerialize };
                await ServiceGeneral.AddBitacoraAsync(bitacora);

                if (validacion.validationResult != null)
                {
                    await ServiceGeneral.UpdateValidacionAsync(validacion.validationResult);
                    validacion = await ServiceValidacion.GetValidacionIneAsync(objectSerialize);
                    if (!string.IsNullOrEmpty(validacion.mensajeResponse))
                    {
                        bitacora = new BitacoraRequest() { OrigenId = origenId, TipoLogId = tipoLogIdError, UsuarioId = Credential.UsuarioId, Mensaje = validacion.mensajeResponse, Referencia = $"ValidacionIne|{model.SolicitanteId}|" + objectSerialize };
                        await ServiceGeneral.AddBitacoraAsync(bitacora);
                    }
                }
            }

            if (validacion.ineResponse != null)
            {
                model.Validaciones.SemaforoIne = validacion.ineResponse.semaforo;
                ValidarIBMS(true);
            }
            validacionIne = validacion.ineResponse;
        }

        private async Task ConsultarComprobantesCargados()
        {
            if (model.DocumentosCargados != null)
            {
                foreach (var item in model.DocumentosCargados)
                {
                    switch (item.TipoDocumentoId)
                    {
                        case "1":
                            _documentoIdComprobanteIngresos = item.DocumentoId;
                            formatoComprobanteIngresos = item.FormatoDocumento;
                            isLoadingComprobanteIngresos = true;
                            break;

                        case "2":
                            _documentoIdComprobanteDomicilio = item.DocumentoId;
                            formatoComprobanteDomicilio = item.FormatoDocumento;
                            isLoadingComprobanteDomicilio = true;
                            break;

                        case "3":
                            _documentoIdComprobanteBancario = item.DocumentoId;
                            formatoComprobanteBancario = item.FormatoDocumento;
                            isLoadingComprobanteBancario = true;
                            break;
                    }
                }

                if (_documentoIdComprobanteDomicilio == -1)
                    _documentoIdComprobanteDomicilio = 0;
                if (_documentoIdComprobanteBancario == -1)
                    _documentoIdComprobanteBancario = 0;
                if (_documentoIdComprobanteIngresos == -1)
                    _documentoIdComprobanteIngresos = 0;

                if (_documentoIdComprobanteDomicilio > 0)
                {
                    documentoComprobanteDomicilio = await ConsultarDocumentoCargado(_tipoDocumentoDomicilio, _documentoIdComprobanteDomicilio);
                    await ConsultarDocumentoDomicilio();
                    await InvokeAsync(StateHasChanged);
                }
                if (_documentoIdComprobanteBancario > 0)
                {
                    documentoComprobanteBancario = await ConsultarDocumentoCargado(_tipoDocumentoBancario, _documentoIdComprobanteBancario);
                    await ConsultarDocumentoBancario();
                    await InvokeAsync(StateHasChanged);
                }
                if (_documentoIdComprobanteIngresos > 0)
                {
                    documentoComprobanteIngresos = await ConsultarDocumentoCargado(_tipoDocumentoIngresos, _documentoIdComprobanteIngresos);
                    await ConsultarDocumentoIngresos();
                    await InvokeAsync(StateHasChanged);
                }
            }
        }

        private async Task<SolicitudDocumento> ConsultarDocumentoCargado(string tipoDocumentoId, int documentoId)
        {
            var request = new SolicitudDocumentoRequest() { SolicitanteId = model.SolicitanteId, DocumentoId = documentoId };
            (string mensajeResponse, SolicitudDocumento documentoResponse, string objectSerialize) = await Service.GetSolicitudDocumentosAsync(request);
            if (!string.IsNullOrEmpty(mensajeResponse))
            {
                var bitacora = new BitacoraRequest() { OrigenId = origenId, TipoLogId = tipoLogIdError, UsuarioId = Credential.UsuarioId, Mensaje = mensajeResponse, Referencia = $"ConsultarDocumentoCargado{tipoDocumentoId}|{model.SolicitanteId}|" + objectSerialize };
                await ServiceGeneral.AddBitacoraAsync(bitacora);
            }
            return documentoResponse;
        }

        private async Task ConsultarDocumentoDomicilio()
        {
            if (documentoComprobanteDomicilio != null)
            {
                validacionComprobanteDomicilio = await ConsultarComprobante(_tipoDocumentoDomicilio);
                if (validacionComprobanteDomicilio != null)
                {
                    if (validacionComprobanteDomicilio.error == 0)
                    {
                        model.Validaciones.SemaforoComprobanteDomicilio = validacionComprobanteDomicilio.semaforo;
                        ValidarIBMS(true);
                    }
                    else
                        validacionComprobanteDomicilio = null;
                }
                isLoadingComprobanteDomicilio = false;
            }
        }

        private async Task ConsultarDocumentoBancario()
        {
            if (documentoComprobanteBancario != null)
            {
                validacionComprobanteBancario = await ConsultarComprobante(_tipoDocumentoBancario);
                if (validacionComprobanteBancario != null)
                {
                    if (validacionComprobanteBancario.error == 0)
                    {
                        model.Validaciones.SemaforoComprobanteBancario = validacionComprobanteBancario.semaforo;
                        ValidarIBMS(true);
                    }
                    else
                        validacionComprobanteBancario = null;
                }
                isLoadingComprobanteBancario = false;
            }
        }

        private async Task ConsultarDocumentoIngresos()
        {
            if (documentoComprobanteIngresos != null)
            {
                validacionComprobanteIngresos = await ConsultarComprobante(_tipoDocumentoIngresos);
                if (validacionComprobanteIngresos != null)
                {
                    if (validacionComprobanteIngresos.error == 0)
                    {
                        model.Validaciones.SemaforoComprobanteIngresos = validacionComprobanteIngresos.semaforo;
                        ValidarIBMS(true);
                    }
                    else
                        validacionComprobanteIngresos = null;
                }
                isLoadingComprobanteIngresos = false;
            }
        }

        private async Task<ComprobanteResponse> ConsultarComprobante(string tipoComprobante)
        {
            var request = new ComprobanteRequest() 
            {
                Validacion_Id = model.Validaciones.ValidacionId,
                Revalidar = "0",
                Tipo_Comprobante = tipoComprobante
            };
            string objectSerialize = JsonConvert.SerializeObject(request);
            (string mensajeResponse, ComprobanteResponse comprobanteResponse, ValidationResult validationResult) validacion;
            validacion = await ServiceValidacion.GetValidacionComprobanteAsync(objectSerialize);
            if (!string.IsNullOrEmpty(validacion.mensajeResponse))
            {
                var bitacora = new BitacoraRequest() { OrigenId = origenId, TipoLogId = tipoLogIdError, UsuarioId = Credential.UsuarioId, Mensaje = validacion.mensajeResponse, Referencia = $"ValidacionDocumento{tipoComprobante}|{model.SolicitanteId}|" + objectSerialize };
                await ServiceGeneral.AddBitacoraAsync(bitacora);

                if (validacion.validationResult != null)
                {
                    TipoSemaforo tipoValidacion = tipoComprobante switch
                    {
                        _tipoDocumentoIngresos => TipoSemaforo.ComprobanteIngresos,
                        _tipoDocumentoDomicilio => TipoSemaforo.ComprobanteDomicilio,
                        _tipoDocumentoBancario => TipoSemaforo.ComprobanteBancario,
                        _ => TipoSemaforo.Otro
                    };
                    validacion.validationResult.TipoValidacion = tipoValidacion;
                    await ServiceGeneral.UpdateValidacionAsync(validacion.validationResult);
                    validacion = await ServiceValidacion.GetValidacionComprobanteAsync(objectSerialize);
                    if (!string.IsNullOrEmpty(validacion.mensajeResponse))
                    {
                        bitacora = new BitacoraRequest() { OrigenId = origenId, TipoLogId = tipoLogIdError, UsuarioId = Credential.UsuarioId, Mensaje = validacion.mensajeResponse, Referencia = $"ValidacionDocumento{tipoComprobante}|{model.SolicitanteId}|" + objectSerialize };
                        await ServiceGeneral.AddBitacoraAsync(bitacora);
                    }
                }
            }
            return validacion.comprobanteResponse;
        }

        private async Task ConsultarValidaciones(string tituloValidacion)
        {
            var request = new SolicitudValidacionRequest() { SolicitanteId = Id };
            (string mensajeResponse, SolicitudValidacion respuestaResponse) = await Service.GetSolicitudValidacionesAsync(request);
            if (string.IsNullOrEmpty(mensajeResponse))
            {
                switch (tituloValidacion)
                {
                    case _TituloIne:
                        model.Validaciones.SemaforoIne = respuestaResponse.SemaforoIne;
                        model.Validaciones.ResultadoIne = respuestaResponse.ResultadoIne;
                        model.Validaciones.FechaIne = respuestaResponse.FechaIne;
                        break;
                    case _TituloComprobanteDomicilio:
                        model.Validaciones.SemaforoComprobanteDomicilio = respuestaResponse.SemaforoComprobanteDomicilio;
                        model.Validaciones.ResultadoComprobanteDomicilio = respuestaResponse.ResultadoComprobanteDomicilio;
                        model.Validaciones.FechaComprobanteDomicilio = respuestaResponse.FechaComprobanteDomicilio;
                        break;
                    case _TituloComprobanteBancario:
                        model.Validaciones.SemaforoComprobanteBancario = respuestaResponse.SemaforoComprobanteBancario;
                        model.Validaciones.ResultadoComprobanteBancario = respuestaResponse.ResultadoComprobanteBancario;
                        model.Validaciones.FechaComprobanteBancario = respuestaResponse.FechaComprobanteBancario;
                        break;
                    case _TituloComprobanteIngresos:
                        model.Validaciones.SemaforoComprobanteIngresos = respuestaResponse.SemaforoComprobanteIngresos;
                        model.Validaciones.ResultadoComprobanteIngresos = respuestaResponse.ResultadoComprobanteIngresos;
                        model.Validaciones.FechaComprobanteIngresos = respuestaResponse.FechaComprobanteIngresos;
                        break;
                }
            }
        }

        private void ValidarIBMS(bool animarScore)
        {
            if (string.IsNullOrEmpty(resultadoIBMS))
            {
                (float score, string semaforo) = Validaciones.CalcularScoreGeneral(model.Validaciones);
                model.Validaciones.ResultadoIBMS = score.ToString("0");
                model.Validaciones.SemaforoIBMS = semaforo;
                if (animarScore)
                    Task.Run(async () => await JS.InvokeVoidAsync("mostrarScore", divProgressScore, 100, model.Validaciones.ResultadoIBMS, 60, "#EAEAEA", model.Validaciones.SemaforoIBMS, "%", false));
            }
        }

        private void OnRegresar()
        {
            if (Estatus >= 0)
            {
                NavigationManager.NavigateTo($"/{UrlOrigen}/{Estatus}");
            }
            else
            {
                NavigationManager.NavigateTo("/" + UrlOrigen);
            }
        }

        private void OnListaNegra()
        {
            agregarListaNegra = true;
            mensajePopup = "¿Estas seguro de agregar a lista negra?";
            popupOpen = true;
        }

        private void OnMostrarIdentificacion()
        {
            mostrarIdentificacion = !mostrarIdentificacion;
            if (mostrarIdentificacion)
            {
                isLoadingIdentificacion = true;
                Task.Run(async () =>
                {
                    await Task.Delay(500);
                    isLoadingIdentificacion = false;
                    await InvokeAsync(StateHasChanged);
                });
            }
        }

        private void OnMostrarHuellas()
        {
            mostrarHuellas = !mostrarHuellas;
            if (mostrarHuellas)
            {
                isLoadingComparacionHuellas = true;
                Task.Run(async () =>
                {
                    await Task.Delay(500);
                    isLoadingComparacionHuellas = false;
                    await InvokeAsync(StateHasChanged);
                });
            }
        }

        private void OnMostrarComparacionFacial()
        {
            mostrarComparacionFacial = !mostrarComparacionFacial;
            if (mostrarComparacionFacial)
            {
                isLoadingComparacionFacial = true;
                Task.Run(async () =>
                {
                    await Task.Delay(500);
                    isLoadingComparacionFacial = false;
                    await InvokeAsync(StateHasChanged);
                });
            }
        }

        private void OnMostrarComprobanteDomicilio()
        {
            mostrarComprobanteDomicilio = !mostrarComprobanteDomicilio;
            if (mostrarComprobanteDomicilio)
            {
                isLoadingComprobanteDomicilio = true;
                Task.Run(async () =>
                {
                    await Task.Delay(500);
                    isLoadingComprobanteDomicilio = false;
                    await InvokeAsync(StateHasChanged);
                });
            }
        }

        private void OnMostrarComprobanteBancario()
        {
            mostrarComprobanteBancario = !mostrarComprobanteBancario;
            if (mostrarComprobanteBancario)
            {
                isLoadingComprobanteBancario = true;
                Task.Run(async () =>
                {
                    await Task.Delay(500);
                    isLoadingComprobanteBancario = false;
                    await InvokeAsync(StateHasChanged);
                });
            }
        }

        private void OnMostrarComprobanteIngresos()
        {
            mostrarComprobanteIngresos = !mostrarComprobanteIngresos;
            if (mostrarComprobanteIngresos)
            {
                isLoadingComprobanteIngresos = true;
                Task.Run(async () =>
                {
                    await Task.Delay(500);
                    isLoadingComprobanteIngresos = false;
                    await InvokeAsync(StateHasChanged);
                });
            }
        }

        private void OnMostrarPrivacidad()
        {
            mostrarPrivacidad = !mostrarPrivacidad;
            if (mostrarPrivacidad)
            {
                isLoadingPrivacidad = true;
                Task.Run(async () =>
                {
                    await Task.Delay(500);
                    isLoadingPrivacidad = false;
                    await InvokeAsync(StateHasChanged);
                });
            }
        }

        private void OnResolucion(bool resolucion)
        {
            agregarResolucion = true;
            if (resolucion)
            {
                tipoResolucionId = 1;
                mensajePopup = "¿Estas seguro de aprobar la solicitud?";
            }
            else
            {
                tipoResolucionId = 2;
                mensajePopup = "¿Estas seguro de rechazar la solicitud?";
            }
            popupOpen = true;
        }

        private void OnPopupClose(bool respuesta)
        {
            popupOpen = false;
            if (respuesta)
            {
                if (agregarListaNegra)
                    mensajePopupComent = "Agregar comentario";

                if (agregarResolucion)
                    mensajePopupComent = "Agregar comentario";

                popupOpenComent = true;
            }
        }

        private async Task OnPopupComentClose(string respuesta)
        {
            popupOpenComent = false;
            if (!string.IsNullOrEmpty(respuesta))
            {
                (string mensajeResponse, int resultado) response = ("", 0);

                if (agregarListaNegra)
                {
                    var lista = new ListaNegraRequest()
                    {
                        SolicitanteId = model.SolicitanteId,
                        UsuarioId = Credential.UsuarioId,
                        Motivo = respuesta,
                        TipoMovimientoId = 1
                    };
                    response = await Service.UpdateListaNegraAsync(lista);
                }
                if (agregarResolucion)
                {
                    var resolucion = new ResolucionRequest()
                    {
                        SolicitanteId = model.SolicitanteId,
                        UsuarioId = Credential.UsuarioId,
                        Comentario = respuesta,
                        TipoResolucionId = tipoResolucionId,
                        ResultadoIBMS = model.Validaciones.ResultadoIBMS,
                        SemaforoIBMS = model.Validaciones.SemaforoIBMS,
                        ResultadoListaNegra = model.Validaciones.ResultadoListaNegra,
                        SemaforoListaNegra = model.Validaciones.SemaforoListaNegra,
                        FechaListaNegra = model.Validaciones.FechaListaNegra
                    };
                    response = await Service.UpdateResolucionAsync(resolucion);
                }
                if (response.resultado <= 0)
                    mensaje = response.mensajeResponse;
                else
                {
                    await ConsultarDatos(model.SolicitanteId);
                }
            }
            agregarListaNegra = false;
            agregarResolucion = false;
        }

        private void OnAccion(int id)
        {
            InicializarVariables();
            NavigationManager.NavigateTo($"/solicitudDetalle/{id}/{UrlOrigen}/{Estatus}");
        }

        private void OnRecargarSemaforo(TipoSemaforo tipoRecarga)
        {
            switch (tipoRecarga)
            {
                case TipoSemaforo.Identificacion:
                    IniciarSemaforoIdentificacion(true);
                    break;

                case TipoSemaforo.Privacidad:
                    IniciarSemaforoPrivacidad(true);
                    break;

                case TipoSemaforo.ComparacionFacial:
                    IniciarSemaforoComparacionFacial(true);
                    break;

                case TipoSemaforo.Huellas:
                    IniciarSemaforoHuellas(true);
                    break;

                case TipoSemaforo.Curp:
                    IniciarSemaforoCurp(true);
                    break;

                case TipoSemaforo.Correo:
                    IniciarSemaforoCorreo(true);
                    break;

                case TipoSemaforo.Telefono:
                    IniciarSemaforoTelefono(true);
                    break;

                case TipoSemaforo.ListasInteres:
                    IniciarSemaforoListasInteres(true);
                    break;

                case TipoSemaforo.Ine:
                    IniciarSemaforoIne(true, true);
                    break;

                case TipoSemaforo.ComprobanteDomicilio:
                    Task.Run(async () => { await IniciarSemaforoComprobanteDomicilio(true, true); });
                    break;

                case TipoSemaforo.ComprobanteBancario:
                    Task.Run(async () => { await IniciarSemaforoComprobanteBancario(true, true); });
                    break;

                case TipoSemaforo.ComprobanteIngresos:
                    Task.Run(async () => { await IniciarSemaforoComprobanteIngresos(true, true); });
                    break;
            }
        }

        private void InicializarVariables()
        {
            recargarPagina = true;
            renderizarMapa = true;
            existeFotoCamara = false;

            isLoadingModel = true;
            model = null;
            mostrarListaNegra = false;
            mostrarResolucion = false;

            IniciarSemaforoIdentificacion(false);
            IniciarSemaforoPrivacidad(false);
            IniciarSemaforoComparacionFacial(false);
            IniciarSemaforoHuellas(false);
            IniciarSemaforoIne(false, false);
            IniciarSemaforoCorreo(false);
            IniciarSemaforoTelefono(false);
            IniciarSemaforoCurp(false);
            IniciarSemaforoListasInteres(false);

            var comprobanteBancario = Task.Run(async () => { await IniciarSemaforoComprobanteBancario(false, false); });
            comprobanteBancario.Wait();
            var comprobanteDomicilio = Task.Run(async () => { await IniciarSemaforoComprobanteDomicilio(false, false); });
            comprobanteDomicilio.Wait();
            var comprobanteIngresos = Task.Run(async () => { await IniciarSemaforoComprobanteIngresos(false, false); });
            comprobanteIngresos.Wait();

        }

        private void IniciarConsultaGeoreferencia()
        {
            Task.Run(async () =>
            {
                await ConsultarGeoreferencia();
            });
        }

        private void IniciarSemaforoIdentificacion(bool consultar)
        {
            isLoadingIdentificacion = true;
            validacionIdentificacion = null;
            mostrarIdentificacion = false;
            if (consultar)
            {
                Task.Run(async () => 
                {
                    await ConsultarIdentificacion();
                    isLoadingIdentificacion = false;
                    await InvokeAsync(StateHasChanged);
                });
            }
        }

        private void IniciarSemaforoPrivacidad(bool consultar)
        {
            isLoadingPrivacidad = true;
            validacionPrivacidad = null;
            mostrarPrivacidad = false;
            if (consultar)
            {
                Task.Run(async () =>
                {
                    await ConsultarAvisoPrivacidad();
                    isLoadingPrivacidad = false;
                    await InvokeAsync(StateHasChanged);
                });
            }
        }

        private void IniciarSemaforoComparacionFacial(bool consultar)
        {
            isLoadingComparacionFacial = true;
            validacionComparacionFacial = null;
            mostrarComparacionFacial = false;
            if (consultar)
            {
                Task.Run(async () =>
                {
                    await ConsultarComparacionFacial();
                    isLoadingComparacionFacial = false;
                    await InvokeAsync(StateHasChanged);
                });
            }
        }

        private void IniciarSemaforoHuellas(bool consultar)
        {
            isLoadingComparacionHuellas = true;
            validacionComparacionHuellas = null;
            mostrarHuellas = false;
            if (consultar)
            {
                Task.Run(async () =>
                {
                    await ConsultarComparacionHuellas();
                    isLoadingComparacionHuellas = false;
                    await InvokeAsync(StateHasChanged);
                });
            }
        }

        private void IniciarSemaforoCorreo(bool consultar)
        {
            isLoadingCorreo = true;
            validacionCorreo = null;
            mostrarCorreo = false;
            if (consultar)
            {
                Task.Run(async () =>
                {
                    await ConsultarCorreo();
                    isLoadingCorreo = false;
                    await InvokeAsync(StateHasChanged);
                });
            }
        }

        private void IniciarSemaforoTelefono(bool consultar)
        {
            isLoadingTelefono = true;
            validacionTelefono = null;
            mostrarTelefono = false;
            if (consultar)
            {
                Task.Run(async () =>
                {
                    await ConsultarTelefono();
                    isLoadingTelefono = false;
                    await InvokeAsync(StateHasChanged);
                });
            }
        }

        private void IniciarSemaforoCurp(bool consultar)
        {
            isLoadingCurp = true;
            validacionCurp = null;
            mostrarCurp = false;
            if (consultar)
            {
                Task.Run(async () =>
                {
                    await ConsultarCurp();
                    isLoadingCurp = false;
                    await InvokeAsync(StateHasChanged);
                });
            }
        }

        private void IniciarSemaforoListasInteres(bool consultar)
        {
            isLoadingListas = true;
            validacionListas = null;
            mostrarListas = false;
            if (consultar)
            {
                Task.Run(async () =>
                {
                    await ConsultarListasInteres();
                    isLoadingListas = false;
                    await InvokeAsync(StateHasChanged);
                });
            }
        }

        private void IniciarSemaforoIne(bool consultar, bool consultarValidaciones)
        {
            isLoadingIne = true;
            validacionIne = null;
            mostrarIne = false;
            if (consultar)
            {
                Task.Run(async () =>
                {
                    await InvokeAsync(StateHasChanged);
                    if (consultarValidaciones)
                        await ConsultarValidaciones(_TituloIne);
                    await ConsultarIne();
                    isLoadingIne = false;
                    await InvokeAsync(StateHasChanged);
                });
            }
        }

        private async Task IniciarSemaforoComprobanteIngresos(bool consultar, bool consultarValidaciones)
        {
            isLoadingComprobanteIngresos = true;
            mostrarComprobanteIngresos = false;
            if (consultar)
            {
                await InvokeAsync(StateHasChanged);
                if (consultarValidaciones)
                    await ConsultarValidaciones(_TituloComprobanteIngresos);
                await ConsultarDocumentoIngresos();
                await InvokeAsync(StateHasChanged);
            }
            else
            {
                validacionComprobanteIngresos = null;
                _documentoIdComprobanteIngresos = -1;
                formatoComprobanteIngresos = "";
                documentoComprobanteIngresos = null;
            }
        }

        private async Task IniciarSemaforoComprobanteDomicilio(bool consultar, bool consultarValidaciones)
        {
            isLoadingComprobanteDomicilio = true;
            mostrarComprobanteDomicilio = false;
            if (consultar)
            {
                await InvokeAsync(StateHasChanged);
                if (consultarValidaciones)
                    await ConsultarValidaciones(_TituloComprobanteDomicilio);
                await ConsultarDocumentoDomicilio();
                await InvokeAsync(StateHasChanged);
            }
            else
            {
                validacionComprobanteDomicilio = null;
                _documentoIdComprobanteDomicilio = -1;
                formatoComprobanteDomicilio = "";
                documentoComprobanteDomicilio = null;
            }
        }

        private async Task IniciarSemaforoComprobanteBancario(bool consultar, bool consultarValidaciones)
        {
            isLoadingComprobanteBancario = true;
            mostrarComprobanteBancario = false;
            if (consultar)
            {
                await InvokeAsync(StateHasChanged);
                if (consultarValidaciones)
                    await ConsultarValidaciones(_TituloComprobanteBancario);
                await ConsultarDocumentoBancario();
                await InvokeAsync(StateHasChanged);
            }
            else
            {
                validacionComprobanteBancario = null;
                _documentoIdComprobanteBancario = -1;
                formatoComprobanteBancario = "";
                documentoComprobanteBancario = null;
            }
        }
    
    }
}
