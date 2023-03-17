using Dapper;
using MercedesBenzDBContext;
using MercedesBenzDBValidaciones;
using MercedesBenzLibrary;
using MercedesBenzModel;
using MoreLinq;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace MercedesBenzApiReportes.Contracts
{
    public class ReporteRepository : IReporteRepository
    {
        private readonly ApplicationDBContext _context;
        private readonly Routes _routes;

        public ReporteRepository(ApplicationDBContext context, Routes routes)
        {
            _context = context;
            _routes = routes;
        }

        public async Task<(int, IEnumerable<ReporteSemaforo>)> SemaforosAsync(FechaRequest fechas, PaginacionRequest paginacion)
        {
            string mensaje = "Fallo la ejecución Reporte-SemaforosAsync";
            string qry = "";
            int totalRegistros = 0;
            try
            {
                var parameters = DbSql.ParametersReportes(fechas);
                string qrySelect = DbSql.QryGetReporteSemaforos(false);  
                if (paginacion == null)
                    qry = qrySelect;
                else
                {
                    string qryCount = DbSql.QryGetCountReporte();   
                    qry += qryCount + qrySelect + DbSql.QryPaginacion(paginacion);
                }
                using var connection = _context.CreateConnection();
                connection.Open();
                var reader = await connection.QueryMultipleAsync(qry, parameters);
                if (paginacion != null)
                    totalRegistros = reader.Read<int>().FirstOrDefault();
                IEnumerable<ReporteSemaforo> data = reader.Read<ReporteSemaforo>().ToList();
                return (totalRegistros, data.ToList());
            }
            catch (Exception ex)
            {
                mensaje += "|" + ex.Message;
                throw new InvalidOperationException(mensaje);
            }
        }

        public async Task<DataTable> SemaforosDescargarAsync(FechaRequest fechas)
        {
            string mensaje = "Fallo la ejecución Reporte-SemaforosDescargarAsync";
            DataTable data = null;
            try
            {
                var parameters = DbSql.ParametersReportes(fechas);
                string qry = DbSql.QryGetReporteSemaforos(true); 
                using var connection = _context.CreateConnection();
                connection.Open();
                IEnumerable<ReporteSemaforo> resultado = await connection.QueryAsync<ReporteSemaforo>(qry, parameters);
                if (resultado.Any())
                    data = resultado.ToDataTable();

                return data;
            }
            catch (Exception ex)
            {
                mensaje += "|" + ex.Message;
                throw new InvalidOperationException(mensaje);
            }
        }

        public async Task<(int, IEnumerable<ReporteBitacora>)> BitacoraAsync(FechaRequest fechas, PaginacionRequest paginacion)
        {
            string mensaje = "Fallo la ejecución Reporte-BitacoraAsync";
            string qry = "";
            int totalRegistros = 0;
            try
            {
                var parameters = DbSql.ParametersReportes(fechas);
                string qrySelect = DbSql.QryGetReporteBitacora(false);   
                if (paginacion == null)
                    qry = qrySelect;
                else
                {
                    string qryCount = DbSql.QryGetCountReporte();    
                    qry += qryCount + qrySelect + DbSql.QryPaginacion(paginacion);
                }
                using var connection = _context.CreateConnection();
                connection.Open();
                var reader = await connection.QueryMultipleAsync(qry, parameters);
                if (paginacion != null)
                    totalRegistros = reader.Read<int>().FirstOrDefault();
                IEnumerable<ReporteBitacora> data = reader.Read<ReporteBitacora>().ToList();
                return (totalRegistros, data.ToList());
            }
            catch (Exception ex)
            {
                mensaje += "|" + ex.Message;
                throw new InvalidOperationException(mensaje);
            }
        }

        public async Task<DataTable> BitacoraDescargarAsync(FechaRequest fechas)
        {
            string mensaje = "Fallo la ejecución Reporte-BitacoraDescargarAsync";
            DataTable data = null;
            try
            {
                var parameters = DbSql.ParametersReportes(fechas);
                string qry = DbSql.QryGetReporteBitacora(true);
                using var connection = _context.CreateConnection();
                connection.Open();
                IEnumerable<ReporteBitacoraDescarga> resultado = await connection.QueryAsync<ReporteBitacoraDescarga>(qry, parameters);
                if (resultado.Any())
                    data = resultado.ToDataTable();
                return data;
            }
            catch (Exception ex)
            {
                mensaje += "|" + ex.Message;
                throw new InvalidOperationException(mensaje);
            }
        }

        public async Task<(int, IEnumerable<ReporteListaNegra>)> ListaNegraAsync(FechaRequest fechas, PaginacionRequest paginacion)
        {
            string mensaje = "Fallo la ejecución Reporte-ListaNegraAsync";
            string qry = "";
            int totalRegistros = 0;
            try
            {
                var parameters = DbSql.ParametersReportes(fechas);
                string qrySelect = DbSql.QryGetReporteListaNegra(false); 

                if (paginacion == null)
                    qry = qrySelect;
                else
                {
                    string qryCount = DbSql.QryGetCountReporteListaNegra();  
                    qry += qryCount + qrySelect + DbSql.QryPaginacion(paginacion);
                }

                using var connection = _context.CreateConnection();
                connection.Open();
                var reader = await connection.QueryMultipleAsync(qry, parameters);
                if (paginacion != null)
                    totalRegistros = reader.Read<int>().FirstOrDefault();
                IEnumerable<ReporteListaNegra> data = reader.Read<ReporteListaNegra>().ToList();

                return (totalRegistros, data.ToList());
            }
            catch (Exception ex)
            {
                mensaje += "|" + ex.Message;
                throw new InvalidOperationException(mensaje);
            }
        }

        public async Task<DataTable> ListaNegraDescargarAsync(FechaRequest fechas)
        {
            string mensaje = "Fallo la ejecución Reporte-ListaNegraDescargarAsync";
            DataTable data = null;
            try
            {
                var parameters = DbSql.ParametersReportes(fechas);

                string qry = DbSql.QryGetReporteListaNegra(true);
                using var connection = _context.CreateConnection();
                connection.Open();
                IEnumerable<ReporteListaNegraDescarga> resultado = await connection.QueryAsync<ReporteListaNegraDescarga>(qry, parameters);
                if (resultado.Any())
                    data = resultado.ToDataTable();
                return data;
            }
            catch (Exception ex)
            {
                mensaje += "|" + ex.Message;
                throw new InvalidOperationException(mensaje);
            }
        }

        public async Task<(int, IEnumerable<ReporteSemaforoFacialDetalleResponse>)> SemaforoFacialDetalleAsync(FechaRequest fechas, PaginacionRequest paginacion)
        {
            string mensaje = "Error en Reporte-SemaforoFacialDetalleAsync";
            IEnumerable<ReporteSemaforoFacialDetalleResponse> dataResponse = Array.Empty<ReporteSemaforoFacialDetalleResponse>();
            string qry = "";
            int totalRegistros = 0;
            try
            {
                var parameters = DbSql.ParametersReportes(fechas);
                string qrySelect  = DbSql.QryGetReporteSemaforoFacialDetalle(false);  
                if (paginacion == null)
                    qry = qrySelect;
                else
                {
                    string qryCount = DbSql.QryGetCountReporte();   
                    qry += qryCount + qrySelect + DbSql.QryPaginacion(paginacion);
                }

                using var connection = _context.CreateConnection();
                connection.Open();
                var reader = await connection.QueryMultipleAsync(qry, parameters);
                if (paginacion != null)
                    totalRegistros = reader.Read<int>().FirstOrDefault();
                IEnumerable<ReporteSemaforoFacialDetalle> data = reader.Read<ReporteSemaforoFacialDetalle>().ToList();
                if (data.Any())
                {
                    foreach (var item in data)
                        item.TipoAlerta = Validaciones.CalcularAlertaxSemaforo(item.SemaforoComparacionFacial);
                    dataResponse = data;
                }
                return (totalRegistros, dataResponse.ToList());
            }
            catch (Exception ex)
            {
                mensaje += "|" + ex.Message;
                throw new InvalidOperationException(mensaje);
            }
        }

        public async Task<DataTable> SemaforoFacialDetalleDescargarAsync(FechaRequest fechas)
        {
            string mensaje = "Fallo la ejecución Reporte-SemaforoFacialDetalleDescargarAsync";
            DataTable data = null;
            try
            {
                var parameters = DbSql.ParametersReportes(fechas);

                string qry = DbSql.QryGetReporteSemaforoFacialDetalle(true);
                using var connection = _context.CreateConnection();
                connection.Open();
                IEnumerable<ReporteSemaforoFacialDetalle> resultado = await connection.QueryAsync<ReporteSemaforoFacialDetalle>(qry, parameters);
                if (resultado.Any())
                {
                    foreach (var item in resultado)
                        item.TipoAlerta = Validaciones.CalcularAlertaxSemaforo(item.SemaforoComparacionFacial);
                    IEnumerable<ReporteSemaforoFacialDetalleResponse> resultadoResponse = resultado;
                    data = resultadoResponse.ToDataTable();
                }
                return data;
            }
            catch (Exception ex)
            {
                mensaje += "|" + ex.Message;
                throw new InvalidOperationException(mensaje);
            }
        }

        public async Task<(int, IEnumerable<ReporteDetalleEnvioResponse>)> DetalleEnvioAsync(FechaRequest fechas, PaginacionRequest paginacion)
        {
            string mensaje = "Error en Reporte-DetalleEnvioAsync";
            IEnumerable<ReporteDetalleEnvioResponse> dataResponse = Array.Empty<ReporteDetalleEnvioResponse>();
            string qry = "";
            int totalRegistros = 0;
            try
            {
                var parameters = DbSql.ParametersReportes(fechas);
                string qrySelect = DbSql.QryGetReporteDetalleEnvio(false);   
                if(paginacion == null)
                    qry = qrySelect;
                else
                {
                    string qryCount = DbSql.QryGetCountReporte();    
                    qry += qryCount + qrySelect + DbSql.QryPaginacion(paginacion);
                }
                using var connection = _context.CreateConnection();
                connection.Open();
                var reader = await connection.QueryMultipleAsync(qry, parameters);
                if (paginacion != null)
                    totalRegistros = reader.Read<int>().FirstOrDefault();
                IEnumerable<ReporteDetalleEnvio> data = reader.Read<ReporteDetalleEnvio>().ToList();

                if (data.Any()) 
                {
                    foreach (var item in data)
                    {
                        item.TipoAlerta = Validaciones.CalcularAlertaxResultado(item.TipoAlerta);
                        if (item.TipoDocumento == "Mexico (MEX) Voter Identification Card")
                            item.IneRespuesta = RespuestaIneConsultar(item.ValidacionId, item.FechaIne);
                        else
                            item.IneRespuesta = "NA";
                    }
                    dataResponse = data;
                }
                return (totalRegistros, dataResponse.ToList());
            }
            catch (Exception ex)
            {
                mensaje += "|" + ex.Message;
                throw new InvalidOperationException(mensaje);
            }
        }

        public async Task<DataTable> DetalleEnvioDescargarAsync(FechaRequest fechas)
        {
            string mensaje = "Fallo la ejecución Reporte-DetalleEnvioDescargarAsync";
            DataTable data = null;
            try
            {
                var parameters = DbSql.ParametersReportes(fechas);

                string qry = DbSql.QryGetReporteDetalleEnvio(true);
                using var connection = _context.CreateConnection();
                connection.Open();
                IEnumerable<ReporteDetalleEnvio> resultado = await connection.QueryAsync<ReporteDetalleEnvio>(qry, parameters);
                if (resultado.Any())
                {
                    foreach (var item in resultado)
                    {
                        item.TipoAlerta = Validaciones.CalcularAlertaxResultado(item.TipoAlerta);
                        if (item.TipoDocumento == "Mexico (MEX) Voter Identification Card")
                            item.IneRespuesta = RespuestaIneConsultar(item.ValidacionId, item.FechaIne);
                        else
                            item.IneRespuesta = "NA";
                    }
                    IEnumerable<ReporteDetalleEnvioResponse> resultadoResponse = resultado;
                    data = resultadoResponse.ToDataTable();
                }

                return data;
            }
            catch (Exception ex)
            {
                mensaje += "|" + ex.Message;
                throw new InvalidOperationException(mensaje);
            }
        }

        private string RespuestaIneConsultar(int validacionId, DateTime? fechaIne)
        {
            string respuesta = "-";
            string prefijoServicioExterno = "08";
            int longitudServicioExterno = 12;
            if (fechaIne != null)
            {
                try
                {
                    string nombreArchivo = $"{prefijoServicioExterno}{validacionId.ToString().PadLeft(longitudServicioExterno, '0')}.json";
                    string repositoryPath = Validaciones.GenerarPathFile(_routes.RepositoryPath, (DateTime)fechaIne);
                    string pathFile = $"{repositoryPath}\\{nombreArchivo}";
                    if (File.Exists(pathFile))
                    {
                        var streamReader = new StreamReader(pathFile);
                        string jsonString = streamReader.ReadToEnd();
                        streamReader.Dispose();
                        var ineResponse = JsonConvert.DeserializeObject<IneResponse>(jsonString);
                        respuesta = ineResponse.datos_validacion.Respuesta;
                    }
                }
                catch (Exception ex)
                {
                    respuesta = ex.Message;
                }
            }

            return respuesta;

        }

    }
}
