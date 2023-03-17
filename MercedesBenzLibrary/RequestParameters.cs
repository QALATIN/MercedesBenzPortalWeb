using MercedesBenzModel;

namespace MercedesBenzLibrary
{
    public static class RequestParameters
    {

        public static PaginacionRequest ConvertPagination(string pages)
        {
            PaginacionRequest paginacion = null;
            if (!string.IsNullOrEmpty(pages))
                paginacion = CodificacionBase64.DecodificarObjeto(pages, new PaginacionRequest());
            return paginacion;
        }

        public static (int, string, PaginacionRequest) ValidarPaginacion(PaginacionRequest paginacion)
        {
            int statusCode = 400;
            string mensaje = "";
            PaginacionRequest paginacionValidada = null;

            if (paginacion.Pagina == null && paginacion.RegistrosPagina == null)
                mensaje += "|Todo";
            else
            {
                if (paginacion.Pagina == null || paginacion.Pagina <= 0)
                {
                    mensaje += $"|El valor '{paginacion.Pagina}' no es válido en el campo Pagina";
                    return (statusCode, mensaje, paginacionValidada);
                }
                if (paginacion.RegistrosPagina == null || paginacion.RegistrosPagina <= 0)
                {
                    mensaje += $"|El valor '{paginacion.RegistrosPagina}' no es válido en el campo RegistrosPagina";
                    return (statusCode, mensaje, paginacionValidada);
                }
                mensaje += $"|Pagina: {paginacion.Pagina}, RegistrosPagina: {paginacion.RegistrosPagina}";
                paginacionValidada = paginacion;
            }
            return (200, mensaje, paginacionValidada);
        }

        public static BusquedaRequest ConvertBusqueda(string busqueda)
        {
            BusquedaRequest request = null;
            if (!string.IsNullOrEmpty(busqueda))
                request = CodificacionBase64.DecodificarObjeto(busqueda, new BusquedaRequest());
            return request;
        }

        public static (int, string) ValidarBusqueda(BusquedaRequest busqueda)
        {
            string filtro = "";

            if (!string.IsNullOrEmpty(busqueda.Nombre))
            {
                filtro = $"Nombre: '{busqueda.Nombre}' ";
            }
            if (!string.IsNullOrEmpty(busqueda.ApellidoPaterno))
            {
                if (!string.IsNullOrEmpty(filtro))
                    filtro += ", ";
                filtro += $"ApellidoPaterno: '{busqueda.ApellidoPaterno}' ";
            }
            if (!string.IsNullOrEmpty(busqueda.ApellidoMaterno))
            {
                if (!string.IsNullOrEmpty(filtro))
                    filtro += ", ";
                filtro += $"ApellidoMaterno: '{busqueda.ApellidoMaterno}' ";
            }
            if (!string.IsNullOrEmpty(busqueda.Folio))
            {
                if (!string.IsNullOrEmpty(filtro))
                    filtro += ", ";
                filtro += $"Folio: '{busqueda.Folio}' ";
            }
            if (busqueda.FechaInicial != null && busqueda.FechaFinal != null)
            {
                if (!string.IsNullOrEmpty(filtro))
                    filtro += ", ";
                filtro += $"Fecha: Del {busqueda.FechaInicial.Value:dd MMMM yyyy} al {busqueda.FechaFinal.Value:dd MMMM yyyy}";
            }
            int statusCode = 400;
            string mensaje;
            if (string.IsNullOrEmpty(filtro))
            {
                mensaje = "|Debe ingresar un campo de búsqueda";
                return (statusCode, mensaje);
            }
            mensaje = "|" + filtro;

            return (200, mensaje);

        }

        public static (int, string) ValidarFechas(FechaRequest periodo)
        {
            int statusCode = 400;
            string mensaje = "";
            var periodoVacio = new FechaRequest();

            if (periodo.FechaInicial == periodoVacio.FechaInicial && periodo.FechaFinal == periodoVacio.FechaFinal)
            {
                mensaje += "|Debe enviar los datos para el Periodo (FechaInicial=AAAA-MM-DD 00:00:00, FechaFinal=AAAA-MM-DD 23:59:59)";
                return (statusCode, mensaje);
            }
            else
            {
                if (periodo.FechaInicial == periodoVacio.FechaInicial)
                {
                    mensaje += $"|El valor '{periodo.FechaInicial}' no es válido en el campo FechaInicial";
                    return (statusCode, mensaje);
                }
                if (periodo.FechaFinal == periodoVacio.FechaFinal)
                {
                    mensaje += $"|El valor '{periodo.FechaFinal}' no es válido en el campo FechaFinal";
                    return (statusCode, mensaje);
                }
                mensaje += $"|Del {periodo.FechaInicial} Al {periodo.FechaFinal}";
                return (200, mensaje);
            }
        }

    }
}
