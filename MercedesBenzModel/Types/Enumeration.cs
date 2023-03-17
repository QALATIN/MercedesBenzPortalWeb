namespace MercedesBenzModel
{
    public enum TipoServicio
    {
        Agencia,
        AgenciaPorId,
        AgenciaAgregar,
        AgenciaActualizar,
        AgenciaEliminar,
        AgenciaSeleccion,

        Usuario,
        UsuarioPorId,
        UsuarioAgregar,
        UsuarioActualizar,
        UsuarioEliminar,
        UsuarioLogin,

        Busqueda,
        BusquedaSolicitudesEstatus,

        ReporteSemaforos,
        ReporteSemaforosDescargar,
        ReporteBitacora,
        ReporteBitacoraDescargar,
        ReporteListaNegra,
        ReporteListaNegraDescargar,
        ReporteSemaforoFacialDetalle,
        ReporteSemaforoFacialDetalleDescargar,
        ReporteDetalleEnvio,
        ReporteDetalleEnvioDescargar,

        PaqueteSolicitantes,
        PaqueteHuellas,
        PaqueteDocumentos,
        PaqueteAvisosPrivacidad,
        PaqueteListaNegra,
        PaqueteResolucion,
        PaqueteNotificacion,
        PaqueteMapaDomicilio
    }

    public enum TipoPopup
    {
        Ok,
        OkCancel,
        DeleteCancel,
        Yes,
        YesNo
    }

    public enum TipoAccion
    {
        Agregar,
        Editar,
        Eliminar
    }

    public enum TipoSemaforo
    {
        Desconocido,
        ListaNegra,
        Identificacion,
        Privacidad,
        ComparacionFacial,
        Huellas,
        Ine,
        Correo,
        Telefono,
        Curp,
        ComprobanteIngresos,
        ComprobanteDomicilio,
        ComprobanteBancario,
        ListasInteres,
        Estado,
        Otro
    }

    public enum SolicitudEstatus
    {
        Nuevas,
        Proceso,
        Finalizadas
    }

    public enum TipoEstatus
    {
        Nueva,
        Proceso,
        Finalizada
    }

    public enum TipoDocumento
    {
        DocumentoDesconocido,
        DocumentoIngresos,
        DocumentoDomicilio,
        DocumentoBancario
    }

    public enum LogLevel {
        Trace = 0, 
        Debug = 1, 
        Information = 2, 
        Warning = 3, 
        Error = 4, 
        Critical = 5,
        None = 6
    }

}
