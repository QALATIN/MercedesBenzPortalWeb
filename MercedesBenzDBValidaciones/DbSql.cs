using Dapper;
using MercedesBenzModel;

namespace MercedesBenzDBValidaciones
{
    public static class DbSql
    {
        #region Consultas para obtener un Id o clave

        public static string QryGetAgenciaIdxAgenciaId(int agenciaId) => $"SELECT Agencia_Id FROM Agencias WHERE Agencia_Id = {agenciaId}; ";

        public static string QryGetAgenciaIdxUsuarioId(int usuarioId) => $"SELECT Agencia_Id FROM Usuarios WHERE Activo = true AND Usuario_Id = {usuarioId}; ";

        public static string QryGetDocumentoIdxDocumentoId(int documentoId) => $"SELECT Documento_Id FROM Documentos WHERE Activo = true AND Documento_Id = {documentoId}; ";

        public static string QryGetFolioxSolicitanteId(int solicitanteId) => $"SELECT Folio FROM Solicitantes WHERE Activo = true AND Solicitante_Id = {solicitanteId}; ";

        public static string QryGetNombreClientexSolicitanteId(int solicitanteId) => $"SELECT Nombre || ' ' || Apellido_Paterno || ' ' || Apellido_Materno NombreCompleto FROM Solicitantes WHERE Activo = true AND Solicitante_Id = {solicitanteId}; ";

        public static string QryGetSolicitanteIdxSolicitanteId(int solicitanteId) => $"SELECT Solicitante_Id FROM Solicitantes WHERE Activo = true AND Solicitante_Id = {solicitanteId}; ";

        public static string QryGetValidacionIdxSolicitanteId(int solicitanteId) => $"SELECT Validacion_Id FROM Validaciones WHERE Validacion_Id = {solicitanteId}; ";

        public static string QryGetValidacionIdxValidacionId(int validacionId) => $"SELECT Validacion_Id FROM Validaciones WHERE Validacion_Id = {validacionId}; ";

        public static string QryGetUsuarioIdExistexUsuarioId(int usuarioId) => $"SELECT Usuario_Id FROM Usuarios WHERE Usuario_Id = {usuarioId}; ";

        public static string QryGetUsuarioIdxUsuarioId(int usuarioId) => $"SELECT Usuario_Id FROM Usuarios WHERE Usuario_Id = {usuarioId} AND Activo = true; ";

        public static string QryGetUsuarioIdxNombreUsuario() => "SELECT Usuario_Id FROM Usuarios WHERE Nombre_Usuario = @NombreUsuario AND Activo = true; ";

        public static string QryGetUsuarioIdxCorreo() => "SELECT Usuario_Id FROM Usuarios WHERE Correo_Electronico = @Correo AND Activo = true; ";

        #endregion

        #region Definición de Parametros

        public static DynamicParameters ParametersUsuarios(Usuario usuario)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@UsuarioId", usuario.UsuarioId);
            parameters.Add("@NombreUsuario", usuario.NombreUsuario);
            parameters.Add("@Password", "4NS<;wZm0)Mj:I/DD–2RRw.U.?WpmZ<m");
            parameters.Add("@Nombre", usuario.Nombre);
            parameters.Add("@ApellidoPaterno", usuario.ApellidoPaterno);
            parameters.Add("@ApellidoMaterno", usuario.ApellidoMaterno);
            parameters.Add("@FechaNacimiento", usuario.FechaNacimiento);
            parameters.Add("@CorreoElectronico", usuario.CorreoElectronico);
            parameters.Add("@PerfilId", usuario.PerfilId);
            parameters.Add("@AgenciaId", usuario.AgenciaId);
            parameters.Add("@ActivarCuenta", usuario.ActivarCuenta);
            parameters.Add("@Activo", usuario.Activo);
            parameters.Add("@FechaRegistro", usuario.FechaRegistro);
            parameters.Add("@FechaBaja", usuario.FechaBaja);
            parameters.Add("@NombreCompleto", usuario.NombreCompleto);

            return parameters;
        }

        public static DynamicParameters ParametersUsuariosUpdateEstatus(UsuarioEstatusRequest usuario)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@Activar", usuario.Activar);
            parameters.Add("@UsuarioId", usuario.UsuarioId);
            parameters.Add("@Motivo", usuario.Motivo);
            parameters.Add("@UsuarioIdActualizo", usuario.UsuarioIdActualizo);
            parameters.Add("@FechaRegistro", usuario.FechaRegistro);
            return parameters;
        }

        public static DynamicParameters ParametersAgencias(Agencia agencia)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@AgenciaId", agencia.AgenciaId);
            parameters.Add("@ClaveAgencia", agencia.ClaveAgencia);
            parameters.Add("@NombreAgencia", agencia.NombreAgencia);
            parameters.Add("@Telefono", agencia.Telefono);
            parameters.Add("@EstadoId", agencia.EstadoId);
            parameters.Add("@Direccion", agencia.Direccion);
            parameters.Add("@TipoAgenciaId", agencia.TipoAgenciaId);
            parameters.Add("@FechaRegistro", agencia.FechaRegistro);
            parameters.Add("@FechaBaja", agencia.FechaBaja);
            return parameters;
        }

        public static DynamicParameters ParametersAgenciasUpdateEstatus(AgenciaEstatusRequest agencia)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@Activar", agencia.Activar);
            parameters.Add("@AgenciaId", agencia.AgenciaId);
            parameters.Add("@Motivo", agencia.Motivo);
            parameters.Add("@UsuarioIdActualizo", agencia.UsuarioIdActualizo);
            parameters.Add("@FechaRegistro", agencia.FechaRegistro);
            return parameters;
        }

        public static DynamicParameters ParametersSolicitantes(Solicitante solicitante)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@SolicitanteId", solicitante.SolicitanteId);
            parameters.Add("@SolicitanteIdOrigen", solicitante.SolicitanteIdOrigen);
            parameters.Add("@UsuarioId", solicitante.UsuarioId);
            parameters.Add("@AgenciaId", solicitante.AgenciaId);
            parameters.Add("@Nombre", solicitante.Nombre);
            parameters.Add("@ApellidoPaterno", solicitante.ApellidoPaterno);
            parameters.Add("@ApellidoMaterno", solicitante.ApellidoMaterno);
            parameters.Add("@FechaNacimiento", solicitante.FechaNacimiento);
            parameters.Add("@Curp", solicitante.Curp);
            parameters.Add("@CorreoElectronico", solicitante.CorreoElectronico);
            parameters.Add("@TipoCliente", solicitante.TipoCliente);
            parameters.Add("@Estatus", solicitante.Estatus);
            parameters.Add("@Folio", solicitante.Folio);
            parameters.Add("@TipoDocumento", solicitante.TipoDocumento);
            parameters.Add("@NumeroDocumento", solicitante.NumeroDocumento);
            parameters.Add("@Guid", solicitante.Guid);
            parameters.Add("@Telefono", solicitante.Telefono);
            parameters.Add("@Sexo", solicitante.Sexo);
            parameters.Add("@ResultadoGeneral", solicitante.ResultadoGeneral);
            parameters.Add("@LugarNacimiento", solicitante.LugarNacimiento);
            parameters.Add("@CoordenadasGps", solicitante.CoordenadasGps);
            parameters.Add("@ScoreComparacionFacial", solicitante.ScoreComparacionFacial);
            parameters.Add("@ResultadoComparacionFacial", solicitante.ResultadoComparacionFacial);
            parameters.Add("@Nacionalidad", solicitante.Nacionalidad);
            parameters.Add("@DireccionCompleta", solicitante.DireccionCompleta);
            parameters.Add("@CodigoPostal", solicitante.CodigoPostal);
            parameters.Add("@CalleNumero", solicitante.CalleNumero);
            parameters.Add("@Colonia", solicitante.Colonia);
            parameters.Add("@Municipio", solicitante.Municipio);
            parameters.Add("@Estado", solicitante.Estado);
            parameters.Add("@PruebaVida", solicitante.PruebaVida);
            parameters.Add("@Edad", solicitante.Edad);
            parameters.Add("@NombreCompletoSolicitante", solicitante.NombreCompletoSolicitante);
            parameters.Add("@FechaEnvio", solicitante.FechaEnvio);
            parameters.Add("@FechaRegistro", solicitante.FechaRegistro);
            parameters.Add("@FechaBaja", solicitante.FechaBaja);
            parameters.Add("@Activo", solicitante.Activo);
            parameters.Add("@DocumentoPdf", solicitante.DocumentoPdf);
            return parameters;
        }

        public static DynamicParameters ParametersIdentificaciones(Identificacion identificacion)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@IdentificacionId", identificacion.IdentificacionId);
            parameters.Add("@SolicitanteId", identificacion.SolicitanteId);
            parameters.Add("@Serie", identificacion.Serie);
            parameters.Add("@NumeroEmision", identificacion.NumeroEmision);
            parameters.Add("@Cic", identificacion.Cic);
            parameters.Add("@Ocr", identificacion.Ocr);
            parameters.Add("@ClaveElector", identificacion.ClaveElector);
            parameters.Add("@IdentificadorCiudadano", identificacion.IdentificadorCiudadano);
            parameters.Add("@Vigencia", identificacion.Vigencia);
            parameters.Add("@AnioRegistro", identificacion.AnioRegistro);
            parameters.Add("@Emision", identificacion.Emision);
            parameters.Add("@Mrz", identificacion.Mrz);
            parameters.Add("@FechaEnvio", identificacion.FechaEnvio);
            parameters.Add("@FechaRegistro", identificacion.FechaRegistro);
            parameters.Add("@FechaBaja", identificacion.FechaBaja);
            parameters.Add("@Activo", identificacion.Activo);
            parameters.Add("@TipoIne", identificacion.TipoIne);
            return parameters;
        }

        public static DynamicParameters ParametersCapturaIdentificaciones(CapturaIdentificacion identificacion)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@CapturaIdentificacionId", identificacion.CapturaIdentificacionId);
            parameters.Add("@SolicitanteId", identificacion.SolicitanteId);
            parameters.Add("@Imagen", identificacion.Imagen);
            parameters.Add("@CapturaNombreId", identificacion.CapturaNombreId);
            parameters.Add("@UsuarioId", identificacion.UsuarioId);
            parameters.Add("@FechaEnvio", identificacion.FechaEnvio);
            parameters.Add("@FechaRegistro", identificacion.FechaRegistro);
            parameters.Add("@FechaBaja", identificacion.FechaBaja);
            parameters.Add("@Activo", identificacion.Activo);
            return parameters;
        }

        public static DynamicParameters ParametersFotos(Foto foto)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@FotoId", foto.FotoId);
            parameters.Add("@SolicitanteId", foto.SolicitanteId);
            parameters.Add("@Imagen", foto.Imagen);
            parameters.Add("@FotoOrigenId", foto.FotoOrigenId);
            parameters.Add("@Guid", foto.Guid);
            parameters.Add("@UsuarioId", foto.UsuarioId);
            parameters.Add("@FechaEnvio", foto.FechaEnvio);
            parameters.Add("@FechaRegistro", foto.FechaRegistro);
            parameters.Add("@FechaBaja", foto.FechaBaja);
            parameters.Add("@Activo", foto.Activo);
            return parameters;
        }

        public static DynamicParameters ParametersResolucion(Resolucion resolucion)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@ResolucionId", resolucion.ResolucionId);
            parameters.Add("@SolicitanteId", resolucion.SolicitanteId);
            parameters.Add("@Comentario", resolucion.Comentario);
            parameters.Add("@TipoResolucionId", resolucion.TipoResolucionId);
            parameters.Add("@UsuarioId", resolucion.UsuarioId);
            parameters.Add("@FechaRegistro", resolucion.FechaRegistro);
            return parameters;
        }

        public static DynamicParameters ParametersValidacion(SolicitudValidacion validacion)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@ValidacionId", validacion.ValidacionId);
            parameters.Add("@SolicitanteId", validacion.SolicitanteId);
            parameters.Add("@SemaforoListaNegra", validacion.SemaforoListaNegra);
            parameters.Add("@ResultadoListaNegra", validacion.ResultadoListaNegra);
            parameters.Add("@FechaListaNegra", validacion.FechaListaNegra);
            parameters.Add("@SemaforoIBMS", validacion.SemaforoIBMS);
            parameters.Add("@ResultadoIBMS", validacion.ResultadoIBMS);
            parameters.Add("@FechaIBMS", validacion.FechaIBMS);

            parameters.Add("@SemaforoIdentificacion", validacion.SemaforoIdentificacion);
            parameters.Add("@ResultadoIdentificacion", validacion.ResultadoIdentificacion);
            parameters.Add("@FechaIdentificacion", validacion.FechaIdentificacion);
            parameters.Add("@SemaforoFacial", validacion.SemaforoFacial);
            parameters.Add("@ResultadoFacial", validacion.ResultadoFacial);
            parameters.Add("@FechaFacial", validacion.FechaFacial);

            parameters.Add("@SemaforoIne", validacion.SemaforoIne);
            parameters.Add("@ResultadoIne", validacion.ResultadoIne);
            parameters.Add("@FechaIne", validacion.FechaIne);
            parameters.Add("@SemaforoCorreo", validacion.SemaforoCorreo);
            parameters.Add("@ResultadoCorreo", validacion.ResultadoCorreo);
            parameters.Add("@FechaCorreo", validacion.FechaCorreo);
            parameters.Add("@SemaforoTelefono", validacion.SemaforoTelefono);
            parameters.Add("@ResultadoTelefono", validacion.ResultadoTelefono);
            parameters.Add("@FechaTelefono", validacion.FechaTelefono);
            parameters.Add("@SemaforoCurp", validacion.SemaforoCurp);
            parameters.Add("@ResultadoCurp", validacion.ResultadoCurp);
            parameters.Add("@FechaCurp", validacion.FechaCurp);
            parameters.Add("@SemaforoComprobanteDomicilio", validacion.SemaforoComprobanteDomicilio);
            parameters.Add("@ResultadoComprobanteDomicilio", validacion.ResultadoComprobanteDomicilio);
            parameters.Add("@FechaComprobanteDomicilio", validacion.FechaComprobanteDomicilio);
            parameters.Add("@SemaforoComprobanteIngresos", validacion.SemaforoComprobanteIngresos);
            parameters.Add("@ResultadoComprobanteIngresos", validacion.ResultadoComprobanteIngresos);
            parameters.Add("@FechaComprobanteIngresos", validacion.FechaComprobanteIngresos);
            parameters.Add("@SemaforoListaAml", validacion.SemaforoListaAml);
            parameters.Add("@ResultadoListaAml", validacion.ResultadoListaAml);
            parameters.Add("@FechaListaAml", validacion.FechaListaAml);
            return parameters;
        }

        public static DynamicParameters ParametersHuellas(Huella huella)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@HuellaId", huella.HuellaId);
            parameters.Add("@SolicitanteId", huella.SolicitanteId);
            parameters.Add("@DedoIndiceId", huella.DedoIndiceId);
            parameters.Add("@DedoEstatusId", huella.DedoEstatusId);
            parameters.Add("@Imagen", huella.Imagen);
            parameters.Add("@UsuarioId", huella.UsuarioId);
            parameters.Add("@FechaEnvio", huella.FechaEnvio);
            parameters.Add("@FechaRegistro", huella.FechaRegistro);
            parameters.Add("@FechaBaja", huella.FechaBaja);
            parameters.Add("@Activo", huella.Activo);
            return parameters;
        }

        public static DynamicParameters ParametersAvisosPrivacidad(AvisoPrivacidad privacidad)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@AvisoPrivacidadId", privacidad.AvisoPrivacidadId);
            parameters.Add("@SolicitanteId", privacidad.SolicitanteId);
            parameters.Add("@Referencia", privacidad.Referencia);
            parameters.Add("@Imagen", privacidad.Imagen);
            parameters.Add("@UsuarioId", privacidad.UsuarioId);
            parameters.Add("@FechaEnvio", privacidad.FechaEnvio);
            parameters.Add("@FechaRegistro", privacidad.FechaRegistro);
            parameters.Add("@FechaBaja", privacidad.FechaBaja);
            parameters.Add("@Activo", privacidad.Activo);
            return parameters;
        }

        public static DynamicParameters ParametersDocumentos(Documento documento)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@DocumentoId", documento.DocumentoId);
            parameters.Add("@SolicitanteId", documento.SolicitanteId);
            parameters.Add("@TipoDocumento", documento.TipoDocumento);
            parameters.Add("@NombreDocumento", documento.NombreDocumento);
            parameters.Add("@Imagen", documento.Imagen);
            parameters.Add("@UsuarioId", documento.UsuarioId);
            parameters.Add("@FechaEnvio", documento.FechaEnvio);
            parameters.Add("@FechaRegistro", documento.FechaRegistro);
            parameters.Add("@FechaBaja", documento.FechaBaja);
            parameters.Add("@Activo", documento.Activo);
            return parameters;
        }

        public static DynamicParameters ParametersSolicitantesEstatus(SolicitudEstatusRequest estatus)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@FechaInicial", estatus.FechaInicial.ToUniversalTime());
            parameters.Add("@FechaFinal", estatus.FechaFinal.ToUniversalTime());

            string estatusNombre = estatus.Estatus switch { 
                SolicitudEstatus.Nuevas => "Nueva",
                SolicitudEstatus.Proceso => "En proceso",
                SolicitudEstatus.Finalizadas => "Finalizada",
                _ => ""
            };

            parameters.Add("@Estatus", estatusNombre);
            return parameters;
        }

        public static DynamicParameters ParametersListaNegra(ListaNegra lista)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@ListaNegraId", lista.ListaNegraId);
            parameters.Add("@SolicitanteId", lista.SolicitanteId);
            parameters.Add("@Motivo", lista.Motivo);
            parameters.Add("@TipoMovimientoId", lista.TipoMovimientoId);
            parameters.Add("@UsuarioId", lista.UsuarioId);
            parameters.Add("@FechaRegistro", lista.FechaRegistro);
            parameters.Add("@FechaBaja", lista.FechaBaja);
            parameters.Add("@Activo", lista.Activo);
            return parameters;
        }

        public static DynamicParameters ParametersBitacora(BitacoraRequest bitacora)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@BitacoraId", bitacora.BitacoraId);
            parameters.Add("@OrigenId", bitacora.OrigenId);
            parameters.Add("@TipoLogId", bitacora.TipoLogId);
            parameters.Add("@UsuarioIdActualizo", bitacora.UsuarioId);
            parameters.Add("@FechaRegistro", bitacora.FechaRegistro);
            parameters.Add("@Mensaje", bitacora.Mensaje);
            parameters.Add("@Referencia", bitacora.Referencia);
            return parameters;
        }

        public static DynamicParameters ParametersUpdateValidacion(ValidationResult validation)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@ValidacionId", validation.ValidacionId);
            parameters.Add("@Semaforo", validation.Semaforo);
            parameters.Add("@Resultado", validation.Resultado);
            parameters.Add("@FechaValidacion", validation.Fecha);
            return parameters;
        }

        public static DynamicParameters ParametersAfis(AfisRequest afis)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@PaqueteId", afis.PaqueteId);
            parameters.Add("@AfisId", afis.AfisId);
            return parameters;
        }

        public static DynamicParameters ParametersReportes(FechaRequest fechas)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@FechaInicial", fechas.FechaInicial.ToUniversalTime());
            parameters.Add("@FechaFinal", fechas.FechaFinal.ToUniversalTime());
            return parameters;
        }

        public static DynamicParameters ParametersBusquedas(BusquedaRequest busqueda)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@Nombre", busqueda.Nombre);
            parameters.Add("@ApellidoPaterno", busqueda.ApellidoPaterno);
            parameters.Add("@ApellidoMaterno", busqueda.ApellidoMaterno);
            parameters.Add("@Folio", busqueda.Folio);
            parameters.Add("@FechaInicial", busqueda.FechaInicial);
            parameters.Add("@FechaFinal", busqueda.FechaFinal);
            return parameters;
        }

        #endregion

        #region Consultas Generales

        public static string QryGetAutentication()
        {
            string qry = @"SELECT 
	                U.Nombre_Usuario NombreUsuario, U.Password, P.Nombre_Perfil NombrePerfil 
                    FROM USUARIOS U
	                INNER JOIN PERFILES P ON U.Perfil_Id = P.Perfil_Id
                    WHERE Nombre_Usuario ILIKE @NombreUsuario AND U.Activo = true
                ";
            return qry;
        }

        public static string QryCreateTokenUsuario()
        {
            string qry = @"UPDATE USUARIOS 
	                SET Token = @Token, Token_Vigencia = @TokenVigencia 
                    WHERE Nombre_Usuario = @NombreUsuario 
                ";
            return qry;
        }

        public static string QryGetLogin()
        {
            string qry = @"SELECT 
                U.Nombre_Usuario NombreUsuario, U.Nombre || ' ' || U.Apellido_Paterno || ' ' || U.Apellido_Materno NombreCompleto, 
	            U.Perfil_Id PerfilId, P.Nombre_Perfil NombrePerfil, U.Agencia_Id AgenciaId, A.Nombre_Agencia NombreAgencia, U.Password 
                FROM USUARIOS U
	            INNER JOIN PERFILES P ON U.Perfil_Id = P.Perfil_Id
	            INNER JOIN AGENCIAS A ON U.Agencia_Id = A.Agencia_Id 
                WHERE Nombre_Usuario ILIKE @NombreUsuario AND U.Activo = true
            ";
            return qry;
        }

        public static string QryGetLogOut()
        {
            string qry = @"UPDATE Usuarios 
                    SET Token = null, Token_Vigencia = null 
                    WHERE Nombre_Usuario = @NombreUsuario AND Activo = true 
            ";
            return qry;
        }

        public static string QryGetCredencial()
        {
            string qry = @"SELECT 
                    U.Usuario_Id UsuarioId, 
                    U.Nombre || ' ' || U.Apellido_Paterno || ' ' || U.Apellido_Materno NombreCompleto, 
	                U.Perfil_Id PerfilId, P.Nombre_Perfil NombrePerfil, U.Agencia_Id AgenciaId, A.Nombre_Agencia NombreAgencia 
                    FROM USUARIOS U 
	                INNER JOIN PERFILES P ON U.Perfil_Id = P.Perfil_Id 
	                INNER JOIN AGENCIAS A ON U.Agencia_Id = A.Agencia_Id 
                    WHERE Nombre_Usuario ILIKE @NombreUsuario AND U.Activo = true 
            ";
            return qry;
        }

        public static string QryCreateTokenRecuperacion()
        {
            string qry = @"UPDATE USUARIOS 
                    SET Token_Recuperacion = @Token, Token_Recuperacion_Creacion = @FechaCreacion, Token_Recuperacion_Vigencia = @FechaVigencia 
                    WHERE Usuario_Id = @UsuarioId; 
            ";
            return qry;
        }

        public static string QryGetTokenRecuperacion()
        {
            string qry = @"SELECT 
                        Correo_Electronico CorreoElectronico
                        , Token_Recuperacion_Vigencia TokenVigencia 
                    FROM Usuarios 
                    WHERE Token_Recuperacion = @Token AND Activo = true 
            ";
            return qry;
        }

        public static string QryGetDatosCorreo()
        {
            string qry = @"
                    SELECT Param_Valor FROM Parametros WHERE Param_Nombre = 'CorreoLibreria'; 
                    SELECT Param_Valor FROM Parametros WHERE Param_Nombre = 'CorreoKey'; 
                    SELECT Param_Valor FROM Parametros WHERE Param_Nombre = 'EnvioCorreoAccount'; 
                    SELECT Param_Valor FROM Parametros WHERE Param_Nombre = 'EnvioCorreoName'; 
                    SELECT Param_Valor FROM Parametros WHERE Param_Nombre = 'EnvioCorreoPassword';
                    SELECT Param_Valor FROM Parametros WHERE Param_Nombre = 'EnvioCorreoHost';
                    SELECT Param_Valor FROM Parametros WHERE Param_Nombre = 'EnvioCorreoPuerto';
                    SELECT Param_Valor FROM Parametros WHERE Param_Nombre = 'EnvioCorreoSsl';
                    SELECT Param_Valor FROM Parametros WHERE Param_Nombre = 'EnvioCorreoDefaultCredentials';
                    SELECT Param_Valor FROM Parametros WHERE Param_Nombre = 'UrlPortal';
            ";
            return qry;
        }

        public static string QryGetCorreoUsuario()
        {
            string qry = @"SELECT 
                        Usuario_Id UsuarioId 
                        , Nombre || ' ' || Apellido_Paterno || ' ' || Apellido_Materno UsuarioNombre 
                    FROM Usuarios 
                    WHERE Activo = true AND Correo_Electronico = @Correo; 
            ";
            return qry;
        }

        public static string QryUpdatePassword()
        {
            string qry = @"UPDATE USUARIOS 
                    SET Password = @Password, Password_Actualizacion = @FechaActualizacion, Token_Recuperacion = null, Token_Recuperacion_Creacion = null  
                    WHERE Usuario_Id = @UsuarioId; 
            ";
            return qry;
        }

        public static string QryGetApkVersion()
        {
            string qry = @"
                    SELECT Param_Valor FROM Parametros WHERE Param_Nombre = 'ApkFecha'; 
                    SELECT Param_Valor FROM Parametros WHERE Param_Nombre = 'ApkVersion'; 
                    SELECT Param_Valor FROM Parametros WHERE Param_Nombre = 'ApkActualizar'; 
            ";
            return qry;
        }

        public static string QryGetUsuarios(bool queryById)
        {
            string qry = @"SELECT 
                    U.Usuario_Id UsuarioId, U.Nombre_Usuario NombreUsuario, U.Password, U.Nombre, U.Apellido_Paterno ApellidoPaterno, U.Apellido_Materno ApellidoMaterno, 
	                U.Perfil_Id PerfilId, P.Nombre_Perfil NombrePerfil, U.Agencia_Id AgenciaId, A.Nombre_Agencia NombreAgencia, 
                    U.Fecha_Nacimiento FechaNacimiento, U.Correo_Electronico CorreoElectronico, 
                    U.Activo, U.Fecha_Registro FechaRegistro, U.Fecha_Baja FechaBaja 
                    , U.Fecha_Activacion FechaActivacion, U.Motivo_Estatus Motivo, U.Usuario_Id_Estatus UsuarioIdEstatus
                    FROM USUARIOS U 
	                INNER JOIN PERFILES P ON U.Perfil_Id = P.Perfil_Id 
	                INNER JOIN AGENCIAS A ON U.Agencia_Id = A.Agencia_Id 
            ";
            if (queryById)
            {
                qry += " WHERE Usuario_Id = @UsuarioId ";
            }
            qry += $" ORDER BY U.Nombre_Usuario ";
            return qry;
        }

        public static string QryCountUsuarios()
        {
            string qry = "SELECT COUNT(*) FROM USUARIOS; ";
            return qry;
        }

        public static string QryAddUsuarios()
        {
            string qry = @"INSERT INTO USUARIOS 
                        (Nombre_Usuario, Password, Nombre, Apellido_Paterno, Apellido_Materno, Fecha_Nacimiento, Correo_Electronico, Perfil_Id, Agencia_Id, 
                        Activar_Cuenta, Activo, Fecha_Registro) 
                        VALUES (@NombreUsuario, @Password, @Nombre, @ApellidoPaterno, @ApellidoMaterno, @FechaNacimiento, @CorreoElectronico, @PerfilId, @AgenciaId, 
                        @ActivarCuenta, @Activo, @FechaRegistro); 
                        SELECT currval('usuarios_usuario_id_seq'); 
            ";
            return qry;
        }

        public static string QryUpdateUsuarios()
        {
            string qry = @"UPDATE USUARIOS 
                    SET Nombre_Usuario = @NombreUsuario, Nombre = @Nombre, Apellido_Paterno = @ApellidoPaterno, Apellido_Materno = @ApellidoMaterno,
                    Perfil_Id = @PerfilId, Agencia_Id = @AgenciaId, Fecha_Nacimiento = @FechaNacimiento, Correo_Electronico = @CorreoElectronico 
                    WHERE Usuario_Id = @UsuarioId;
            ";
            return qry;
        }

        public static string QryDeleteUsuario()
        {
            string qry = @"UPDATE USUARIOS 
                    SET Activo = false, Fecha_Baja = @FechaBaja 
                    WHERE Usuario_Id = @UsuarioId AND Activo = true
            ";
            return qry;
        }

        public static string QryGetUsuariosByUserNameOrCorreoOrNombre()
        {
            string qry = @"SELECT 
                    Usuario_Id UsuarioId
	                , UPPER(Nombre_Usuario) NombreUsuario 
	                , UPPER(Correo_Electronico) CorreoElectronico 
	                , UPPER(Nombre) || ' ' || UPPER(Apellido_Paterno) || ' ' || UPPER(Apellido_Materno) NombreCompleto 
                FROM Usuarios 
                WHERE (UPPER(Nombre_Usuario) = @NombreUsuario OR UPPER(Correo_Electronico) = @CorreoElectronico OR UPPER(Nombre)  || ' ' || UPPER(Apellido_Paterno) || ' ' || UPPER(Apellido_Materno) = @NombreCompleto) 
                ;
            ";
            return qry;
        }

        public static string QryUpdateUsuariosEstatus(bool activar)
        {
            string qry = "UPDATE Usuarios SET Activo = @Activar, ";
            if (activar)
                qry += "Fecha_Activacion ";
            else
                qry += "Fecha_Baja ";
            qry += "= @FechaRegistro, Motivo_Estatus = @Motivo, Usuario_Id_Estatus = @UsuarioIdActualizo WHERE Usuario_Id = @UsuarioId; ";

            return qry;
        }

        public static string QryGetAgencias(bool consultarTodo)
        {
            string qry = @"SELECT 
                        A.Agencia_Id AgenciaId, A.Clave_Agencia ClaveAgencia, A.Nombre_Agencia NombreAgencia, A.Telefono, 
                        A.Estado_Id EstadoId, E.Nombre_Estado NombreEstado, A.Direccion,
                        A.Tipo_Agencia_Id TipoAgenciaId, ATI.Tipo_Agencia_Nombre TipoAgenciaNombre, 
                        A.Activo, A.Fecha_Registro FechaRegistro, A.Fecha_Baja FechaBaja 
                        , A.Fecha_Activacion FechaActivacion, A.Motivo_Estatus Motivo, A.Usuario_Id_Estatus UsuarioIdEstatus
                    FROM AGENCIAS A 
                    INNER JOIN AgenciasTipo ATI ON A.Tipo_Agencia_Id = ATI.Tipo_Agencia_Id
                    INNER JOIN ESTADOS E ON A.Estado_Id = E.Estado_Id 
                    WHERE A.Agencia_Id > 0 
                ";
            if (!consultarTodo)
                qry += "AND Agencia_Id = @AgenciaId ";
            qry += $"ORDER BY A.Nombre_Agencia ";
            return qry;
        }

        public static string QryGetCountAgencias()
        {
            string qry = "SELECT COUNT(*) FROM AGENCIAS WHERE Agencia_Id > 0; ";
            return qry;
        }
        
        public static string QryAddAgencias()
        {
            string qry = @"INSERT INTO AGENCIAS 
                    (Clave_Agencia, Nombre_Agencia, Direccion, Tipo_Agencia_Id, Activo, Fecha_Registro, Telefono, Estado_Id) 
                    VALUES (@ClaveAgencia, @NombreAgencia, @Direccion, @TipoAgenciaId, true, @FechaRegistro, @Telefono, @EstadoId); 
                    SELECT currval('agencias_agencia_id_seq'); 
            ";
            return qry;
        }

        public static string QryDeleteAgencia()
        {
            string qry = @"UPDATE AGENCIAS 
                    SET Activo = false, Fecha_Baja = @FechaBaja 
                    WHERE Agencia_Id = @AgenciaId And Activo = true;
            ";
            return qry;
        }

        public static string QryUpdateAgencias()
        {
            string qry = @"UPDATE AGENCIAS 
                    SET Clave_Agencia = @ClaveAgencia, Nombre_Agencia = @NombreAgencia, Direccion = @Direccion, 
                    Tipo_Agencia_Id = @TipoAgenciaId, Telefono = @Telefono, Estado_Id = @EstadoId 
                    WHERE Agencia_Id = @AgenciaId;
            ";
            return qry;
        }

        public static string QryGetAgenciasByClaveOrNombre()
        {
            string qry = @"SELECT 
	                    Agencia_Id AgenciaId 
	                    , UPPER(Clave_Agencia) ClaveAgencia 
	                    , UPPER(Nombre_Agencia) NombreAgencia 
                    FROM Agencias 
                    WHERE (UPPER(Clave_Agencia) = @ClaveAgencia OR UPPER(Nombre_Agencia) = @NombreAgencia) 
                ;
            ";
            return qry;
        }

        public static string QryUpdateAgenciasEstatus(bool activar)
        {
            var qry = "UPDATE Agencias SET Activo = @Activar, ";
            if (activar)
                qry += "Fecha_Activacion ";
            else
                qry += "Fecha_Baja ";
            qry += "= @FechaRegistro, Motivo_Estatus = @Motivo, Usuario_Id_Estatus = @UsuarioIdActualizo WHERE Agencia_Id = @AgenciaId; ";
            return qry;
        }

        public static string QryAddSolicitantes()
        {
            string qry = @"INSERT INTO Solicitantes 
                    (
                        Solicitante_Id_Origen, Usuario_Id, Agencia_Id, Nombre, Apellido_Paterno, Apellido_Materno, Fecha_Nacimiento, Curp, 
                        Correo_Electronico, Tipo_Cliente, Estatus, Folio, 
                        Tipo_Documento, Numero_Documento, Guid, Telefono, Sexo, Resultado_General, Lugar_Nacimiento, Coordenadas_Gps, 
                        Score_Comparacion_Facial, Resultado_Comparacion_Facial, Nacionalidad, Direccion_Completa, Codigo_Postal, 
                        Calle_Numero, Colonia, Municipio, Estado, Prueba_Vida, Edad, Nombre_Completo_Solicitante, 
                        Fecha_Envio, Fecha_Registro, Activo, DocumentoPdf 
                    ) 
                    VALUES ( 
                        @SolicitanteIdOrigen, @UsuarioId, @AgenciaId, @Nombre, @ApellidoPaterno, @ApellidoMaterno, @FechaNacimiento, @Curp, 
                        @CorreoElectronico, @TipoCliente, @Estatus, @Folio, 
                        @TipoDocumento, @NumeroDocumento, @Guid, @Telefono, @Sexo, @ResultadoGeneral, @LugarNacimiento, @CoordenadasGps, 
                        @ScoreComparacionFacial, @ResultadoComparacionFacial, @Nacionalidad, @DireccionCompleta, @CodigoPostal, 
                        @CalleNumero, @Colonia, @Municipio, @Estado, @PruebaVida, @Edad, @NombreCompletoSolicitante, 
                        @FechaEnvio, @FechaRegistro, @Activo, @DocumentoPdf  
                    ); 
                    SELECT currval('Solicitantes_Solicitante_Id_seq'); 
            ";
            return qry;
        }

        public static string QryAddIdentificaciones()
        {
            string qry = @"INSERT INTO Identificaciones 
                    (
                        Solicitante_Id, Serie, Numero_Emision, Cic, Ocr, Clave_Elector, Identificador_Ciudadano, 
                        Vigencia, Anio_Registro, Emision, MRZ, 
                        Fecha_Envio, Fecha_Registro, Activo, Tipo_Ine 
                    ) 
                    VALUES (
                        @SolicitanteId, @Serie, @NumeroEmision, @Cic, @Ocr, @ClaveElector, @IdentificadorCiudadano, 
                        @Vigencia, @AnioRegistro, @Emision, @Mrz, 
                        @FechaEnvio, @FechaRegistro, @Activo, @TipoIne 
                    ); 
                    SELECT currval('Identificaciones_Identificacion_Id_seq'); 
            ";
            return qry;
        }

        public static string QryAddCapturaIdentificaciones()
        {
            string qry = @"INSERT INTO CapturasIdentificacion 
                    (
                        Solicitante_Id, Imagen, Captura_Nombre_Id 
                        , Usuario_Id, Fecha_Envio, Fecha_Registro, Activo 
                    ) 
                    VALUES (
                        @SolicitanteId, @Imagen, @CapturaNombreId
                        , @UsuarioId, @FechaEnvio, @FechaRegistro, @Activo
                    ); 
                    SELECT currval('CapturasIdentificacion_Captura_Identificacion_Id_seq'); 
            ";
            return qry;
        }

        public static string QryAddFotos()
        {
            string qry = @"INSERT INTO Fotos 
                    (
                        Solicitante_Id, Imagen, Foto_Origen_Id, Guid, 
                        Usuario_Id, Fecha_Envio, Fecha_Registro, Activo 
                    ) 
                    VALUES (
                        @SolicitanteId, @Imagen, @FotoOrigenId, @Guid, 
                        @UsuarioId, @FechaEnvio, @FechaRegistro, @Activo
                    ); 
                    SELECT currval('Fotos_Foto_Id_seq'); 
            ";
            return qry;
        }

        public static string QryAddResolucion()
        {
            string qry = @"INSERT INTO Resolucion 
                    (
                        Solicitante_Id, Tipo_Resolucion_Id, Usuario_Id 
                    ) 
                    VALUES ( 
                        @SolicitanteId, @TipoResolucionId, @UsuarioId 
                    ); 
                    SELECT currval('Resolucion_Resolucion_Id_seq'); 
            ";
            return qry;
        }

        public static string QryAddValidacion()
        {
            string qry = @"INSERT INTO Validaciones 
                    (
                        Solicitante_Id, 
                        Semaforo_Identificacion, Resultado_Identificacion, Fecha_Identificacion, 
                        Semaforo_Facial, Resultado_Facial, Fecha_Facial 
                    ) 
                    VALUES ( 
                        @SolicitanteId, 
                        @SemaforoIdentificacion, @ResultadoIdentificacion, @FechaIdentificacion, 
                        @SemaforoFacial, @ResultadoFacial, @FechaFacial
                    ); 
                    SELECT currval('Validaciones_Validacion_Id_seq'); 
            ";
            return qry;
        }

        public static string QryAddHuellas()
        {
            string qry = @"INSERT INTO Huellas 
                    (
                        Solicitante_Id, Dedo_Indice_Id, Dedo_Estatus_Id, Imagen 
                        , Usuario_Id, Fecha_Envio, Fecha_Registro, Activo 
                    ) 
                    VALUES (
                        @SolicitanteId, @DedoIndiceId, @DedoEstatusId, @Imagen 
                        , @UsuarioId, @FechaEnvio, @FechaRegistro, @Activo 
                    ); 
                    SELECT currval('Huellas_Huella_Id_seq'); 
            ";
            return qry;
        }

        public static string QryAddDocumentos()
        {
            string qry = @"INSERT INTO Documentos 
                    (
                        Solicitante_Id, Tipo_Documento, Nombre_Documento, Imagen 
                        , Usuario_Id, Fecha_Envio, Fecha_Registro, Activo 
                    ) 
                    VALUES (
                        @SolicitanteId, @TipoDocumento, @NombreDocumento, @Imagen 
                        , @UsuarioId, @FechaEnvio, @FechaRegistro, @Activo
                    ); 
                    SELECT currval('Documentos_Documento_Id_seq'); 
            ";
            return qry;
        }

        public static string QryAddAvisosPrivacidad()
        {
            string qry = @"INSERT INTO AvisosPrivacidad 
                    (
                        Solicitante_Id, Referencia, Imagen 
                        , Usuario_Id, Fecha_Envio, Fecha_Registro, Activo 
                    ) 
                    VALUES (
                        @SolicitanteId, @Referencia, @Imagen 
                        , @UsuarioId, @FechaEnvio, @FechaRegistro, @Activo 
                    ); 
                    SELECT currval('AvisosPrivacidad_Aviso_Privacidad_Id_seq'); 
            ";
            return qry;
        }

        public static string QryAddListaNegra()
        {
            string qry = @"INSERT INTO ListaNegra 
                    (
                        Solicitante_Id, Motivo, Tipo_Movimiento_Id 
                        , Usuario_Id, Fecha_Registro, Activo 
                    ) 
                    VALUES (
                        @SolicitanteId, @Motivo, @TipoMovimientoId 
                        , @UsuarioId, @FechaRegistro, @Activo 
                    ); 
                    SELECT currval('ListaNegra_Lista_Negra_Id_seq'); 
            ";
            return qry;
        }

        public static string QryUpdateSemaforoListaNegra()
        {
            string qry = @"UPDATE Validaciones 
                    SET 
                        Semaforo_Lista_Negra = @SemaforoListaNegra, Resultado_Lista_Negra = @ResultadoListaNegra, Fecha_Lista_Negra = @FechaRegistro 
                    WHERE Solicitante_Id = @SolicitanteId; 
            ";
            return qry;
        }

        public static string QryUpdateSemaforoIbms()
        {
            string qry = @"UPDATE Validaciones 
                    SET 
                        Semaforo_IBMS = @SemaforoIBMS, Resultado_IBMS = @ResultadoIBMS, Fecha_IBMS = @FechaRegistro
                    WHERE Solicitante_Id = @SolicitanteId; 
            ";
            return qry;
        }

        public static string QryUpdateResolucion()
        {
            string qry = @"UPDATE Resolucion 
                    SET 
                        Comentario = @Comentario, Tipo_Resolucion_Id = @TipoResolucionId, Usuario_Id = @UsuarioId, Fecha_Registro = @FechaRegistro
                    WHERE Solicitante_Id = @SolicitanteId; 
            ";
            return qry;
        }

        public static string QryUpdateSolicitanteEstatus()
        {
            string qry = @$"UPDATE Solicitantes 
                    SET Estatus = @Estatus 
                    WHERE Solicitante_Id = @SolicitanteId; 
            ";
            return qry;
        }

        public static string QryGetNotificacionesSolicitudesNuevas()
        {
            string qry = @"SELECT 
	                S.Solicitante_Id SolicitanteId 
                	, S.Fecha_Envio FechaEnvio 
	                , S.Estatus 
	                , S.Nombre || ' ' || S.Apellido_Paterno || ' ' || S.Apellido_Materno NombreCompleto 
	                , '' TiempoRegistro 
                FROM Solicitantes S 
                WHERE S.Estatus = 'Nueva' 
                ORDER BY S.Solicitante_Id DESC 
            ";
            return qry;
        }

        public static string QryGetCountNotificacionesSolicitudesNuevas()
        {
            string qry = "SELECT COUNT(*) FROM Solicitantes S WHERE S.Estatus = 'Nueva'; ";
            return qry;
        }

        public static string QryGetSolicitanteFicha()
        {
            var qry = @"SELECT 
	                S.Solicitante_Id SolicitanteId 
	                , S.Solicitante_Id_Origen SolicitanteIdOrigen 
	                , S.Folio 
	                , S.Fecha_Registro FechaCaptura 
	                , S.Fecha_Envio FechaEnvio 
	                , current_timestamp FechaUltimaConsulta	
	                , S.Nombre SolicitanteNombre 
	                , S.Apellido_Paterno SolicitanteApellidoPaterno 
	                , S.Apellido_Materno SolicitanteApellidoMaterno 
	                , S.Edad 
	                , S.Curp 
	                , S.Fecha_Nacimiento FechaNacimiento 
	                , S.Telefono 
	                , S.Correo_Electronico CorreoElectronico 
	                , S.Tipo_Cliente TipoCliente 
	                , S.Coordenadas_Gps CoordenadasGps 
	                , S.Direccion_Completa DireccionCompleta 
	                , S.Estatus 
	                , S.Usuario_Id EnroladorId 
	                , U.Nombre_Usuario EnroladorUsuario 
	                , U.Nombre EnroladorNombre 
	                , U.Apellido_Paterno EnroladorApellidoPaterno 
	                , U.Apellido_Materno EnroladorApellidoMaterno 
	                , S.Agencia_Id AgenciaId 
	                , A.Clave_Agencia ClaveAgencia 
	                , A.Nombre_Agencia NombreAgencia 
	                , S.Tipo_Documento TipoDocumento 
	                , S.Numero_Documento NumeroDocumento 
	                , S.Guid 
	                , S.Sexo 
	                , S.Lugar_Nacimiento LugarNacimiento 
	                , S.Score_Comparacion_Facial ScoreComparacionFacial 
	                , S.Resultado_Comparacion_Facial ResultadoComparacionFacial 
	                , S.Nacionalidad 
	                , S.Codigo_Postal CodigoPostal 
	                , S.Calle_Numero CalleNumero 
	                , S.Colonia 
	                , S.Municipio 
	                , S.Estado 
	                , S.Prueba_Vida PruebaVida 
	                , S.Nombre_Completo_Solicitante NombreCompletoSolicitante 
                    , S.Afis_Id AfisId 
                FROM Solicitantes S 
	                INNER JOIN Usuarios U ON S.Usuario_Id = U.Usuario_Id 
	                INNER JOIN Agencias A ON S.Agencia_Id = A.Agencia_Id 
                WHERE S.Activo = true 
                    AND S.Solicitante_Id = @SolicitanteId
                    ;
            ";
            return qry;
        }

        public static string QryGetIdentificacionesFicha()
        {
            string qry = @"SELECT 
	                Identificacion_Id IdentificacionId 
	                , Solicitante_Id SolicitanteId 
	                , Serie 
	                , Numero_Emision NumeroEmision 
	                , Cic 
	                , Ocr 
	                , Clave_Elector ClaveElector 
	                , Identificador_Ciudadano IdentificadorCiudadano 
	                , Vigencia 
	                , Anio_Registro AnioRegistro 
	                , Emision 
	                , MRZ 
	                , Fecha_Envio FechaEnvio 
	                , Fecha_Registro FechaCaptura 
                FROM Identificaciones I 
                WHERE I.Solicitante_Id = @SolicitanteId 
                    AND I.Activo = true 
                ORDER BY I.Identificacion_Id 
                ; 
            ";
            return qry;
        }

        public static string QryGetIneFicha()
        {
            string qry = @"SELECT 
	                I.Solicitante_Id SolicitanteId 
	                , S.Tipo_Documento TipoDocumento 
	                , I.Serie 
	                , I.Tipo_Ine Modelo 
	                , I.Cic 
                , I.Identificador_Ciudadano IdentificadorCiudadano 
	                , I.Ocr 
	                , I.Clave_Elector ClaveElector  
                , I.Numero_Emision NumeroEmision  
	                , I.Mrz 
                FROM Identificaciones I 
	                INNER JOIN Solicitantes S ON I.Solicitante_Id = S.Solicitante_Id 
                WHERE S.Tipo_Documento = 'Mexico (MEX) Voter Identification Card' 
                	AND I.Solicitante_Id = @SolicitanteId  
                ; 
            ";
            return qry;
        }

        public static string QryGetDocumentosCargadosFicha()
        {
            string qry = @"SELECT 
	                D.Documento_Id DocumentoId  
	                , D.Solicitante_Id SolicitanteId 
                    , D.Nombre_Documento TipoDocumentoId
	                , D.Tipo_Documento FormatoDocumento 
                FROM Documentos D 
                WHERE D.Activo = true 
	                AND D.Solicitante_Id = @SolicitanteId 
                ORDER BY D.Documento_Id 
                ; 
            ";
            return qry;
        }

        public static string QryGetAvisoPrivacidadFicha()
        {
            string qry = @"SELECT 
	                Aviso_Privacidad_Id AvisoPrivacidadId 
                FROM AvisosPrivacidad 
                WHERE Activo = true 
	                AND Solicitante_Id = @SolicitanteId 
                ; 
            ";
            return qry;
        }

        public static string QryGetListaNegraFicha()
        {
            string qry = @"SELECT 
	                LN.Lista_Negra_Id ListaNegraId 
	                , LN.Solicitante_Id SolicitanteId 
                    , LN.Motivo 
                    , LN.Tipo_Movimiento_Id TipoMovimientoId 				
                    , LN.Usuario_Id AnalistaId 
	                , U.Nombre_Usuario AnalistaUsuario 
	                , U.Nombre AnalistaNombre 
	                , U.Apellido_Paterno AnalistaApellidoPaterno 
	                , U.Apellido_Materno AnalistaApellidoMaterno 
	                , LN.Fecha_Registro FechaCaptura 
                FROM ListaNegra LN 
	                INNER JOIN Usuarios U on LN.Usuario_id = U.Usuario_id 
                WHERE LN.Solicitante_Id = @SolicitanteId 
	                AND LN.Activo = true 
                ORDER BY LN.Lista_Negra_Id Desc 
                ; 
            ";
            return qry;
        }

        public static string QryGetResolucionFicha()
        {
            string qry = @"SELECT 
	                R.Resolucion_Id ResolucionId 
	                , R.Solicitante_Id SolicitanteId  
                    , R.Comentario 
                    , R.Tipo_Resolucion_Id TipoResolucionId 
                    , TR.Tipo_Resolucion_Nombre TipoResolucionNombre 
                    , R.Usuario_Id AnalistaId  
	                , U.Nombre_Usuario AnalistaUsuario 
	                , U.Nombre AnalistaNombre 
	                , U.Apellido_Paterno AnalistaApellidoPaterno 
	                , U.Apellido_Materno AnalistaApellidoMaterno 
	                , R.Fecha_Registro FechaCaptura 	
                FROM Resolucion R 
	                INNER JOIN usuarios U on R.usuario_id = U.usuario_id 
	                INNER JOIN TiposResolucion TR on R.Tipo_Resolucion_Id = TR.Tipo_Resolucion_Id 
                WHERE R.Solicitante_Id = @SolicitanteId
                ; 
            ";
            return qry;
        }

        public static string QryGetValidaciones()
        {
            string qry = @"SELECT 
	                V.ValidacionId 
	                , V.SolicitanteId 
	                , V.UsuarioConsultaId
	                , V.UsuarioConsultaNombre
	                , V.UsuarioConsultaApellidoPaterno
	                , V.UsuarioConsultaApellidoMaterno
	                , V.FechaConsulta
	                , V.SemaforoListaNegra 
	                , V.ResultadoListaNegra 
	                , V.FechaListaNegra 
	                , V.SemaforoIBMS 
	                , V.ResultadoIBMS 
	                , V.FechaIBMS 
	                , V.SemaforoIdentificacion 
	                , V.ResultadoIdentificacion 
	                , V.FechaIdentificacion 
	                , V.SemaforoIne 
	                , V.ResultadoIne 
	                , V.FechaIne 
	                , V.SemaforoFacial 
	                , V.ResultadoFacial 
	                , V.FechaFacial 
	                , V.SemaforoCorreo 
	                , V.ResultadoCorreo 
	                , V.FechaCorreo 
	                , V.SemaforoTelefono 
	                , V.ResultadoTelefono 
	                , V.FechaTelefono 
	                , V.SemaforoCurp 
	                , V.ResultadoCurp 
	                , V.FechaCurp 
	                , V.SemaforoComprobanteDomicilio 
	                , V.ResultadoComprobanteDomicilio 
	                , V.FechaComprobanteDomicilio 
	                , V.SemaforoComprobanteIngresos 
	                , V.ResultadoComprobanteIngresos 
	                , V.FechaComprobanteIngresos 
	                , V.SemaforoComprobanteBancario 
	                , V.ResultadoComprobanteBancario 
	                , V.FechaComprobanteBancario 
	                , V.SemaforoListaAml 
	                , V.ResultadoListaAml 
	                , V.FechaListaAml 
	                , V.SemaforoAfis 
	                , V.ResultadoAfis 
	                , V.FechaAfis 
	                , V.ResultadoGeoreferencia 
	                , V.FechaGeoreferencia 
                FROM v_Validaciones V 
                WHERE V.SolicitanteId = @SolicitanteId	
                ; 
            ";
            return qry;
        }

        public static string QryGetSolicitanteSolidarioFicha(bool acreditado)
        {
            string qry = @"SELECT 
	                Solicitante_Id SolicitanteId 
	                , Solicitante_Id_Origen SolicitanteIdOrigen 
	                , Folio 
	                , Tipo_Cliente TipoCliente 
	                , Nombre SolicitanteNombre 
	                , Apellido_Paterno SolicitanteApellidoPaterno 
	                , Apellido_Materno SolicitanteApellidoMaterno 
                FROM Solicitantes 
                WHERE Activo = true AND 
            ";
            if (acreditado)
                qry += "Solicitante_Id_Origen = @SolicitanteId ";
            else
                qry += "Solicitante_Id = @SolicitanteIdOrigen ";
            qry += " ORDER BY Nombre, Apellido_Paterno, Apellido_Materno DESC; ";

            return qry;
        }

        public static string QryGetSolicitudSemaforoIdentificacion()
        {
            string qry = @"SELECT 
	                V.ValidacionId 
	                , V.SolicitanteId 
	                , V.SemaforoIdentificacion Semaforo 
	                , V.ResultadoIdentificacion Mensaje 
	                , V.FechaIdentificacion FechaValidacion 
                FROM v_Validaciones V 
                WHERE V.SolicitanteId = @SolicitanteId 
                ; 
            ";
            return qry;
        }

        public static string QryGetSolicitudIdentificacion()
        {
            string qry = @"SELECT 
	                C.Captura_Identificacion_Id CapturaIdentificacionId 
	                , C.Solicitante_Id SolicitanteId 
	                , C.Imagen 
	                , C.Captura_Nombre_Id CapturaNombreId 
	                , CN.Captura_Nombre CapturaNombre 
	                , C.Fecha_Envio FechaEnvio 
	                , C.Fecha_Registro FechaCaptura 
                FROM CapturasIdentificacion C 
	                INNER JOIN CapturasNombre CN on C.captura_nombre_id = CN.captura_nombre_id 
                WHERE C.Solicitante_Id = @SolicitanteId
	                AND C.Activo = true 
                ORDER BY C.Captura_Identificacion_Id 
                ; 
            ";
            return qry;
        }

        public static string QryGetSolicitudIdentificacionPdf()
        {
            string qry = @"SELECT 
                    DocumentoPdf 
                FROM Solicitantes S 
                WHERE Activo = true AND Solicitante_Id = @SolicitanteId
                ;
            ";
            return qry;
        }

        public static string QryGetSolicitudSemaforoComparacionFacial()
        {
            string qry = @"SELECT 
	                V.ValidacionId 
	                , V.SolicitanteId 
		            , V.SemaforoFacial Semaforo 
		            , V.ResultadoFacial Mensaje 
		            , V.FechaFacial FechaValidacion 
                FROM v_Validaciones V 
                WHERE V.SolicitanteId = @SolicitanteId 
                ; 
            ";
            return qry;
        }

        public static string QryGetSolicitudSemaforoHuellas()
        {
            string qry = @"SELECT 
                	ValidacionId 
	                , SolicitanteId 
	                , ClienteNombreCompleto  
	                , AfisId 
	                , AfisFecha 
	                , SemaforoAfis 
	                , ResultadoAfis 
	                , FechaAfis 
                FROM v_Validaciones 
                WHERE SolicitanteId = @SolicitanteId 
                ; 
            ";
            return qry;
        }

        public static string QryGetSolicitudFotos()
        {
            string qry = @"SELECT 
	                Foto_Id FotoId 
	                , F.Solicitante_Id SolicitanteId 
	                , F.Imagen 
	                , F.Foto_Origen_Id FotoOrigenId 
	                , FO.Foto_Origen_Nombre FotoOrigenNombre 
	                , F.Fecha_Envio FechaEnvio 
	                , F.Fecha_Registro FechaCaptura 
                FROM Fotos F 
	                INNER JOIN FotosOrigen FO on F.Foto_Origen_Id = FO.Foto_Origen_Id 
                WHERE F.Solicitante_Id = @SolicitanteId
	                AND F.Activo = true 
                ORDER BY F.Foto_Id 
                ; 
            ";
            return qry;
        }

        public static string QryGetSolicitudHuellas()
        {
            string qry = @"SELECT 
	                H.Huella_Id HuellaId 
	                , H.Solicitante_Id SolicitanteId 
	                , H.Dedo_Indice_Id DedoIndiceId 
	                , DI.Dedo_Indice_Nombre DedoIndiceNombre 
	                , H.Dedo_Estatus_Id DedoEstatusId 
	                , DE.Dedo_Estatus_Nombre DedoEstatusNombre 
	                , DE.Dedo_Estatus_Clave DedoEstatusClave 
	                , H.Imagen
                FROM Huellas H 
	                INNER JOIN DedosIndice DI ON H.dedo_indice_id = DI.dedo_indice_id 
	                INNER JOIN dedosestatus DE ON H.dedo_estatus_id = DE.dedo_estatus_id  
                WHERE H.Solicitante_Id = @SolicitanteId 
	                AND H.Activo = true 
                ORDER BY H.Huella_Id 
                ; 
            ";
            return qry;
        }

        public static string QryGetSolicitudAvisoPrivacidad()
        {
            string qry = @"SELECT 
	                Aviso_Privacidad_Id AvisoPrivacidadId 
	                , Solicitante_Id SolicitanteId 
	                , Referencia 
	                , Imagen 
                FROM AvisosPrivacidad 
                WHERE Activo = true 
	                AND Solicitante_Id = @SolicitanteId 
                ; 
            ";
            return qry;
        }

        public static string QryUpdateSolicitanteConsulta()
        {
            string qry = @"UPDATE Validaciones 
                    SET Usuario_Consulta_Id = @UsuarioId, Fecha_Consulta = @FechaRegistro
                    WHERE Solicitante_Id = @SolicitanteId; 
            ";
            return qry;
        }

        public static string QryGetFiltroSolicitantesEstatus(SolicitudEstatus estatus)
        {
            string qry = "WHERE S.Activo = true AND S.Estatus = @Estatus AND ";
            if (estatus == SolicitudEstatus.Finalizadas)
                qry += "R.Fecha_Registro ";
            else
                qry += "S.Fecha_Envio ";
            qry += "BETWEEN @FechaInicial AND @FechaFinal ";
            return qry;
        }

        public static string QryGetSolicitantesEstatus(bool descargar, string filtro)
        {
            string qry = @"SELECT 
	            S.Solicitante_Id SolicitanteId 
	            , S.Solicitante_Id_Origen SolicitanteIdOrigen 
	            , S.Folio
	            , S.Fecha_Registro FechaCaptura 
	            , S.Fecha_Envio FechaEnvio
	            , S.Nombre SolicitanteNombre 
	            , S.Apellido_Paterno SolicitanteApellidoPaterno 
	            , S.Apellido_Materno SolicitanteApellidoMaterno 
	            , S.Fecha_Nacimiento FechaNacimiento
	            , S.Tipo_Cliente TipoCliente
	            , S.Estatus
            ";
            if (descargar)
                qry += "";
            else
                qry += ", (SELECT Imagen FROM Fotos WHERE Solicitante_Id = S.Solicitante_Id AND Foto_Origen_Id = 1 AND Activo = true) Imagen ";
            qry += $"FROM Solicitantes S INNER JOIN Resolucion R on S.solicitante_id = R.solicitante_id {filtro} ORDER BY S.Fecha_Envio DESC ";
            return qry;
        }

        public static string QryGetCountSolicitantesEstatus(string filtro)
        {
            string qry = $"SELECT COUNT(*) FROM Solicitantes S INNER JOIN Resolucion R on S.solicitante_id = R.solicitante_id {filtro}; ";
            return qry;
        }

        public static string QryGetSolicitantesResultadoGeneralIdentificacion()
        {
            string qry = @"SELECT 
                    Resultado_General ResultadoGeneral 
                FROM Solicitantes 
                WHERE Activo = true AND Solicitante_Id = @SolicitanteId 
                ; 
            ";
            return qry;
        }

        public static string QryUpdateSemaforoIdentificacion()
        {
            string qry = @"UPDATE Validaciones 
                    SET Semaforo_Identificacion = @Semaforo, Resultado_Identificacion = @Resultado, Fecha_Identificacion = @FechaValidacion
                    WHERE Solicitante_Id = @SolicitanteId; 
            ";
            return qry;
        }

        public static string QryGetSemaforoComparacionFacial()
        {
            string qry = @"SELECT 
                    CASE WHEN UPPER(Resultado_Comparacion_Facial) = 'TRUE' THEN Score_Comparacion_Facial ELSE '0' END ScoreFacial 
                FROM Solicitantes 
                WHERE Activo = true AND Solicitante_Id = @SolicitanteId 
                ; 
            ";
            return qry;
        }

        public static string QryUpdateSemaforoComparacionFacial()
        {
            string qry = @"UPDATE Validaciones 
                    SET Semaforo_Facial = @Semaforo, Resultado_Facial = @Resultado, Fecha_Facial = @FechaValidacion
                    WHERE Solicitante_Id = @SolicitanteId; 
            ";
            return qry;
        }

        public static string QryGetDatosProyecto()
        {
            string qry = @"
                    SELECT Param_Valor FROM Parametros WHERE Param_Nombre = 'ProyectoNombre'; 
                    SELECT Param_Valor FROM Parametros WHERE Param_Nombre = 'ProyectoPrefijo';
            ";
            return qry;
        }

        public static string QryGetAfisValidar()
        {
            string qry = @"SELECT 
	                    PaqueteId 
	                    , NombreCompleto 
                    FROM v_AfisValidar 
                    LIMIT 1 OFFSET 0; 
            ";
            return qry;
        }

        public static string QryGetAfisHuellas()
        {
            string qry = @"SELECT 
	                H.Dedo_Indice_Id DedoId 
	                , H.Imagen Bytes 
	                , D.Dedo_Indice_Nombre DedoNombre 
	                , H.Dedo_Estatus_Id EstatusId
	                , E.Dedo_Estatus_Nombre EstatusNombre 
                FROM Huellas H 
	                INNER JOIN DedosIndice D ON H.dedo_indice_id = D.dedo_indice_id  
	                INNER JOIN DedosEstatus E ON E.Dedo_Estatus_Id = H.Dedo_Estatus_Id 
                WHERE Solicitante_Id = @PaqueteId; 
            ";
            return qry;
        }

        public static string QryUpdateAfis()
        {
            string qry = @"UPDATE Solicitantes 
                    SET Afis_Id = @AfisId, Afis_Fecha = @AfisFecha 
                    WHERE Solicitante_Id = @PaqueteId; 
            ";
            return qry;
        }

        public static string QryGetCountReporte()
        {
            string qry = "SELECT COUNT(*) FROM Solicitantes WHERE Activo = true AND Fecha_Envio BETWEEN @FechaInicial AND @FechaFinal; ";
            return qry;
        }
        
        public static string QryGetCountReporteListaNegra()
        {
            string qry = "SELECT COUNT(*) FROM ListaNegra WHERE Activo = true AND Fecha_Registro BETWEEN @FechaInicial AND @FechaFinal; ";
            return qry;
        }

        public static string QryGetReporteSemaforos(bool descargar)
        {
            string qry = @"SELECT 
	                Agencia 
	                , Folio 
	                , FechaEnvio 
	                , SemaforoListaNegra 
	                , SemaforoIdentificacion 
                    , SemaforoFacial SemaforoComparacionFacial 
                    , SemaforoAfis SemaforoHuellas 
	                , TipoDocumento TipoIdentificacion 
            ";
            if (descargar)
                qry += "";
            qry += @"FROM v_validaciones V  
                    WHERE FechaEnvio BETWEEN @FechaInicial AND @FechaFinal 
                    ORDER BY FechaEnvio 
            ";
            return qry;
        }

        public static string QryGetReporteBitacora(bool descargar)
        {
            string qry = "SELECT ";
            if (descargar)
            {
                qry += @"
                        ClienteNombre 
                        , Folio 
                        , EliminacionMotivo 
	                    , TieneAfis 
 	                    , EliminacionFecha 
	                    , EliminacionUsuario 
	                    , AgenciaNombre 
                ";
            }
            else
            {
                qry += @"
	                    ClienteImagen 
                        , ClienteNombre 
                        , Folio 
                        , EliminacionMotivo 
	                    , TieneAfis 
 	                    , EliminacionFecha 
	                    , EliminacionUsuario 
	                    , AgenciaNombre 
                ";
            }
            qry += @"FROM TmpReporteBitacora  
                    WHERE EliminacionFecha BETWEEN @FechaInicial AND @FechaFinal 
                    ORDER BY EliminacionFecha 
            ";
            return qry;
        }

        public static string QryGetReporteListaNegra(bool descargar)
        {
            string qry = @"SELECT 
	            LN.Lista_Negra_Id ListaNegraId 
	            , LN.Solicitante_Id SolicitanteId  
            ";
            if (!descargar)
            {
                qry += @"
	            , (SELECT Imagen FROM Fotos WHERE Solicitante_Id = LN.Solicitante_Id AND Foto_Origen_Id = 1 AND Activo = true) ClienteImagen
                ";
            }
            qry += @"
	            , S.Nombre || ' ' || S.Apellido_Paterno || ' ' || S.Apellido_Materno ClienteNombre 
	            , S.Folio
                , LN.Motivo 
	            , LN.Fecha_Registro FechaIngreso 
	            , A.Nombre_Agencia AgenciaNombre 
                ";
            qry += @"FROM ListaNegra LN 
	                    INNER JOIN Solicitantes S ON LN.Solicitante_Id = S.Solicitante_Id 
	                    INNER JOIN Agencias A ON S.agencia_id = A.agencia_id 
                    WHERE LN.Activo = true 
                        AND LN.Fecha_Registro BETWEEN @FechaInicial AND @FechaFinal 
                        AND LN.Tipo_Movimiento_Id = 1 
                    ORDER BY LN.Fecha_Registro 
            ";
            return qry;
        }

        public static string QryGetReporteSemaforoFacialDetalle(bool descargar)
        {
            string qry = @"SELECT 
	                SemaforoFacial SemaforoComparacionFacial 
	                , null TipoAlerta 
	                , ScoreComparacionFacial ScoreFacial 
	                , ClienteNombreCompleto ClienteNombre 
	                , Folio 
	                , FechaRegistro FechaCaptura 
	                , FechaEnvio 
	                , Agencia NombreAgencia 
            ";
            if (descargar)
            {
                qry += @"
            		, Curp 
                ";
            }
            qry += @"FROM v_validaciones V
                    WHERE FechaEnvio BETWEEN @FechaInicial AND @FechaFinal 
                    ORDER BY NombreAgencia, FechaEnvio 
            ";
            return qry;
        }

        public static string QryGetReporteDetalleEnvio(bool descargar)
        {
            string qry = @"SELECT 
	                    Agencia AgenciaNombre 
	                    , ClienteNombreCompleto ClienteNombre 
	                    , Folio 
	                    , FechaRegistro FechaCaptura 
	                    , FechaEnvio 
	                    , TipoDocumento 
	                    , ResultadoGeneral TipoAlerta
	                    , null IneRespuesta
	                    , (SELECT TR.Tipo_Resolucion_Nombre FROM Resolucion R INNER JOIN TiposResolucion TR ON R.Tipo_Resolucion_Id = TR.Tipo_Resolucion_Id WHERE R.Solicitante_Id = V.SolicitanteId) RevisionAnalista 
	                    , SolicitanteId 
	                    , ValidacionId 
	                    , FechaIne
            ";
            if (descargar)
            {
                qry += "";
            }
            qry += @"FROM v_validaciones V 
                    WHERE FechaEnvio BETWEEN @FechaInicial AND @FechaFinal 
                    ORDER BY Agencia, FechaEnvio 
            ";
            return qry;
        }

        public static string QryFilterBusquedas(BusquedaRequest busqueda)
        {
            string qry = "";
            if (!string.IsNullOrEmpty(busqueda.Nombre))
            {
                qry += $"S.Nombre ILIKE '%{busqueda.Nombre}%' ";
            }
            if (!string.IsNullOrEmpty(busqueda.ApellidoPaterno))
            {
                if (!string.IsNullOrEmpty(qry))
                    qry += "AND ";
                qry += $"S.ApellidoPaterno ILIKE '%{busqueda.ApellidoPaterno}%' ";
            }
            if (!string.IsNullOrEmpty(busqueda.ApellidoMaterno))
            {
                if (!string.IsNullOrEmpty(qry))
                    qry += "AND ";
                qry += $"S.ApellidoMaterno ILIKE '%{busqueda.ApellidoMaterno}%' ";
            }
            if (!string.IsNullOrEmpty(busqueda.Folio))
            {
                if (!string.IsNullOrEmpty(qry))
                    qry += "AND ";
                qry += $"S.Folio ILIKE '%{busqueda.Folio}%' ";
            }
            if (busqueda.FechaInicial != null && busqueda.FechaFinal != null)
            {
                if (!string.IsNullOrEmpty(qry))
                    qry += "AND ";
                qry += $"S.FechaEnvio BETWEEN @FechaInicial AND @FechaFinal ";
            }
            return qry;
        }

        public static string QryGetBusquedas(bool descargar, string filtro)
        {
            string qry = @"SELECT 
	                SolicitanteId 
	                , Folio 
	                , NombreCompleto 
	                , TipoCliente 
                    , TipoDocumento 
	                , CorreoElectronico 
	                , ListaNegra 
	                , FechaEnvio 
	                , EstadoSolicitud 
            ";
            if (descargar)
                qry += "";
            qry += $"FROM v_Solicitantes S WHERE {filtro} ORDER BY FechaEnvio";
            return qry;
        }

        public static string QryGetCountBusquedas(string qryfiltro)
        {
            string qry = $"SELECT COUNT(*) FROM v_Solicitantes S WHERE {qryfiltro}; ";
            return qry;
        }

        public static string QryAddBitacora()
        {
            string qry = @"INSERT INTO Bitacora 
                    (Origen_Id, Tipo_Log_Id, Usuario_Id, Fecha_Registro, Mensaje, Referencia) 
                    VALUES (@OrigenId, @TipoLogId, @UsuarioIdActualizo, @FechaRegistro, @Mensaje, @Referencia); 
                    SELECT currval('bitacora_bitacora_id_seq'); 
            ";
            return qry;
        }

        public static string QryPaginacion(PaginacionRequest paginacion)
        {
            if (paginacion == null)
                return "";
            int registroInicial = (int)((paginacion.Pagina - 1) * paginacion.RegistrosPagina);
            string qry = $" LIMIT {paginacion.RegistrosPagina} ";
            qry += $" OFFSET {registroInicial} ";
            return qry;
        }

        public static string QryGetSolicitudHuellasCoincidentes()
        {
            string qry = @"SELECT 
	                Afis_Id AfisId 
	                , Solicitante_Id SolicitanteId 
	                , Folio 
	                , S.Nombre || ' ' || S.Apellido_Paterno || ' ' || S.Apellido_Materno NombreCompleto 
	                , (SELECT Imagen FROM Fotos WHERE Foto_Origen_Id = 1 AND solicitante_id = S.solicitante_id) Fotografia 
                FROM Solicitantes S 
                WHERE S.Afis_Id = @AfisId AND S.Solicitante_Id <> @SolicitanteId 
                ORDER BY S.Nombre, S.Apellido_Paterno, S.Apellido_Materno, Solicitante_Id 
                ; 
            ";
            return qry;
        }

        public static string QryGetSolicitudDocumento()
        {
            string qry = @"SELECT 
	                Documento_Id DocumentoId 
	                , Solicitante_Id SolicitanteId 
	                , Imagen Documento 
                FROM Documentos  
                WHERE Activo = true
	                AND Documento_Id = @DocumentoId
                ; 
            ";
            return qry;
        }

        public static string QryUpdateSemaforoHuellas()
        {
            string qry = @"UPDATE Validaciones 
                    SET Semaforo_Afis = @Semaforo, Resultado_Afis = @Resultado, Fecha_Afis = @FechaValidacion
                    WHERE Solicitante_Id = @SolicitanteId; 
            ";
            return qry;
        }

        public static string QryUpdateValidacion(string campoSemaforo, string campoResultado, string campoFecha)
        {
            string qry = @$"UPDATE Validaciones 
                    SET {campoSemaforo} = @Semaforo, {campoResultado} = @Resultado, {campoFecha} = @FechaValidacion
                    WHERE Validacion_Id = @ValidacionId; 
            ";
            return qry;
        }

        #endregion

    }
}
