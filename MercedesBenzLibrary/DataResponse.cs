using MercedesBenzModel;
using MercedesBenzSecurity;
using System;

namespace MercedesBenzLibrary
{
    public static class DataResponse
    {
        public static object PaginateData(object data, PaginacionRequest paginacion, int totalRegistros, bool isCifrado)
        {
            object dataResponder;
            if (isCifrado)
                dataResponder = EncryptData(data, paginacion, totalRegistros);
            else
                dataResponder = FormatData(data, paginacion, totalRegistros);
            return dataResponder;
        }

        private static string EncryptData(object data, PaginacionRequest paginacion, int totalRegistros)
        {
            var dataFormatear = FormatData(data, paginacion, totalRegistros);
            string dataCifrada = EncryptionAes.EncryptObject(dataFormatear);
            //var prueba = EncryptionAes.DecryptObject(dataCifrada, new RespuestaPaginada());
            return dataCifrada;
        }

        private static object FormatData(object data, PaginacionRequest paginacion, int totalRegistros)
        {
            if (paginacion == null)
                return data;

            int totalPaginas = Convert.ToInt32(Math.Ceiling(((double)totalRegistros / (double)paginacion.RegistrosPagina)));
            var dataPaginada = new RespuestaPaginada()
            {
                TotalRegistros = totalRegistros,
                PaginaActual = (int)paginacion.Pagina,
                TotalPaginas = totalPaginas,
                RegistrosPagina = (int)paginacion.RegistrosPagina,
                Data = data
            };

            return dataPaginada;
        }
    }
}
