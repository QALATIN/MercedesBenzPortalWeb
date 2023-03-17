using Dapper;
using MercedesBenzApiServicios.Services.Contracts;
using MercedesBenzDBContext;
using MercedesBenzDBValidaciones;
using MercedesBenzLibrary;
using MercedesBenzModel;
using System;
using System.Data;
using System.IO;
using System.Threading.Tasks;

namespace MercedesBenzApiServicios.Services
{
    public class GeneralRepository : IGeneralRepository
    {
        private readonly ApplicationDBContext _context;

        public GeneralRepository(ApplicationDBContext context)
        {
            _context = context;
        }

        public async Task<BitacoraRequest> AddBitacoraPostAsync(BitacoraRequest Bitacora)
        {
            string mensaje = "Error en AddBitacoraPostAsync";
            try
            {
                using var connection = _context.CreateConnection();
                connection.Open();

                string qry = DbSql.QryGetAgenciaIdxUsuarioId(Bitacora.UsuarioId);
                _ = ConsultarCampoId(connection, qry, "UsuarioId");

                Bitacora.FechaRegistro = DateTime.Now;
                var parameters = DbSql.ParametersBitacora(Bitacora);
                qry = DbSql.QryAddBitacora();
                int id = await connection.QuerySingleAsync<int>(qry, parameters);
                Bitacora.BitacoraId = id;
                BitacoraRequest data = Bitacora;

                return data;
            }
            catch (Exception ex)
            {
                mensaje += "|" + ex.Message;
                throw new InvalidOperationException(mensaje);
            }
        }


        public bool SubirArchivoBitacora(BitacoraArchivoRequest bitacora, string Repositorio)
        {
            string mensaje = "Error en SubirArchivoBitacora";
            bool response = false;
            try
            {
                DateTime fecha_dinamica = DateTime.Now;

                if (!string.IsNullOrEmpty(bitacora.Base64))
                {
                    string carpeta_year = fecha_dinamica.Year.ToString().PadLeft(4, '0');
                    string carpeta_month = fecha_dinamica.Month.ToString().PadLeft(4, '0');
                    string carpeta_day = fecha_dinamica.Day.ToString().PadLeft(4, '0');
                    string CarpetaLog = "LogArchivos";
                    Repositorio =  Path.Combine(Repositorio,CarpetaLog);
                    if(!Directory.Exists(Repositorio))
                    {
                        Directory.CreateDirectory(Repositorio);
                    }

                    if (!Directory.Exists(Path.Combine(Repositorio, carpeta_year)))
                    {
                        if (!Directory.Exists(Path.Combine(Repositorio, carpeta_year, carpeta_month)))
                        {
                            if (!Directory.Exists(Path.Combine(Repositorio, carpeta_year, carpeta_month, carpeta_day)))
                            {
                                Directory.CreateDirectory(Path.Combine(Repositorio, carpeta_year));
                                Directory.CreateDirectory(Path.Combine(Repositorio, carpeta_year, carpeta_month));
                                Directory.CreateDirectory(Path.Combine(Repositorio, carpeta_year, carpeta_month, carpeta_day));
                            }
                        }
                    }
                    else
                    {
                        if (!Directory.Exists(Path.Combine(Repositorio, carpeta_year, carpeta_month)))
                        {
                            if (!Directory.Exists(Path.Combine(Repositorio, carpeta_year, carpeta_month, carpeta_day)))
                            {
                                Directory.CreateDirectory(Path.Combine(Repositorio, carpeta_year, carpeta_month));
                                Directory.CreateDirectory(Path.Combine(Repositorio, carpeta_year, carpeta_month, carpeta_day));
                            }
                        }
                        else
                        {
                            if (!Directory.Exists(Path.Combine(Repositorio, carpeta_year, carpeta_month, carpeta_day)))
                            {
                                Directory.CreateDirectory(Path.Combine(Repositorio, carpeta_year, carpeta_month, carpeta_day));
                            }
                        }
                    }

                    string fileName = Path.Combine(Repositorio, carpeta_year, carpeta_month, carpeta_day,DateTime.Now.ToString("ddMMyyyyHHmmssfff")) + ".log";
                    File.WriteAllBytes(fileName, Convert.FromBase64String(bitacora.Base64));
                    response = true;
                }
                return response;
            }
            catch (Exception ex)
            {
                mensaje += "|" + ex.Message;
                throw new InvalidOperationException(mensaje);
            }
        }

        public async Task<bool> UpdateValidacionPostAsync(ValidationResult validation)
        {
            string mensaje = "Error en UpdateValidacionPostAsync";
            bool response = false;
            try
            {
                (string CampoSemaforo, string CampoResultado, string CampoFecha) = Validaciones.ComprobarTipoValidacion(validation.TipoValidacion);

                using var connection = _context.CreateConnection();
                connection.Open();

                string qry = DbSql.QryGetValidacionIdxValidacionId(validation.ValidacionId);
                _ = ConsultarCampoId(connection, qry, "ValidacionId");

                var parameters = DbSql.ParametersUpdateValidacion(validation);
                qry = DbSql.QryUpdateValidacion(CampoSemaforo, CampoResultado, CampoFecha);
                int id = await connection.ExecuteAsync(qry, parameters);
                if (id > 0)
                    response = true;
                
                return response;
            }
            catch (Exception ex)
            {
                mensaje += "|" + ex.Message;
                throw new InvalidOperationException(mensaje);
            }
        }

        private static int ConsultarCampoId(IDbConnection Connection, string Qry, string NombreCampo)
        {
            int id = Connection.QuerySingleOrDefault<int>(Qry);
            if (id > 0)
                return id;

            throw Validaciones.ExceptionCampoIncorrecto(NombreCampo);
        }

    }
}
