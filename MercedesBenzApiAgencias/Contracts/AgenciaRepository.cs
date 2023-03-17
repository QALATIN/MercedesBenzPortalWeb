using Dapper;
using MercedesBenzDBContext;
using MercedesBenzDBValidaciones;
using MercedesBenzLibrary;
using MercedesBenzModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace MercedesBenzApiAgencias.Contracts
{
    public class AgenciaRepository : IAgenciaRepository
    {
        private readonly ApplicationDBContext _context;

        public AgenciaRepository(ApplicationDBContext context)
        {
            _context = context;
        }

        public async Task<(int, IEnumerable<Agencia>)> GetAllAsync(PaginacionRequest paginacion)
        {
            string mensaje = "Error en Agencia-GetAllAsync";
            string qry;
            string qryCount;
            int totalRegistros = 0;
            try
            {
                if (paginacion == null)
                    qry = DbSql.QryGetAgencias(true);
                else
                {
                    qryCount = DbSql.QryGetCountAgencias();
                    qry = qryCount + DbSql.QryGetAgencias(true) + DbSql.QryPaginacion(paginacion);
                }

                using var connection = _context.CreateConnection();
                connection.Open();
                var reader = await connection.QueryMultipleAsync(qry);
                if (paginacion != null)
                    totalRegistros = reader.Read<int>().FirstOrDefault();
                IEnumerable<Agencia> data = reader.Read<Agencia>().ToList();
                return (totalRegistros, data.ToList());
            }
            catch (Exception ex)
            {
                mensaje += "|" + ex.Message;
                throw new InvalidOperationException(mensaje);
            }
        }

        public async Task<Agencia> GetByIdAsync(int agenciaId)
        {
            string mensaje = "Error en Agencia-GetByIdAsync";
            Agencia data = null;
            string qry;
            try
            {
                if(agenciaId > 0)
                {
                    var parameters = DbSql.ParametersAgencias(new Agencia() { AgenciaId = agenciaId });
                    qry = DbSql.QryGetAgencias(false);
                    using var connection = _context.CreateConnection();
                    connection.Open();
                    data = await connection.QuerySingleOrDefaultAsync<Agencia>(qry, parameters);
                }
                return data;
            }
            catch (Exception ex)
            {
                mensaje += "|" + ex.Message;
                throw new InvalidOperationException(mensaje);
            }
        }

        public async Task<Agencia> AddAsync(Agencia agencia)
        {
            string mensaje = "Error en Agencia-AddAsync";
            try
            {
                using var connection = _context.CreateConnection();
                connection.Open();

                ValidarAgenciaExiste(connection, new Agencia()
                {
                    AgenciaId = agencia.AgenciaId
                    , ClaveAgencia = agencia.ClaveAgencia.ToUpper()
                    , NombreAgencia = agencia.NombreAgencia.ToUpper()
                });

                agencia.FechaRegistro = DateTime.Now;
                var parameters = DbSql.ParametersAgencias(agencia);
                string qry = DbSql.QryAddAgencias();
                int id = await connection.QuerySingleAsync<int>(qry, parameters);
                agencia.AgenciaId = id;
                Agencia data = agencia;
                return data;
            }
            catch (Exception ex)
            {
                mensaje += "|" + ex.Message;
                throw new InvalidOperationException(mensaje);
            }
        }

        public async Task<Agencia> UpdateAsync(Agencia agencia, string nombreUsuarioActualizo)
        {
            string mensaje = "Error en Agencia-UpdateAsync";
            try
            {
                using var connection = _context.CreateConnection();
                connection.Open();

                ValidarAgenciaExiste(connection, new Agencia()
                {
                    AgenciaId = agencia.AgenciaId,
                    ClaveAgencia = agencia.ClaveAgencia.ToUpper(),
                    NombreAgencia = agencia.NombreAgencia.ToUpper()
                });

                agencia.FechaRegistro = DateTime.Now;
                var parameters = DbSql.ParametersAgencias(agencia);

                string qry = DbSql.QryGetAgencias(false);
                Agencia dataActual = await connection.QuerySingleOrDefaultAsync<Agencia>(qry, parameters);

                string camposActualizados = string.Empty;
                if (dataActual.ClaveAgencia != agencia.ClaveAgencia)
                    camposActualizados += $"|ClaveAgencia:{dataActual.ClaveAgencia}";
                if (dataActual.NombreAgencia != agencia.NombreAgencia)
                    camposActualizados += $"|NombreAgencia:{dataActual.NombreAgencia}";
                if (dataActual.Direccion != agencia.Direccion)
                    camposActualizados += $"|Direccion:{dataActual.Direccion}";
                if (dataActual.TipoAgenciaId != agencia.TipoAgenciaId)
                    camposActualizados += $"|TipoAgencia:{dataActual.TipoAgenciaNombre}";

                if (!string.IsNullOrEmpty(camposActualizados))
                {
                    var parametersCampo = new DynamicParameters();
                    parametersCampo.Add("@NombreUsuario", nombreUsuarioActualizo);
                    qry = DbSql.QryGetUsuarioIdxNombreUsuario();
                    int UsuarioIdActualizo = await connection.QuerySingleOrDefaultAsync<int>(qry, parametersCampo);

                    parameters.Add("@OrigenId", 1);
                    parameters.Add("@TipoLogId", 2);
                    parameters.Add("@UsuarioIdActualizo", UsuarioIdActualizo);
                    parameters.Add("@FechaRegistro", agencia.FechaRegistro);
                    string bMensaje = "Agencia Actualizada";
                    string bReferencia = $"AgenciaId:{agencia.AgenciaId}{camposActualizados}";
                    parameters.Add("@Mensaje", bMensaje);
                    parameters.Add("@Referencia", bReferencia);

                    qry = DbSql.QryUpdateAgencias();
                    qry += DbSql.QryAddBitacora();
                    _ = await connection.ExecuteAsync(qry, parameters);
                }
                return agencia;
            }
            catch (Exception ex)
            {
                mensaje += "|" + ex.Message;
                throw new InvalidOperationException(mensaje);
            }
        }

        public async Task<bool> DeleteAsync(int agenciaId)
        {
            string mensaje = "Error en Agencia-DeleteAsync";
            bool response = false;
            string qry;
            try
            {
                if (agenciaId > 0)
                {
                    DateTime fechaBaja = DateTime.Now;
                    var parameters = DbSql.ParametersAgencias(new Agencia() { AgenciaId = agenciaId, FechaBaja = fechaBaja });
                    qry = DbSql.QryDeleteAgencia();
                    using var connection = _context.CreateConnection();
                    connection.Open();
                    int affectedRow = await connection.ExecuteAsync(qry, parameters);
                    if (affectedRow > 0)
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

        public async Task<bool> UpdateEstatusAsync(AgenciaEstatusRequest estatus)
        {
            string mensaje = "Error en Agencia-UpdateEstatusAsync";
            bool response = false;
            string qry;
            try
            {
                using var connection = _context.CreateConnection();
                connection.Open();

                qry = DbSql.QryGetAgenciaIdxAgenciaId(estatus.AgenciaId);
                _ = ConsultarCampoId(connection, qry, "AgenciaId");

                qry = DbSql.QryGetUsuarioIdxUsuarioId(estatus.UsuarioIdActualizo);
                _ = ConsultarCampoId(connection, qry, "UsuarioIdActualizo");

                estatus.FechaRegistro = DateTime.Now;
                var parameters = DbSql.ParametersAgenciasUpdateEstatus(estatus);

                string bMensaje = $"Agencia Estatus {(estatus.Activar ? "Activación" : "Desactivación")}";
                string bReferencia = $"AgenciaId:{estatus.AgenciaId}|{estatus.Motivo}";

                parameters.Add("@OrigenId", 1);
                parameters.Add("@TipoLogId", 2);

                parameters.Add("@Mensaje", bMensaje);
                parameters.Add("@Referencia", bReferencia);

                qry = DbSql.QryUpdateAgenciasEstatus(estatus.Activar);
                qry += DbSql.QryAddBitacora();
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


        private static void ValidarAgenciaExiste(IDbConnection connection, Agencia agencia)
        {
            string qry;
            var parameters  = DbSql.ParametersAgencias(agencia);

            qry = DbSql.QryGetAgenciasByClaveOrNombre();
            IEnumerable<Agencia> data = connection.Query<Agencia>(qry, parameters);

            foreach (var item in data)
            {
                string nombreCampo;
                if (item.ClaveAgencia == agencia.ClaveAgencia)
                {
                    nombreCampo = "Clave de agencia";
                    Validaciones.ValidarCampoExiste(nombreCampo, item.AgenciaId, agencia.AgenciaId);
                }

                if (item.NombreAgencia == agencia.NombreAgencia)
                {
                    nombreCampo = "Nombre de agencia";
                    Validaciones.ValidarCampoExiste(nombreCampo, item.AgenciaId, agencia.AgenciaId);
                }
            }
        }

        private static int ConsultarCampoId(IDbConnection connection, string qry, string nombreCampo)
        {
            int id = connection.QuerySingleOrDefault<int>(qry);
            if (id > 0)
                return id;

            throw Validaciones.ExceptionCampoIncorrecto(nombreCampo);
        }

    }
}
