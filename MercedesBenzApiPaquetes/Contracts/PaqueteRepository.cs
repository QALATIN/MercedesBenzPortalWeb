using Dapper;
using MercedesBenzApiPaquetes.Services;
using MercedesBenzDBContext;
using MercedesBenzDBValidaciones;
using MercedesBenzLibrary;
using MercedesBenzModel;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Threading.Tasks;
using Wsq.Managed;

namespace MercedesBenzApiPaquetes.Contracts
{
    public class PaqueteRepository : IPaqueteRepository
    {
        private readonly ApplicationDBContext _context;
        private readonly Routes _routes;
        private readonly IValidacionService _validacionService;
        private readonly IComparacionFacialService _comparacionfacialserviceService;
        private static readonly int _origenId = 1;
        private static readonly int _tipoLogIdError = 1;

        public PaqueteRepository(ApplicationDBContext context, Routes routes, IValidacionService validacionService, IComparacionFacialService comparacionfacialserviceService)
        {
            _context = context;
            _routes = routes;
            _validacionService = validacionService;
            _comparacionfacialserviceService = comparacionfacialserviceService;
        }

        public async Task<(int solicitanteId, int identificacionId, int capturaIdentificacionId1, int capturaIdentificacionId4, int fotoId1, int fotoId2)> AddSolicitanteAsync(SolicitanteRequest request)
        {
            string mensaje = "Error en Paquete-AddSolicitanteAsync";
            try
            {
                int solicitanteId = -1;
                int identificacionId = -1;
                int capturaIdentificacionId1 = -1;
                int capturaIdentificacionId4 = -1;
                int fotoId1 = -1;
                int fotoId2 = -1;
                int resolucionId = -1;
                int validacionId = -1;
                bool identificacionIne = false;

                Solicitante solicitante = RequestSolicitante(request);
                if (solicitante.TipoDocumento.ToUpper() == "MEXICO (MEX) VOTER IDENTIFICATION CARD")
                    identificacionIne = true;

                Identificacion identificacion = RequestIdentificacion(request, identificacionIne);
                CapturaIdentificacion capturaIdentificacion1 = RequestCapturaIdentificacion1(request.UsuarioId, request.FechaDeRegistro, request.CapturaFrenteEnBase64);
                CapturaIdentificacion capturaIdentificacion4 = RequestCapturaIdentificacion4(request.UsuarioId, request.FechaDeRegistro, request.CapturaReversoEnBase64);
                Foto foto1 = RequestFoto1(request.UsuarioId, request.FechaDeRegistro, request.FotoEnBase64, request.SelfieBase64);
                Foto foto2 = RequestFoto2(request.UsuarioId, request.FechaDeRegistro, request.FotoDeIdentificacionEnBase64);
                var resolucion = new Resolucion();
                var validacion = new SolicitudValidacion();

                using (var connection = _context.CreateConnection())
                {
                    connection.Open();

                    string qrySelect = DbSql.QryGetAgenciaIdxUsuarioId(solicitante.UsuarioId);
                    int agenciaId = ConsultarCampoId(connection, qrySelect, "UsuarioId");

                    solicitante.SolicitanteIdOrigen = 0;
                    if (solicitante.SolicitanteId > 0)
                    {
                        qrySelect = DbSql.QryGetSolicitanteIdxSolicitanteId(solicitante.SolicitanteId);
                        _ = ConsultarCampoId(connection, qrySelect, "SolicitanteId");

                        solicitante.SolicitanteIdOrigen = solicitante.SolicitanteId;
                    }
                    else
                        solicitante.TipoCliente = "Acreditado";

                    solicitante.Estatus = "Nueva";

                    DateTime fechaEnvio = DateTime.Now;
                    solicitante.FechaEnvio = fechaEnvio;
                    solicitante.AgenciaId = agenciaId;
                    using var transaction = connection.BeginTransaction();
                    try
                    {

                        qrySelect = DbSql.QryAddSolicitantes();
                        var parameters = DbSql.ParametersSolicitantes(solicitante);

                        solicitanteId = await connection.QuerySingleAsync<int>(qrySelect, parameters);
                        solicitante.SolicitanteId = solicitanteId;

                        if (identificacion != null)
                        {
                            identificacion.SolicitanteId = solicitante.SolicitanteId;
                            identificacion.FechaEnvio = fechaEnvio;
                            qrySelect = DbSql.QryAddIdentificaciones();
                            parameters = DbSql.ParametersIdentificaciones(identificacion);
                            identificacionId = await connection.QuerySingleAsync<int>(qrySelect, parameters);
                            identificacion.IdentificacionId = identificacionId;
                        }
                        if (capturaIdentificacion1 != null)
                        {
                            capturaIdentificacion1.SolicitanteId = solicitante.SolicitanteId;
                            capturaIdentificacion1.FechaEnvio = fechaEnvio;
                            qrySelect = DbSql.QryAddCapturaIdentificaciones();
                            parameters = DbSql.ParametersCapturaIdentificaciones(capturaIdentificacion1);
                            capturaIdentificacionId1 = await connection.QuerySingleAsync<int>(qrySelect, parameters);
                            capturaIdentificacion1.CapturaIdentificacionId = capturaIdentificacionId1;
                        }
                        if (capturaIdentificacion4 != null)
                        {
                            capturaIdentificacion4.SolicitanteId = solicitante.SolicitanteId;
                            capturaIdentificacion4.FechaEnvio = fechaEnvio;
                            qrySelect = DbSql.QryAddCapturaIdentificaciones();
                            parameters = DbSql.ParametersCapturaIdentificaciones(capturaIdentificacion4);
                            capturaIdentificacionId4 = await connection.QuerySingleAsync<int>(qrySelect, parameters);
                            capturaIdentificacion4.CapturaIdentificacionId = capturaIdentificacionId4;
                        }
                        if (foto1 != null)
                        {
                            foto1.SolicitanteId = solicitante.SolicitanteId;
                            foto1.FechaEnvio = fechaEnvio;
                            qrySelect = DbSql.QryAddFotos();
                            parameters = DbSql.ParametersFotos(foto1);
                            fotoId1 = await connection.QuerySingleAsync<int>(qrySelect, parameters);
                            foto1.FotoId = fotoId1;
                        }
                        if (foto2 != null)
                        {
                            foto2.SolicitanteId = solicitante.SolicitanteId;
                            foto2.FechaEnvio = fechaEnvio;
                            qrySelect = DbSql.QryAddFotos();
                            parameters = DbSql.ParametersFotos(foto2);
                            fotoId2 = await connection.QuerySingleAsync<int>(qrySelect, parameters);
                            foto2.FotoId = fotoId2;
                        }

                        resolucion.SolicitanteId = solicitanteId;
                        resolucion.TipoResolucionId = 0;
                        resolucion.UsuarioId = solicitante.UsuarioId;

                        qrySelect = DbSql.QryAddResolucion();
                        parameters = DbSql.ParametersResolucion(resolucion);
                        resolucionId = await connection.QuerySingleAsync<int>(qrySelect, parameters);
                        resolucion.ResolucionId = resolucionId;

                        validacion.SolicitanteId = solicitanteId;

                        (string semaforoIdentificacion, string resultadoIdentificacion) = Validaciones.CalcularSemaforoIdentificacion(solicitante.ResultadoGeneral);
                        validacion.SemaforoIdentificacion = semaforoIdentificacion;
                        validacion.ResultadoIdentificacion = resultadoIdentificacion;
                        validacion.FechaIdentificacion = fechaEnvio;

                        string score="0";
                        if (solicitante.ResultadoComparacionFacial.ToUpper() == "TRUE")
                            score = solicitante.ScoreComparacionFacial;

                        (string semaforoFacial, string resultadoFacial) = Validaciones.CalcularSemaforoComparacionFacial(score);
                        validacion.SemaforoFacial = semaforoFacial;
                        validacion.ResultadoFacial = resultadoFacial;
                        validacion.FechaFacial = fechaEnvio;

                        parameters = DbSql.ParametersValidacion(validacion);

                        qrySelect = DbSql.QryAddValidacion();

                        validacionId = await connection.QuerySingleAsync<int>(qrySelect, parameters);
                        validacion.ValidacionId = validacionId;

                        transaction.Commit();

                        IniciarValidacionDatosExternos(validacionId, solicitanteId, identificacionIne, solicitante.UsuarioId);
                    }
                    catch (Exception ex)
                    {
                        _ = ex.Message;
                        transaction.Rollback();
                        throw new InvalidOperationException("Error inesperado");
                    }
                }
                return (solicitanteId, identificacionId, capturaIdentificacionId1, capturaIdentificacionId4, fotoId1, fotoId2);
            }
            catch (Exception ex)
            {
                mensaje += "|" + ex.Message;
                throw new InvalidOperationException(mensaje);
            }
        }

        public async Task<int> AddHuellaAsync(HuellaRequest request)
        {
            string mensaje = "Error en Paquete-AddHuellaAsync";
            try
            {
                Huella huella = RequestHuella(request);
                huella.FechaEnvio = DateTime.Now;

                using var connection = _context.CreateConnection();
                connection.Open();

                string qry = DbSql.QryGetSolicitanteIdxSolicitanteId(huella.SolicitanteId);
                _ = ConsultarCampoId(connection, qry, "SolicitanteId");

                qry = DbSql.QryAddHuellas();
                var parameters = DbSql.ParametersHuellas(huella);
                int id = await connection.QuerySingleAsync<int>(qry, parameters);
                huella.HuellaId = id;

                return huella.HuellaId;
            }
            catch (Exception ex)
            {
                mensaje += "|" + ex.Message;
                throw new InvalidOperationException(mensaje);
            }
        }
        
        public async Task<int> AddDocumentoAsync(DocumentoRequest request)
        {
            string mensaje = "Error en Paquete-AddDocumentoAsync";
            try
            {
                Documento documento = RequestDocumento(request);
                documento.FechaEnvio = DateTime.Now;

                using var connection = _context.CreateConnection();
                connection.Open();

                string qry = DbSql.QryGetSolicitanteIdxSolicitanteId(documento.SolicitanteId);
                _ = ConsultarCampoId(connection, qry, "SolicitanteId");

                qry = DbSql.QryAddDocumentos();
                var parameters = DbSql.ParametersDocumentos(documento);
                int id = await connection.QuerySingleAsync<int>(qry, parameters);
                documento.DocumentoId = id;

                qry = DbSql.QryGetValidacionIdxSolicitanteId(documento.SolicitanteId);
                int validacionId = ConsultarCampoId(connection, qry, "ValidacionId");
                IniciarValidacionDocumentosExternos(validacionId, request.SolicitanteId, request.UsuarioId, request.NombreDocumento);

                return documento.DocumentoId;
            }
            catch (Exception ex)
            {
                mensaje += "|" + ex.Message;
                throw new InvalidOperationException(mensaje);
            }
        }

        public async Task<int> AddAvisoPrivacidadAsync(AvisoPrivacidadRequest request)
        {
            string mensaje = "Error en Paquete-AddAvisoPrivacidadAsync";
            try
            {
                AvisoPrivacidad privacidad = RequestDocumentoPrivacidad(request);
                privacidad.FechaEnvio = DateTime.Now;

                using var connection = _context.CreateConnection();
                connection.Open();

                string qry = DbSql.QryGetSolicitanteIdxSolicitanteId(privacidad.SolicitanteId);
                _ = ConsultarCampoId(connection, qry, "SolicitanteId");

                qry = DbSql.QryAddAvisosPrivacidad();
                var parameters = DbSql.ParametersAvisosPrivacidad(privacidad);
                int id = await connection.QuerySingleAsync<int>(qry, parameters);
                privacidad.AvisoPrivacidadId = id;

                return privacidad.AvisoPrivacidadId;
            }
            catch (Exception ex)
            {
                mensaje += "|" + ex.Message;
                throw new InvalidOperationException(mensaje);
            }
        }
        
        public async Task<int> AddListaNegraAsync(ListaNegraRequest request)
        {
            string mensaje = "Error en Paquete-AddListaNegraAsync";
            try
            {
                ListaNegra lista = RequestListaNegra(request);
                lista.FechaRegistro = DateTime.Now;

                using var connection = _context.CreateConnection();
                connection.Open();

                string qry = DbSql.QryGetSolicitanteIdxSolicitanteId(lista.SolicitanteId);
                _ = ConsultarCampoId(connection, qry, "SolicitanteId");

                using var transaction = connection.BeginTransaction();
                try
                {
                    qry = DbSql.QryAddListaNegra();
                    var parameters = DbSql.ParametersListaNegra(lista);
                    int id = await connection.QuerySingleAsync<int>(qry, parameters);
                    lista.ListaNegraId = id;

                    parameters.Add("@SemaforoListaNegra", "rojo");
                    parameters.Add("@ResultadoListaNegra", "true");
                    qry = DbSql.QryUpdateSemaforoListaNegra();
                    _ = await connection.ExecuteAsync(qry, parameters);
                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    _ = ex.Message;
                    transaction.Rollback();
                    throw new InvalidOperationException("Error inesperado");
                }
                return lista.ListaNegraId;
            }
            catch (Exception ex)
            {
                mensaje += "|" + ex.Message;
                throw new InvalidOperationException(mensaje);
            }
        }

        public async Task<Resolucion> UpdateResolucionAsync(ResolucionRequest request)
        {
            string mensaje = "Error en Paquete-UpdateResolucionAsync";
            try
            {
                Resolucion resolucion = RequestResolucion(request);
                resolucion.FechaRegistro = DateTime.Now;

                using var connection = _context.CreateConnection();
                connection.Open();

                string qry = DbSql.QryGetSolicitanteIdxSolicitanteId(resolucion.SolicitanteId);
                _ = ConsultarCampoId(connection, qry, "SolicitanteId");

                var parameters = DbSql.ParametersResolucion(resolucion);
                parameters.Add("@Estatus", "Finalizada");
                parameters.Add("@SemaforoIBMS", request.SemaforoIBMS);
                parameters.Add("@ResultadoIBMS", request.ResultadoIBMS);

                Resolucion data = null;
                using var transaction = connection.BeginTransaction();
                try
                {
                    qry = DbSql.QryUpdateSolicitanteEstatus();
                    qry += DbSql.QryUpdateSemaforoIbms();
                    await connection.ExecuteAsync(qry, parameters);

                    if (request.FechaListaNegra == null)
                    {
                        parameters.Add("@SemaforoListaNegra", request.SemaforoListaNegra);
                        parameters.Add("@ResultadoListaNegra", request.ResultadoListaNegra);
                        qry = DbSql.QryUpdateSemaforoListaNegra();
                        await connection.ExecuteAsync(qry, parameters);
                    }

                    qry = DbSql.QryUpdateResolucion();
                    int id = await connection.ExecuteAsync(qry, parameters);
                    if (id > 0)
                        data = resolucion;

                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    _ = ex.Message;
                    transaction.Rollback();
                    throw new InvalidOperationException("Error inesperado");
                }
                return data;
            }
            catch (Exception ex)
            {
                mensaje += "|" + ex.Message;
                throw new InvalidOperationException(mensaje);
            }
        }

        public async Task<SolicitudFicha> GetSolicitudFichaAsync(SolicitudRequest request)
        {
            string mensaje  = "Error en Paquete-GetSolicitudFichaAsync";
            try
            {
                if (request.SolicitanteId > 0)
                {
                    var parameters = new DynamicParameters();
                    parameters.Add("@SolicitanteId", request.SolicitanteId);

                    using var connection = _context.CreateConnection();
                    connection.Open();

                    string qrySelect = DbSql.QryGetSolicitanteFicha();
                    qrySelect += DbSql.QryGetIdentificacionesFicha();
                    qrySelect += DbSql.QryGetIneFicha();
                    qrySelect += DbSql.QryGetDocumentosCargadosFicha();
                    qrySelect += DbSql.QryGetAvisoPrivacidadFicha();
                    qrySelect += DbSql.QryGetListaNegraFicha();
                    qrySelect += DbSql.QryGetResolucionFicha();
                    qrySelect += DbSql.QryGetValidaciones();

                    var reader = await connection.QueryMultipleAsync(qrySelect, parameters);

                    SolicitudFicha data = reader.Read<SolicitudFicha>().FirstOrDefault();
                    List<SolicitanteIdentificacion> identificaciones = (List<SolicitanteIdentificacion>)reader.Read<SolicitanteIdentificacion>();
                    SolicitudIne ine = reader.Read<SolicitudIne>().FirstOrDefault();
                    List<SolicitanteDocumentoCargado> documentosCargados = (List<SolicitanteDocumentoCargado>)reader.Read<SolicitanteDocumentoCargado>();

                    int id = reader.Read<int>().FirstOrDefault();
                    if (id > 0)
                        data.ExisteAvisoPrivacidad = true;
                    else
                        data.ExisteAvisoPrivacidad = false;

                    List<SolicitanteListaNegra> listaNegra = (List<SolicitanteListaNegra>)reader.Read<SolicitanteListaNegra>();
                    SolicitanteResolucion resolucion = reader.Read<SolicitanteResolucion>().FirstOrDefault();
                    SolicitudValidacion validaciones = reader.Read<SolicitudValidacion>().FirstOrDefault();

                    parameters.Add("@SolicitanteIdOrigen", data.SolicitanteIdOrigen);
                    string qry = DbSql.QryGetSolicitanteSolidarioFicha(data.SolicitanteIdOrigen == 0);
                    List<SolicitanteSolidario> solidarios = (List<SolicitanteSolidario>)await connection.QueryAsync<SolicitanteSolidario>(qry, parameters);

                    if (data.Estatus == "Nueva")
                    {
                        parameters.Add("@FechaRegistro", DateTime.Now);
                        parameters.Add("@UsuarioId", request.UsuarioId);
                        parameters.Add("@Estatus", "En proceso");

                        using var transaction = connection.BeginTransaction();
                        try
                        {
                            qry = DbSql.QryUpdateSolicitanteConsulta();
                            await connection.ExecuteAsync(qry, parameters);

                            qry = DbSql.QryUpdateSolicitanteEstatus();
                            await connection.ExecuteAsync(qry, parameters);
                            transaction.Commit();
                        }
                        catch (Exception ex)
                        {
                            _ = ex.Message;
                            transaction.Rollback();
                            throw new InvalidOperationException("Error inesperado");
                        }
                    }

                    if (identificaciones.Count > 0)
                        data.Identificaciones = identificaciones;
                    if (ine != null)
                        data.Ine = ine;
                    if (documentosCargados.Count > 0)
                        data.DocumentosCargados = documentosCargados;
                    if (listaNegra.Count > 0)
                        data.ListaNegra = listaNegra;
                    if (resolucion == null)
                        resolucion = new() { SolicitanteId = data.SolicitanteId};
                    data.Resolucion = resolucion;
                    if (solidarios.Count > 0)
                        data.Solidarios = solidarios;
                    if (validaciones == null)
                        validaciones = new() { SolicitanteId = data.SolicitanteId };

                    validaciones.ValidarIdentificacion = true;  // El semáforo se calcula con el campo Resultado_General que se guarda en la alta
                    validaciones.ValidarFacial = true;  // El semáforo se calcula con el campo Score_Comparacion_Facial que se guarda en la alta 
                    validaciones.ValidarCorreo = true;  // El semáforo se calcula con el campo CorreoElectronico que se guarda en la alta
                    validaciones.ValidarTelefono = true;  // El semáforo se calcula con el campo Telefono que se guarda en la alta
                    if (!string.IsNullOrEmpty(data.Curp))
                        validaciones.ValidarCurp = true;    // El semáforo se calcula con el campo Curp que se guarda en la alta
                    validaciones.ValidarListaAml = true;  // El semáforo se calcula con el Nombre Completo del Cliente que se guarda en la alta
                    foreach (var item in documentosCargados)
                    {
                        switch (item.TipoDocumentoId)
                        {
                            case "1":
                                validaciones.ValidarComprobanteIngresos = true;
                                break;
                            case "2":
                                validaciones.ValidarComprobanteDomicilio = true;
                                break;
                            case "3":
                                validaciones.ValidarComprobanteBancario = true;
                                break;
                        }
                    }
                    validaciones.ValidarListaNegra = true;  // El semáforo se calcula con base a la respuesta del analista
                    if (string.IsNullOrEmpty(validaciones.ResultadoListaNegra))
                    {
                        if (listaNegra.Count > 0)
                        {
                            validaciones.SemaforoListaNegra = "rojo";
                            validaciones.ResultadoListaNegra = "true";
                        }
                        else
                        {
                            validaciones.SemaforoListaNegra = "verde";
                            validaciones.ResultadoListaNegra = "false";
                        }
                    }

                    if (data.TipoDocumento == "Mexico (MEX) Voter Identification Card")
                        validaciones.ValidarIne = true; // El semáforo se calcula cuando el documento de identificación es una credencial de elector

                    validaciones.ValidarAfis = true;    // El semáforo se calcula con las huellas que se guardaron en la alta

                    data.Validaciones = validaciones;

                    return data;
                }
                else
                    throw Validaciones.ExceptionCampoIncorrecto("SolicitanteId");
            }
            catch (Exception ex)
            {
                mensaje += "|" + ex.Message;
                throw new InvalidOperationException(mensaje);
            }
        }

        public async Task<SolicitudIdentificacion> GetSolicitudIdentificacionAsync(SolicitudValidacionRequest request)
        {
            string mensaje = "Error en Paquete-GetSolicitudIdentificacionAsync";
            string qry;

            bool ejecutarValidacion = false;
            SolicitudIdentificacion data = new();
            Resultado resultado = null;
            try
            {
                if (request.SolicitanteId > 0)
                {
                    var parameters = new DynamicParameters();
                    parameters.Add("@SolicitanteId", request.SolicitanteId);

                    using var connection = _context.CreateConnection();
                    connection.Open();

                    qry = DbSql.QryGetSolicitanteIdxSolicitanteId(request.SolicitanteId);
                    _ = ConsultarCampoId(connection, qry, "SolicitanteId");
                    if (request.Validar == "1")
                        ejecutarValidacion = true;
                    else
                    {
                        qry = DbSql.QryGetSolicitudSemaforoIdentificacion();
                        resultado = await connection.QuerySingleOrDefaultAsync<Resultado>(qry, parameters);
                        if (resultado.Semaforo == null)
                            ejecutarValidacion = true;
                    }

                    if (ejecutarValidacion)
                    {
                        int id = ValidarSemaforoIdentificacion(connection, parameters);
                        if (id > 0)
                        {
                            qry = DbSql.QryGetSolicitudSemaforoIdentificacion();
                            resultado = await connection.QuerySingleOrDefaultAsync<Resultado>(qry, parameters);
                        }
                    }

                    qry = DbSql.QryGetSolicitudIdentificacion();
                    qry += DbSql.QryGetSolicitudIdentificacionPdf();

                    var reader = await connection.QueryMultipleAsync(qry, parameters);

                    List<SolicitanteCapturaIdentificacion> identificaciones = (List<SolicitanteCapturaIdentificacion>)reader.Read<SolicitanteCapturaIdentificacion>();
                    byte[] documentoPdf = reader.Read<byte[]>().FirstOrDefault();

                    data.Resultado = resultado;
                    data.Identificaciones = identificaciones;
                    data.DocumentoPdf = documentoPdf;
                    return data;
                }
                else
                    throw Validaciones.ExceptionCampoIncorrecto("SolicitanteId");
            }
            catch (Exception ex)
            {
                mensaje += "|" + ex.Message;
                throw new InvalidOperationException(mensaje);
            }
        }

        public async Task<SolicitudComparacionFacial> GetSolicitudComparacionFacialAsync(SolicitudValidacionRequest request)
        {
            string mensaje = "Error en Paquete-GetSolicitudComparacionFacialAsync";
            var data = new SolicitudComparacionFacial();
            Resultado resultado = null;
            try
            {
                if (request.SolicitanteId > 0)
                {
                    var parameters = new DynamicParameters();
                    parameters.Add("@SolicitanteId", request.SolicitanteId);

                    using var connection = _context.CreateConnection();
                    connection.Open();

                    string qry = DbSql.QryGetSolicitanteIdxSolicitanteId(request.SolicitanteId);
                    _ = ConsultarCampoId(connection, qry, "SolicitanteId");

                    bool ejecutarValidacion = false;
                    if (request.Validar == "1")
                        ejecutarValidacion = true;
                    else
                    {
                        qry = DbSql.QryGetSolicitudSemaforoComparacionFacial();
                        resultado = await connection.QuerySingleOrDefaultAsync<Resultado>(qry, parameters);
                        if (resultado.Semaforo == null)
                            ejecutarValidacion = true;
                    }
                    if (ejecutarValidacion)
                    {
                        int id = ValidarSemaforoComparacionFacial(connection, parameters);
                        if (id > 0)
                        {
                            qry = DbSql.QryGetSolicitudSemaforoComparacionFacial();
                            resultado = await connection.QuerySingleOrDefaultAsync<Resultado>(qry, parameters);
                        }
                    }

                    qry = DbSql.QryGetSolicitudFotos();
                    List<SolicitanteFoto> comparacion = (List<SolicitanteFoto>)await connection.QueryAsync<SolicitanteFoto>(qry, parameters);
                    data.Resultado = resultado;
                    data.Fotos = comparacion;
                    return data;
                }
                else
                    throw Validaciones.ExceptionCampoIncorrecto("SolicitanteId");
            }
            catch (Exception ex)
            {
                mensaje += "|" + ex.Message;
                throw new InvalidOperationException(mensaje);
            }
        }

        public async Task<SolicitudComparacionHuellas> GetSolicitudComparacionHuellasAsync(SolicitudValidacionRequest request)
        {
            string mensaje = "Error en Paquete-GetSolicitudComparacionHuellasAsync";
            HuellaSemaforo huellaSemaforo;
            var data = new SolicitudComparacionHuellas();
            List<SolicitudHuella> huellas;

            DateTime fechaValidacion = DateTime.Now;
            string repositoryPath = Validaciones.GenerarPathFile(_routes.RepositoryPath, fechaValidacion);
            string pathFile = Validaciones.GenerarPathFileHuellasValidacion(repositoryPath, request.SolicitanteId);

            try
            {
                if (request.SolicitanteId > 0)
                {
                    var parameters = new DynamicParameters();
                    parameters.Add("@SolicitanteId", request.SolicitanteId);

                    using var connection = _context.CreateConnection();
                    connection.Open();

                    string qry = DbSql.QryGetNombreClientexSolicitanteId(request.SolicitanteId);
                    _ = ConsultarCampoTexto(connection, qry, "SolicitanteId");

                    qry = DbSql.QryGetSolicitudSemaforoHuellas();
                    huellaSemaforo = await connection.QuerySingleOrDefaultAsync<HuellaSemaforo>(qry, parameters);
                    if (huellaSemaforo.FechaAfis != null)
                    {
                        repositoryPath = Validaciones.GenerarPathFile(_routes.RepositoryPath, (DateTime)huellaSemaforo.FechaAfis);
                        pathFile = Validaciones.GenerarPathFileHuellasValidacion(repositoryPath, request.SolicitanteId);
                    }

                    if (request.Validar == "1")
                        huellaSemaforo.FechaAfis = null;

                    if (huellaSemaforo.FechaAfis == null)
                    {
                        if (huellaSemaforo.AfisId > 0)
                        {
                            fechaValidacion = DateTime.Now;
                            repositoryPath = Validaciones.GenerarPathFile(_routes.RepositoryPath, fechaValidacion);
                            pathFile = Validaciones.GenerarPathFileHuellasValidacion(repositoryPath, request.SolicitanteId);
                            data = await DbValidaciones.HuellasValidar(connection, request.SolicitanteId, huellaSemaforo.AfisId, fechaValidacion, huellaSemaforo.ClienteNombreCompleto, pathFile);
                        }
                        else
                        {
                            Resultado resultado = new() { Semaforo = "gris", Mensaje = "", FechaValidacion = null };
                            data.Resultado = resultado;
                        }
                    }
                    else
                    {
                        if (File.Exists(pathFile)) {
                            var streamReader = new StreamReader(pathFile);
                            string jsonString = streamReader.ReadToEnd();
                            streamReader.Dispose();
                            data = JsonConvert.DeserializeObject<SolicitudComparacionHuellas>(jsonString);
                        } else
                        {
                            fechaValidacion = DateTime.Now;
                            repositoryPath = Validaciones.GenerarPathFile(_routes.RepositoryPath, fechaValidacion);
                            pathFile = Validaciones.GenerarPathFileHuellasValidacion(repositoryPath, request.SolicitanteId);
                            data = await DbValidaciones.HuellasValidar(connection, request.SolicitanteId, huellaSemaforo.AfisId, fechaValidacion, huellaSemaforo.ClienteNombreCompleto, pathFile);
                        }
                    }
                    qry = DbSql.QryGetSolicitudHuellas();
                    huellas = (List<SolicitudHuella>)await connection.QueryAsync<SolicitudHuella>(qry, parameters);
                    foreach(var item in huellas)
                    {
                        try
                        {
                            if (item.Imagen != null && item.Imagen.Length > 0) 
                            {
                                var decoder = new WsqDecoder();
                                item.Imagen = decoder.Decode(item.Imagen); // Transforma el formato wsq a imagen
                            }
                            else
                            {
                                item.Imagen = null;
                            }
                        } 
                        catch (Exception ex)
                        {
                            _ = ex.Message;
                            mensaje += "|No se puede convertir el formato de la huella a imagen";
                            item.Imagen = null;
                        }
                    }

                    data.Huellas = huellas;
                    return data;
                }
                else
                    throw Validaciones.ExceptionCampoIncorrecto("SolicitanteId");
            }
            catch (Exception ex)
            {
                mensaje += "|" + ex.Message;
                throw new InvalidOperationException(mensaje);
            }
        }

        public async Task<SolicitudDocumento> GetSolicitudDocumentoAsync(SolicitudDocumentoRequest request)
        {
            string mensaje = "Error en Paquete-GetSolicitudDocumentoAsync";
            try
            {
                if (request.DocumentoId > 0)
                {
                    var parameters = new DynamicParameters();
                    parameters.Add("@DocumentoId", request.DocumentoId);

                    using var connection = _context.CreateConnection();
                    connection.Open();

                    string qry = DbSql.QryGetDocumentoIdxDocumentoId(request.DocumentoId);
                    _ = ConsultarCampoId(connection, qry, "DocumentoId");

                    qry = DbSql.QryGetSolicitudDocumento();
                    SolicitudDocumento data = await connection.QuerySingleOrDefaultAsync<SolicitudDocumento>(qry, parameters);
                    return data;
                }
                else
                    throw Validaciones.ExceptionCampoIncorrecto("DocumentoId");
            }
            catch (Exception ex)
            {
                mensaje += "|" + ex.Message;
                throw new InvalidOperationException(mensaje);
            }
        }

        public async Task<SolicitudAvisoPrivacidad> GetSolicitudAvisoPrivacidadAsync(SolicitudValidacionRequest request)
        {
            string mensaje = "Error en Paquete-GetSolicitudAvisoPrivacidadAsync";
            try
            {
                if (request.SolicitanteId > 0)
                {
                    var parameters = new DynamicParameters();
                    parameters.Add("@SolicitanteId", request.SolicitanteId);

                    using var connection = _context.CreateConnection();
                    connection.Open();

                    string qry = DbSql.QryGetSolicitanteIdxSolicitanteId(request.SolicitanteId);
                    _ = ConsultarCampoId(connection, qry, "SolicitanteId");

                    qry = DbSql.QryGetSolicitudAvisoPrivacidad();
                    SolicitudAvisoPrivacidad data = await connection.QuerySingleOrDefaultAsync<SolicitudAvisoPrivacidad>(qry, parameters);
                    return data;
                }
                else
                    throw Validaciones.ExceptionCampoIncorrecto("SolicitanteId");
            }
            catch (Exception ex)
            {
                mensaje += "|" + ex.Message;
                throw new InvalidOperationException(mensaje);
            }
        }

        public async Task<SolicitudValidacion> GetSolicitudValidacionesAsync(SolicitudValidacionRequest request)
        {
            string mensaje = "Error en Paquete-GetSolicitudValidacionesAsync";
            try
            {
                if (request.SolicitanteId > 0)
                {
                    var parameters = new DynamicParameters();
                    parameters.Add("@SolicitanteId", request.SolicitanteId);

                    using var connection = _context.CreateConnection();
                    connection.Open();

                    string qry = DbSql.QryGetSolicitanteIdxSolicitanteId(request.SolicitanteId);
                    _ = ConsultarCampoId(connection, qry, "SolicitanteId");

                    qry = DbSql.QryGetValidaciones();
                    SolicitudValidacion data = await connection.QuerySingleOrDefaultAsync<SolicitudValidacion>(qry, parameters);
                    return data;
                }
                else
                    throw Validaciones.ExceptionCampoIncorrecto("SolicitanteId");
            }
            catch (Exception ex)
            {
                mensaje += "|" + ex.Message;
                throw new InvalidOperationException(mensaje);
            }
        }

        public async Task<(string, string)> GetSolicitudHuellasByIdAsync(int solicitanteId)
        {
            string mensaje = "Error en Paquete-GetSolicitudHuellasByIdAsync";
            string file;
            string repositoryPath;
            string fileZip;
            string fileWsq;
            bool existenHuellas = false;

            try
            {
                if (solicitanteId > 0)
                {
                    var parameters = new DynamicParameters();
                    parameters.Add("@SolicitanteId", solicitanteId);

                    using var connection = _context.CreateConnection();
                    connection.Open();

                    string qry = DbSql.QryGetFolioxSolicitanteId(solicitanteId);
                    string folio = ConsultarCampoTexto(connection, qry, "SolicitanteId");

                    file = "Huellas" + folio;
                    string repositoryTemp = "Temp";
                    string repositoryPaquete = file;
                    repositoryPath = Path.Combine(_routes.RepositoryPath, repositoryTemp);
                    fileZip = Path.Combine(_routes.RepositoryPath, repositoryTemp) + "\\" + file + ".zip";
                    fileWsq = file + ".wsq";

                    if (!Directory.Exists(repositoryPath))
                        Directory.CreateDirectory(repositoryPath);

                    repositoryPath = Path.Combine(_routes.RepositoryPath, repositoryTemp, repositoryPaquete);

                    if (!Directory.Exists(repositoryPath))
                        Directory.CreateDirectory(repositoryPath);

                    if (File.Exists(fileZip))
                        File.Delete(fileZip);

                    qry = DbSql.QryGetSolicitudHuellas();
                    List<SolicitudHuella> data = (List<SolicitudHuella>)await connection.QueryAsync<SolicitudHuella>(qry, parameters);

                    foreach (var item in data)
                    {
                        fileWsq = repositoryPath + "\\Huella" + item.DedoIndiceId.ToString() + item.DedoEstatusNombre + ".wsq";
                        if (File.Exists(fileWsq))
                            File.Delete(fileWsq);
                        if (item.Imagen != null && item.Imagen.Length > 0)
                        {
                            existenHuellas = true;
                            using var fs = new FileStream(fileWsq, FileMode.Create);
                            fs.Write(item.Imagen, 0, item.Imagen.Length);
                        }
                    }
                    if (!existenHuellas)
                    {
                        fileWsq = repositoryPath + "\\SinHuellas.txt";
                        System.IO.File.WriteAllText(fileWsq, "Sin huellas");
                    }
                    ZipFile.CreateFromDirectory(repositoryPath, fileZip);
                    return (fileZip, file + ".zip");
                }
                else
                    throw Validaciones.ExceptionCampoIncorrecto("SolicitanteId");
            }
            catch (Exception ex)
            {
                mensaje += "|" + ex.Message;
                throw new InvalidOperationException(mensaje);
            }
        }

        public async Task<(int, IEnumerable<SolicitanteEstatus>)> GetSolicitantesEstatusAsync(SolicitudEstatusRequest request, PaginacionRequest paginacion)
        {
            string mensaje = "Error en Paquete-GetSolicitantesEstatusAsync";
            string qry = "";
            string qrySelect;
            int totalRegistros = 0;
            try
            {
                var parameters = DbSql.ParametersSolicitantesEstatus(request);

                string qryFiltro = DbSql.QryGetFiltroSolicitantesEstatus(request.Estatus);

                qrySelect = DbSql.QryGetSolicitantesEstatus(false, qryFiltro);
                if (paginacion == null)
                    qry = qrySelect;
                else
                {
                    string qryCount = DbSql.QryGetCountSolicitantesEstatus(qryFiltro);
                    qry += qryCount + qrySelect + DbSql.QryPaginacion(paginacion);
                }
                using var connection = _context.CreateConnection();
                connection.Open();
                var reader = await connection.QueryMultipleAsync(qry, parameters);
                if (paginacion != null)
                    totalRegistros = reader.Read<int>().FirstOrDefault();
                IEnumerable<SolicitanteEstatus> data = reader.Read<SolicitanteEstatus>().ToList();
                return (totalRegistros, data.ToList());
            }
            catch (Exception ex)
            {
                mensaje += "|" + ex.Message;
                throw new InvalidOperationException(mensaje);
            }
        }

        public async Task<(int, IEnumerable<SolicitudNotificacion>)> GetNotificacionesAsync(PaginacionRequest paginacion)
        {
            string mensaje = "Error en Paquete-GetNotificacionesAsync";
            string qry = "";
            int totalRegistros = 0;
            try
            {
                string qrySelect = DbSql.QryGetNotificacionesSolicitudesNuevas();
                if (paginacion == null)
                    qry = qrySelect;
                else
                {
                    string qryCount = DbSql.QryGetCountNotificacionesSolicitudesNuevas();
                    qry += qryCount + qrySelect + DbSql.QryPaginacion(paginacion);
                }
                using var connection = _context.CreateConnection();
                connection.Open();
                var reader = await connection.QueryMultipleAsync(qry);
                if (paginacion != null)
                    totalRegistros = reader.Read<int>().FirstOrDefault();
                IEnumerable<SolicitudNotificacion> data = reader.Read<SolicitudNotificacion>().ToList();

                foreach (var item in data)
                {
                    item.TiempoRegistro = Validaciones.CalcularTiempoEnvio(item.FechaEnvio);
                }
                return (totalRegistros, data.ToList());
            }
            catch (Exception ex)
            {
                mensaje += "|" + ex.Message;
                throw new InvalidOperationException(mensaje);
            }
        }

        public async Task<int> GetUserIdAsync(string userName, bool regresarError)
        {
            string mensaje = "Error en Paquete-GetUserIdAsync";
            int userId = 0;
            try
            {

                using var connection = _context.CreateConnection();
                connection.Open();

                var parameters = new DynamicParameters();
                parameters.Add("@NombreUsuario", userName);
                string qry = DbSql.QryGetUsuarioIdxNombreUsuario();
                userId = await connection.QuerySingleOrDefaultAsync<int>(qry, parameters);
            }
            catch (Exception ex)
            {
                mensaje += "|" + ex.Message;
                if (regresarError)
                    throw new InvalidOperationException(mensaje);
            }
            return userId;
        }

        public async Task<BitacoraRequest> AddBitacoraAsync(BitacoraRequest bitacora, bool regresarError)
        {
            string mensaje = "Error en AddBitacoraAsync";
            BitacoraRequest data = null;
            try
            {
                using var connection = _context.CreateConnection();
                connection.Open();

                //string qry = DbSql.QryValidarAgenciaIdxUsuarioId(Bitacora.UsuarioId);
                //int usuarioId = ConsultarCampoId(connection, qry, "UsuarioId");

                bitacora.FechaRegistro = DateTime.Now;
                var parameters = DbSql.ParametersBitacora(bitacora);
                string qry = DbSql.QryAddBitacora();
                int id = await connection.QuerySingleAsync<int>(qry, parameters);
                bitacora.BitacoraId = id;
                data = bitacora;
            }
            catch (Exception ex)
            {
                mensaje += "|" + ex.Message;
                if (regresarError)
                    throw new InvalidOperationException(mensaje);
            }
            return data;
        }

        public async Task<bool> UpdateValidacionAsync(ValidationResult validation, bool regresarError)
        {
            string mensaje = "Error en Paquete-UpdateValidacionAsync";
            bool response = false;
            try
            {
                (string campoSemaforo, string campoResultado, string campoFecha) = Validaciones.ComprobarTipoValidacion(validation.TipoValidacion);

                using var connection = _context.CreateConnection();
                connection.Open();

                //string qry = DbSql.QryValidarValidacionIdxValidacionId(validation.ValidacionId);
                //int validacionId = ConsultarCampoId(connection, qry, "ValidacionId");

                var parameters = DbSql.ParametersUpdateValidacion(validation);
                string qry = DbSql.QryUpdateValidacion(campoSemaforo, campoResultado, campoFecha);
                int id = await connection.ExecuteAsync(qry, parameters);
                if (id > 0)
                    response = true;
            }
            catch (Exception ex)
            {
                mensaje += "|" + ex.Message;
                if (regresarError)
                    throw new InvalidOperationException(mensaje);
            }
            return response;
        }

        public void IniciarValidacionDatosExternos(int validacionId, int solicitanteId, bool identificacionIne, int usuarioId)
        {
            Task.Run(async () =>
            {
                await ConsultarCurp(validacionId, solicitanteId, usuarioId);
            });
            Task.Run(async () =>
            {
                await ConsultarCorreo(validacionId, solicitanteId, usuarioId);
            });
            Task.Run(async () =>
            {
                await ConsultarTelefono(validacionId, solicitanteId, usuarioId);
            });
            Task.Run(async () =>
            {
                await ConsultarListasInteres(validacionId, solicitanteId, usuarioId);
            });
            if (identificacionIne)
            {
                Task.Run(async () =>
                {
                    await ConsultarIne(validacionId, solicitanteId, usuarioId);
                });
            }
        }

        public async Task IniciarValidacionDatosExternosAsync(int validacionId, int solicitanteId, bool identificacionIne, int usuarioId)
        {
            await ConsultarCurp(validacionId, solicitanteId, usuarioId);
            await ConsultarCorreo(validacionId, solicitanteId, usuarioId);
            await ConsultarTelefono(validacionId, solicitanteId, usuarioId);
            await ConsultarListasInteres(validacionId, solicitanteId, usuarioId);
            if (identificacionIne)
                await ConsultarIne(validacionId, solicitanteId, usuarioId);
        }

        public void IniciarValidacionDocumentosExternos(int validacionId, int solicitanteId, int usuarioId, string tipoComprobante)
        {
            Task.Run(async () =>
            {
                await ConsultarComprobante(validacionId, solicitanteId, usuarioId, tipoComprobante);
            });
        }

        public async Task IniciarValidacionDocumentosExternosAsync(int validacionId, int solicitanteId, int usuarioId)
        {
            string mensaje = "Error en Paquete-IniciarValidacionDocumentosExternosAsync";

            try
            {
                using var connection = _context.CreateConnection();
                connection.Open();
                var parameters = new DynamicParameters();
                parameters.Add("@SolicitanteId", solicitanteId);

                string qry = DbSql.QryGetDocumentosCargadosFicha();

                List<SolicitanteDocumentoCargado> documentosCargados = (List<SolicitanteDocumentoCargado>)await connection.QueryAsync<SolicitanteDocumentoCargado>(qry, parameters);

                foreach (var item in documentosCargados)
                {
                    await ConsultarComprobante(validacionId, solicitanteId, usuarioId, item.TipoDocumentoId);
                }
            }
            catch (Exception ex)
            {
                mensaje += "|" + ex.Message;
                throw new InvalidOperationException(mensaje);
            }
        }



        private static int ValidarSemaforoIdentificacion(IDbConnection connection, DynamicParameters parameters)
        {
            int id = 0;
            string qry = DbSql.QryGetSolicitantesResultadoGeneralIdentificacion();
            string resultadoGeneral = connection.QuerySingleOrDefault<string>(qry, parameters);

            if (!string.IsNullOrEmpty(resultadoGeneral))
            {
                (string semaforoIdentificacion, string resultadoIdentificacion) = Validaciones.CalcularSemaforoIdentificacion(resultadoGeneral);
                parameters.Add("@Semaforo", semaforoIdentificacion);
                parameters.Add("@Resultado", resultadoIdentificacion);
                parameters.Add("@FechaValidacion", DateTime.Now);
                qry = DbSql.QryUpdateSemaforoIdentificacion();
                id = connection.Execute(qry, parameters);
            }
            return id;
        }

        private static int ValidarSemaforoComparacionFacial(IDbConnection connection, DynamicParameters parameters)
        {
            int id = 0;
            string qry = DbSql.QryGetSemaforoComparacionFacial();
            string score = connection.QuerySingleOrDefault<string>(qry, parameters);

            if (!string.IsNullOrEmpty(score))
            {
                (string semaforoFacial, string resultadoFacial) = Validaciones.CalcularSemaforoComparacionFacial(score);

                parameters.Add("@Semaforo", semaforoFacial);
                parameters.Add("@Resultado", resultadoFacial);
                parameters.Add("@FechaValidacion", DateTime.Now);
                qry = DbSql.QryUpdateSemaforoComparacionFacial();
                id = connection.Execute(qry, parameters);
            }
            return id;
        }

        private static int ConsultarCampoId(IDbConnection connection, string qry, string nombreCampo)
        {
            int id = connection.QuerySingleOrDefault<int>(qry);
            if (id > 0)
                return id;

            throw Validaciones.ExceptionCampoIncorrecto(nombreCampo);
        }

        private static string ConsultarCampoTexto(IDbConnection connection, string qry, string nombreCampo)
        {
            string texto = connection.QuerySingleOrDefault<string>(qry);
            if (!string.IsNullOrEmpty(texto))
                return texto;

            throw Validaciones.ExceptionCampoIncorrecto(nombreCampo);
        }

        private static Solicitante RequestSolicitante(SolicitanteRequest request)
        {
            Solicitante solicitante = new()
            {
                SolicitanteId = request.SolicitanteId, 
                UsuarioId = request.UsuarioId,
                Nombre = request.Nombre,
                ApellidoPaterno = request.Paterno,
                ApellidoMaterno = request.Materno,
                FechaNacimiento = string.IsNullOrEmpty(request.FechaDeNacimiento) ? null : Convert.ToDateTime(request.FechaDeNacimiento),
                Curp = request.Curp,
                CorreoElectronico = request.CorreoElectronico,
                TipoCliente = request.TipoCliente, 
                Estatus = "Nueva", 
                Folio = request.Folio, 
                TipoDocumento = request.TipoDeDocumento,
                NumeroDocumento = request.NumeroDeDocumento,
                Guid = request.Guid,
                Telefono = request.Telefono,
                Sexo = request.Sexo,
                ResultadoGeneral = request.ResultadoGeneral,
                LugarNacimiento = request.LugarDeNacimiento,
                CoordenadasGps = request.CoordenadasGps,
                ScoreComparacionFacial = request.ScoreDeLaComparacionFacial,
                ResultadoComparacionFacial = request.ResultadoDeLaComparacionFacial,
                Nacionalidad = request.Nacionalidad,
                DireccionCompleta = request.DireccionCompleta,
                CodigoPostal = request.CodigoPostal,
                CalleNumero = request.CalleNumero,
                Colonia = request.Colonia,
                Municipio = request.Municipio,
                Estado = request.Estado,
                PruebaVida = request.PruebaDeVida,
                Edad = request.Edad,
                NombreCompletoSolicitante = request.NombreCompletoDelSolicitante,
                FechaRegistro = request.FechaDeRegistro,
                DocumentoPdf = request.DocumentoPdfBase64
            };
            return solicitante;
        }

        private static Identificacion RequestIdentificacion(SolicitanteRequest request, bool identificacionIne)
        {
            if (string.IsNullOrEmpty(request.Serie)
                && string.IsNullOrEmpty(request.NumeroEmision)
                && string.IsNullOrEmpty(request.CIC)
                && string.IsNullOrEmpty(request.OCR)
                && string.IsNullOrEmpty(request.ClaveElector)
                && string.IsNullOrEmpty(request.IdentificadorCiudadano)
                && string.IsNullOrEmpty(request.Vigencia)
                && string.IsNullOrEmpty(request.AnioRegistro)
                && string.IsNullOrEmpty(request.Emision)
                && string.IsNullOrEmpty(request.Mrz)
                && string.IsNullOrEmpty(request.TipoIne)
                )
                return null;

            string modeloIne = string.Empty;
            string identificadorCiudadano = request.IdentificadorCiudadano;
            string ocr = request.OCR;
            string numeroEmision = request.NumeroEmision;

            if (identificacionIne)
            {
                switch (request.Serie)
                {
                    case "2001":
                        modeloIne = "B";
                        break;
                    case "2004":
                        modeloIne = "C";
                        ocr = request.IdentificadorCiudadano;
                        numeroEmision = request.AnioRegistro;
                        break;
                    case "2013":
                        modeloIne = "D";
                        break;
                    case "2014":
                    case "2015":
                    case "2019":
                        modeloIne = "E";
                        int longitudOCR = request.OCR.Length;
                        if (longitudOCR >= 5)
                            identificadorCiudadano = request.OCR[4..longitudOCR];
                        else
                            identificadorCiudadano = null;
                        break;
                    default:
                        modeloIne = "";
                        break;
                }
            }

            Identificacion identificacion = new()
            {
                Serie = request.Serie,
                NumeroEmision = numeroEmision,
                Cic = request.CIC,
                Ocr = ocr,
                ClaveElector = request.ClaveElector,
                IdentificadorCiudadano = identificadorCiudadano,
                Vigencia = request.Vigencia,
                AnioRegistro = request.AnioRegistro,
                Emision = request.Emision,
                Mrz = request.Mrz,
                FechaRegistro = request.FechaDeRegistro,
                TipoIne = modeloIne
            };
            return identificacion;
        }

        private static CapturaIdentificacion RequestCapturaIdentificacion1(int usuarioId, DateTime fechaDeRegistro, byte[] capturaFrenteEnBase64)
        {
            if (capturaFrenteEnBase64 == null || capturaFrenteEnBase64.Length == 0)
                return null;

            CapturaIdentificacion capturaIdentificacion = new ()
            {
                Imagen = capturaFrenteEnBase64,
                CapturaNombreId = 1,
                UsuarioId = usuarioId,
                FechaRegistro = fechaDeRegistro
            };
            return capturaIdentificacion;
        }

        private static CapturaIdentificacion RequestCapturaIdentificacion4(int usuarioId, DateTime fechaDeRegistro, byte[] capturaReversoEnBase64)
        {
            if (capturaReversoEnBase64 == null || capturaReversoEnBase64.Length == 0)
                return null;

            CapturaIdentificacion capturaIdentificacion = new()
            {
                Imagen = capturaReversoEnBase64,
                CapturaNombreId = 4,
                UsuarioId = usuarioId,
                FechaRegistro = fechaDeRegistro
            };
            return capturaIdentificacion;
        }

        private static Foto RequestFoto1(int usuarioId, DateTime fechaDeRegistro, byte[] fotoEnBase64, byte[] selfieBase64)
        {
            byte[] fotoBase64 = fotoEnBase64;
            if (fotoEnBase64 == null || fotoEnBase64.Length == 0)
            {
                fotoBase64 = selfieBase64;
                if (selfieBase64 == null || selfieBase64.Length == 0) 
                    return null;
            }

            Foto foto = new()
            {
                Imagen = fotoBase64,
                FotoOrigenId = 1,
                UsuarioId = usuarioId,
                FechaRegistro = fechaDeRegistro
            };
            return foto;
        }

        private static Foto RequestFoto2(int usuarioId, DateTime fechaDeRegistro, byte[] fotoDeIdentificacionEnBase64)
        {
            if (fotoDeIdentificacionEnBase64 == null || fotoDeIdentificacionEnBase64.Length == 0)
                return null;
            Foto foto = new()
            {
                Imagen = fotoDeIdentificacionEnBase64,
                FotoOrigenId = 2,
                UsuarioId = usuarioId,
                FechaRegistro = fechaDeRegistro
            };
            return foto;
        }

        private static Huella RequestHuella(HuellaRequest request)
        {
            Huella huella = new()
            {
                SolicitanteId = request.SolicitanteId,
                DedoIndiceId = request.DedoId,
                DedoEstatusId = request.OmisionId,
                Imagen = request.HuellaBase64,
                UsuarioId = request.UsuarioId,
                FechaRegistro = request.FechaDeRegistro
            };
            return huella;
        }

        private static Documento RequestDocumento(DocumentoRequest request)
        {
            if (request.DocumentoBase64 == null || request.DocumentoBase64.Length == 0)
                return null;

            Documento documento = new()
            {
                SolicitanteId = request.SolicitanteId,
                TipoDocumento = request.TipoDocumento,
                NombreDocumento = request.NombreDocumento, 
                Imagen = request.DocumentoBase64,
                UsuarioId = request.UsuarioId,
                FechaRegistro = request.FechaDeRegistro
            };
            return documento;
        }

        private static AvisoPrivacidad RequestDocumentoPrivacidad(AvisoPrivacidadRequest request)
        {
            if (request.DocumentoBase64 == null || request.DocumentoBase64.Length == 0)
                return null;

            AvisoPrivacidad privacidad = new()
            {
                SolicitanteId = request.SolicitanteId,
                Referencia = request.Referencia,
                Imagen = request.DocumentoBase64,
                UsuarioId = request.UsuarioId,
                FechaRegistro = request.FechaDeRegistro
            };
            return privacidad;
        }

        private static ListaNegra RequestListaNegra(ListaNegraRequest request)
        {
            var lista = new ListaNegra()
            {
                SolicitanteId = request.SolicitanteId,
                Motivo = request.Motivo,
                TipoMovimientoId = request.TipoMovimientoId,
                UsuarioId = request.UsuarioId,
            };
            return lista;
        }

        private static Resolucion RequestResolucion(ResolucionRequest request)
        {
            var resolucion = new Resolucion()
            {
                SolicitanteId = request.SolicitanteId,
                Comentario = request.Comentario,
                TipoResolucionId = request.TipoResolucionId,
                UsuarioId = request.UsuarioId,
            };
            return resolucion;
        }

        private async Task ConsultarCurp(int validacionId, int solicitanteId, int usuarioId)
        {
            var request = new CurpRequest() { ValidacionId = validacionId, ResultadoCurp = "0" };
            string objectSerialize = JsonConvert.SerializeObject(request);
            (string mensajeResponse, CurpResponse curpResponse, ValidationResult validationResult) validacion;
            validacion = await _validacionService.ValidarCurpAsync(objectSerialize);
            if (!string.IsNullOrEmpty(validacion.mensajeResponse))
            {
                var bitacora = new BitacoraRequest() { OrigenId = _origenId, TipoLogId = _tipoLogIdError, UsuarioId = usuarioId, Mensaje = validacion.mensajeResponse, Referencia = $"ValidacionCurp|{solicitanteId}|" + objectSerialize };
                await AddBitacoraAsync(bitacora, false);

                if (validacion.validationResult != null)
                    await UpdateValidacionAsync(validacion.validationResult, false);
            }

        }

        public async Task<ComparacionfacialResponse> ComparacionFacial(ComparacionfacialRequest request)
        {
            string mensaje = "Error en ComparacionFacial";
            try
            {
                faceservice ser = new faceservice();
                ser.workflow = new workflow();
                ser.workflow.comparator = new comparator
                {
                    algorithm = "F500",
                    face_types = new List<string> { "VISIBLE_FRONTAL" }
                };
                ser.gallery = new gallery
                {
                    VISIBLE_FRONTAL = request.ImagenCredencial
                };
                ser.probe = new probe
                {
                    VISIBLE_FRONTAL = request.ImagenCamara
                };

                string objectSerialize = JsonConvert.SerializeObject(ser);
                (string mensajeResponse, ComparacionfacialResponse response) validacion;
                validacion = await _comparacionfacialserviceService.ValidarComparacionFacial(objectSerialize);

                return validacion.response;
            }
            catch (Exception ex)
            {
                mensaje += "|" + ex.Message;
                throw new InvalidOperationException(mensaje);
            }
        }


        private async Task ConsultarCorreo(int validacionId, int solicitanteId, int usuarioId)
        {
            var request = new CorreoRequest() { ValidacionId = validacionId, ResultadoCorreo = "0" };
            string objectSerialize = JsonConvert.SerializeObject(request);
            (string mensajeResponse, CorreoResponse correoResponse, ValidationResult validationResult) validacion;
            validacion = await _validacionService.ValidarCorreoAsync(objectSerialize);
            if (!string.IsNullOrEmpty(validacion.mensajeResponse))
            {
                var bitacora = new BitacoraRequest() { OrigenId = _origenId, TipoLogId = _tipoLogIdError, UsuarioId = usuarioId, Mensaje = validacion.mensajeResponse, Referencia = $"ValidacionCorreo|{solicitanteId}|" + objectSerialize };
                await AddBitacoraAsync(bitacora, false);

                if (validacion.validationResult != null)
                    await UpdateValidacionAsync(validacion.validationResult, false);
            }
        }

        private async Task ConsultarTelefono(int validacionId, int solicitanteId, int usuarioId)
        {
            var request = new TelefonoRequest() { ValidacionId = validacionId, ResultadoTelefono = "0" };
            string objectSerialize = JsonConvert.SerializeObject(request);
            (string mensajeResponse, TelefonoResponse telefonoResponse, ValidationResult validationResult) validacion;
            validacion = await _validacionService.ValidarTelefonoAsync(objectSerialize);
            if (!string.IsNullOrEmpty(validacion.mensajeResponse))
            {
                var bitacora = new BitacoraRequest() { OrigenId = _origenId, TipoLogId = _tipoLogIdError, UsuarioId = usuarioId, Mensaje = validacion.mensajeResponse, Referencia = $"ValidacionTelefono|{solicitanteId}|" + objectSerialize };
                await AddBitacoraAsync(bitacora, false);

                if (validacion.validationResult != null)
                    await UpdateValidacionAsync(validacion.validationResult, false);
            }
        }

        private async Task ConsultarListasInteres(int validacionId, int solicitanteId, int usuarioId)
        {
            var request = new ListaRequest() { ValidacionId = validacionId, ResultadoListaAml = "0" };
            string objectSerialize = JsonConvert.SerializeObject(request);
            (string mensajeResponse, ListaResponse listaResponse, ValidationResult validationResult) validacion;
            validacion = await _validacionService.ValidarListasAsync(objectSerialize);
            if (!string.IsNullOrEmpty(validacion.mensajeResponse))
            {
                var bitacora = new BitacoraRequest() { OrigenId = _origenId, TipoLogId = _tipoLogIdError, UsuarioId = usuarioId, Mensaje = validacion.mensajeResponse, Referencia = $"ValidacionListasInteres|{solicitanteId}|" + objectSerialize };
                await AddBitacoraAsync(bitacora, false);

                if (validacion.validationResult != null)
                    await UpdateValidacionAsync(validacion.validationResult, false);
            }
        }

        private async Task ConsultarIne(int validacionId, int solicitanteId, int usuarioId)
        {
            var request = new IneRequest() { ValidacionId = validacionId, ResultadoIne = "0" };
            string objectSerialize = JsonConvert.SerializeObject(request);
            (string mensajeResponse, IneResponse ineResponse, ValidationResult validationResult) validacion;
            validacion = await _validacionService.ValidarIneAsync(objectSerialize);
            if (!string.IsNullOrEmpty(validacion.mensajeResponse))
            {
                var bitacora = new BitacoraRequest() { OrigenId = _origenId, TipoLogId = _tipoLogIdError, UsuarioId = usuarioId, Mensaje = validacion.mensajeResponse, Referencia = $"ValidacionIne|{solicitanteId}|" + objectSerialize };
                await AddBitacoraAsync(bitacora, false);

                if (validacion.validationResult != null)
                    await UpdateValidacionAsync(validacion.validationResult, false);
            }
        }

        private async Task ConsultarComprobante(int validacionId, int solicitanteId, int usuarioId, string tipoComprobante)
        {
            var request = new ComprobanteRequest() { Validacion_Id = validacionId, Revalidar = "0", Tipo_Comprobante = tipoComprobante };
            string objectSerialize = JsonConvert.SerializeObject(request);
            (string mensajeResponse, ComprobanteResponse comprobanteResponse, ValidationResult validationResult) validacion;
            validacion = await _validacionService.ValidarComprobanteAsync(objectSerialize);
            if (!string.IsNullOrEmpty(validacion.mensajeResponse))
            {
                var bitacora = new BitacoraRequest() { OrigenId = _origenId, TipoLogId = _tipoLogIdError, UsuarioId = usuarioId, Mensaje = validacion.mensajeResponse, Referencia = $"ValidacionDocumento{tipoComprobante}|{solicitanteId}|" + objectSerialize };
                await AddBitacoraAsync(bitacora, false);

                if (validacion.validationResult != null)
                {
                    TipoDocumento tipoDocumento = (TipoDocumento)Convert.ToInt32(tipoComprobante);

                    TipoSemaforo tipoValidacion = tipoDocumento switch 
                    {
                        TipoDocumento.DocumentoIngresos => TipoSemaforo.ComprobanteIngresos,
                        TipoDocumento.DocumentoDomicilio => TipoSemaforo.ComprobanteDomicilio,
                        TipoDocumento.DocumentoBancario => TipoSemaforo.ComprobanteBancario,
                        _ => TipoSemaforo.Desconocido
                    };

                    validacion.validationResult.TipoValidacion = tipoValidacion;
                    await UpdateValidacionAsync(validacion.validationResult, false);
                }
            }
        }

    }
}
