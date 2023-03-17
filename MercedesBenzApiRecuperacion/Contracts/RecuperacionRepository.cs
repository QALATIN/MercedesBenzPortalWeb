using Dapper;
using MercedesBenzDBContext;
using MercedesBenzDBValidaciones;
using MercedesBenzLibrary;
using MercedesBenzModel;
using System;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace MercedesBenzApiRecuperacion.Contracts
{
    public class RecuperacionRepository : IRecuperacionRepository
    {
        private readonly ApplicationDBContext _context;

        public RecuperacionRepository(ApplicationDBContext context)
        {
            _context = context;
        }

        public async Task<(EmailDatos, Correo, string)> GetDatosCorreoAsync(string correo)
        {
            string mensaje = "Error en Recuperacion-GetDatosCorreoAsync";
            Correo data = new();
            try
            {
                using var connection = _context.CreateConnection();
                connection.Open();

                var parameters = new DynamicParameters();
                parameters.Add("@Correo", correo);
                string qry = DbSql.QryGetCorreoUsuario();
                EmailDatos emailDatos = await connection.QuerySingleOrDefaultAsync<EmailDatos>(qry, parameters);

                string urlPortal;
                if (emailDatos == null)
                    throw Validaciones.ExceptionCampoIncorrecto("correo electrónico");
                else
                {
                    qry = DbSql.QryGetDatosCorreo();

                    var reader = await connection.QueryMultipleAsync(qry);
                    data.Libreria = reader.Read<string>().FirstOrDefault();
                    data.Key = reader.Read<string>().FirstOrDefault();
                    data.Account = reader.Read<string>().FirstOrDefault();
                    data.Name = reader.Read<string>().FirstOrDefault();
                    data.Password = reader.Read<string>().FirstOrDefault();
                    data.Host = reader.Read<string>().FirstOrDefault();
                    data.Puerto = reader.Read<string>().FirstOrDefault();
                    data.Ssl = reader.Read<string>().FirstOrDefault();
                    data.DefaultCredentials = reader.Read<string>().FirstOrDefault();

                    urlPortal = reader.Read<string>().FirstOrDefault();
                }
                return (emailDatos, data, urlPortal);
            }
            catch (Exception ex)
            {
                mensaje += "|" + ex.Message;
                throw new InvalidOperationException(mensaje);
            }
        }

        public async Task<bool> CreateTokenRecuperacionAsync(int usuarioId, string token)
        {
            string mensaje = "Error en Recuperacion-CreateTokenRecuperacionAsync";
            bool resultado = false;
            try
            {
                DateTime fechaCreacion = DateTime.Now;
                var parameters = new DynamicParameters();
                parameters.Add("@UsuarioId", usuarioId);
                parameters.Add("@Token", token);
                parameters.Add("@FechaCreacion", fechaCreacion);

                using var connection = _context.CreateConnection();
                connection.Open();

                string qry = "SELECT Param_Valor FROM Parametros WHERE Param_Nombre = 'TokenRecuperacionVigencia';";
                string vigenciaRecuperacionHoras = connection.QuerySingleOrDefault<string>(qry);
                if (!int.TryParse(vigenciaRecuperacionHoras, out int vigenciaHoras))
                    vigenciaHoras = 24;

                DateTime FechaVigencia = fechaCreacion.AddHours(vigenciaHoras);
                parameters.Add("@FechaVigencia", FechaVigencia);

                qry = DbSql.QryCreateTokenRecuperacion();
                int id = await connection.ExecuteAsync(qry, parameters);
                if (id > 0)
                    resultado = true;
                return resultado;
            }
            catch (Exception ex)
            {
                mensaje += "|" + ex.Message;
                throw new InvalidOperationException(mensaje);
            }
        }

        public async Task<bool> UpdatePasswordAsync(ResetPasswordRequest request)
        {
            string mensaje = "Error en Recuperacion-UpdatePasswordAsync";
            bool resultado = false;
            try
            {
                DateTime FechaActualizacion = DateTime.Now;
                var parameters = new DynamicParameters();
                parameters.Add("@Password", request.Password);
                parameters.Add("@FechaActualizacion", FechaActualizacion);

                using var connection = _context.CreateConnection();
                connection.Open();

                string qry = DbSql.QryGetUsuarioIdxCorreo();
                var parametersCampo = new DynamicParameters();
                parametersCampo.Add("@Correo", request.CorreoElectronico);
                int usuarioId = ConsultarCampoId(connection, qry, "correo electrónico", parametersCampo);

                parameters.Add("@UsuarioId", usuarioId);
                qry = DbSql.QryUpdatePassword();
                int id = await connection.ExecuteAsync(qry, parameters);
                if (id > 0)
                    resultado = true;
                return resultado;
            }
            catch (Exception ex)
            {
                mensaje += "|" + ex.Message;
                throw new InvalidOperationException(mensaje);
            }
        }

        public async Task<(string, ValidacionTokenRecuperacion)> GetValidacionTokenRecuperacionAsync(string token)
        {
            string mensaje = "Error en Recuperacion-GetValidacionTokenRecuperacionAsync";
            string resultado = "";
            try
            {
                var parameters = new DynamicParameters();
                parameters.Add("@Token", token);

                using var connection = _context.CreateConnection();
                connection.Open();
                string qry = DbSql.QryGetTokenRecuperacion();

                ValidacionTokenRecuperacion data = await connection.QuerySingleOrDefaultAsync<ValidacionTokenRecuperacion>(qry, parameters);
                if (data == null)
                    resultado = "El Link es inválido";
                else
                {
                    if (DateTime.Now > data.TokenVigencia)
                    {
                        resultado = "El Link ha expirado";
                    }
                }
                return (resultado, data);
            }
            catch (Exception ex)
            {
                mensaje += "|" + ex.Message;
                throw new InvalidOperationException(mensaje);
            }
        }

        public async Task<ApkVersion> GetApkVersionAsync()
        {
            string mensaje = "Error en Recuperacion-GetApkVersionAsync";
            ApkVersion data = new();
            try
            {
                using var connection = _context.CreateConnection();
                connection.Open();

                string qry = DbSql.QryGetApkVersion();
                var reader = await connection.QueryMultipleAsync(qry);
                data.Fecha = reader.Read<string>().FirstOrDefault();
                data.Version = reader.Read<string>().FirstOrDefault();
                data.Actualizar = reader.Read<string>().FirstOrDefault();
                return data;
            }
            catch (Exception ex)
            {
                mensaje += "|" + ex.Message;
                throw new InvalidOperationException(mensaje);
            }
        }

        private static int ConsultarCampoId(IDbConnection connection, string qry, string nombreCampo, DynamicParameters parameters)
        {
            int id = connection.QuerySingleOrDefault<int>(qry, parameters);
            if (id > 0)
                return id;

            throw Validaciones.ExceptionCampoIncorrecto(nombreCampo);
        }

    }
}
