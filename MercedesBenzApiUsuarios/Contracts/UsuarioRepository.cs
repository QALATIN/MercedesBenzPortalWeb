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

namespace MercedesBenzApiUsuarios.Contracts
{
    public class UsuarioRepository : IUsuarioRepository
    {

        private readonly ApplicationDBContext _context;        

        public UsuarioRepository(ApplicationDBContext context)
        {
            _context = context;
        }

        public async Task<(int, IEnumerable<Usuario>)> GetAllAsync(PaginacionRequest paginacion)
        {
            string mensaje = "Error en Usuario-GetAllAsync";
            string qry;
            int totalRegistros = 0;
            try
            {
                if (paginacion == null)
                    qry = DbSql.QryGetUsuarios(false);
                else
                {
                    string qryCount = DbSql.QryCountUsuarios();
                    qry = qryCount + DbSql.QryGetUsuarios(false) + DbSql.QryPaginacion(paginacion);
                }
                using var connection = _context.CreateConnection();
                connection.Open();
                var reader = await connection.QueryMultipleAsync(qry);
                if (paginacion != null)
                    totalRegistros = reader.Read<int>().FirstOrDefault();
                IEnumerable<Usuario> data = reader.Read<Usuario>().ToList();
                return (totalRegistros, data.ToList());
            }
            catch (Exception ex)
            {
                mensaje += "|" + ex.Message;
                throw new InvalidOperationException(mensaje);
            }
        }

        public async Task<Usuario> GetByIdAsync(int id)
        {
            string mensaje = "Error en Usuario-GetByIdAsync";
            Usuario data = null;
            string qry;
            try
            {
                if (id > 0)
                {
                    var parameters = DbSql.ParametersUsuarios(new Usuario() { UsuarioId = id });
                    qry = DbSql.QryGetUsuarios(true);
                    using var connection = _context.CreateConnection();
                    connection.Open();
                    data = await connection.QuerySingleOrDefaultAsync<Usuario>(qry, parameters);
                }
                return data;
            }
            catch (Exception ex)
            {
                mensaje += "|" + ex.Message;
                throw new InvalidOperationException(mensaje);
            }
        }

        public async Task<Usuario> AddAsync(Usuario usuario)
        {
            string mensaje = "Error en Usuario-AddAsync";
            try
            {
                using var connection = _context.CreateConnection();
                connection.Open();

                ValidarUsuarioExiste(connection, new Usuario()
                {
                    UsuarioId = usuario.UsuarioId,
                    NombreUsuario = usuario.NombreUsuario.ToUpper(),
                    CorreoElectronico = usuario.CorreoElectronico.ToUpper(),
                    NombreCompleto = usuario.Nombre.ToUpper() + " " + usuario.ApellidoPaterno.ToUpper() + " " + usuario.ApellidoMaterno.ToUpper()
                });

                usuario.FechaRegistro = DateTime.Now;
                var parameters = DbSql.ParametersUsuarios(usuario);
                string qry = DbSql.QryAddUsuarios();
                int id = await connection.QuerySingleAsync<int>(qry, parameters);
                usuario.UsuarioId = id;
                Usuario data = usuario;
                return data;
            }
            catch (Exception ex)
            {
                mensaje += "|" + ex.Message;
                throw new InvalidOperationException(mensaje);
            }
        }

        public async Task<Usuario> UpdateAsync(Usuario usuario, string nombreUsuarioActualizo)
        {
            string mensaje = "Error en Usuario-UpdateAsync";
            try
            {
                using var connection = _context.CreateConnection();
                connection.Open();

                ValidarUsuarioExiste(connection, new Usuario()
                {
                    UsuarioId = usuario.UsuarioId,
                    NombreUsuario = usuario.NombreUsuario.ToUpper(),
                    CorreoElectronico = usuario.CorreoElectronico.ToUpper(),
                    NombreCompleto = usuario.Nombre.ToUpper() + " " + usuario.ApellidoPaterno.ToUpper() + " " + usuario.ApellidoMaterno.ToUpper()
                });

                usuario.FechaRegistro = DateTime.Now;
                var parameters = DbSql.ParametersUsuarios(usuario);

                string qry = DbSql.QryGetUsuarios(true);
                Usuario dataActual = await connection.QuerySingleOrDefaultAsync<Usuario>(qry, parameters);

                if (dataActual != null)
                {

                    string camposActualizados = "";
                    if (dataActual.Nombre != usuario.Nombre)
                        camposActualizados += $"|Nombre:{dataActual.Nombre}";
                    if (dataActual.ApellidoPaterno != usuario.ApellidoPaterno)
                        camposActualizados += $"|ApellidoPaterno:{dataActual.ApellidoPaterno}";
                    if (dataActual.ApellidoMaterno != usuario.ApellidoMaterno)
                        camposActualizados += $"|ApellidoMaterno:{dataActual.ApellidoMaterno}";
                    if (dataActual.FechaNacimiento != usuario.FechaNacimiento)
                        camposActualizados += $"|FechaNacimiento:{dataActual.FechaNacimiento}";
                    if (dataActual.NombreUsuario != usuario.NombreUsuario)
                        camposActualizados += $"|usuario:{dataActual.NombreUsuario}";
                    if (dataActual.CorreoElectronico != usuario.CorreoElectronico)
                        camposActualizados += $"|CorreoElectronico:{dataActual.CorreoElectronico}";
                    if (dataActual.PerfilId != usuario.PerfilId)
                        camposActualizados += $"|NombrePerfil:{dataActual.NombrePerfil}";
                    if (dataActual.AgenciaId != usuario.AgenciaId)
                        camposActualizados += $"|NombreAgencia:{dataActual.NombreAgencia}";

                    if (!string.IsNullOrEmpty(camposActualizados))
                    {
                        var parametersCampo = new DynamicParameters();
                        parametersCampo.Add("@NombreUsuario", nombreUsuarioActualizo);
                        qry = DbSql.QryGetUsuarioIdxNombreUsuario();
                        int usuarioIdActualizo = await connection.QuerySingleOrDefaultAsync<int>(qry, parametersCampo);
                        parameters.Add("@UsuarioIdActualiza", usuarioIdActualizo);

                        string bMensaje = "Usuario Actualizado";
                        string bReferencia = $"UsuarioId:{usuario.UsuarioId}{camposActualizados}";

                        parameters.Add("@OrigenId", 1);
                        parameters.Add("@TipoLogId", 2);
                        parameters.Add("@UsuarioIdActualizo", usuarioIdActualizo);
                        parameters.Add("@FechaRegistro", usuario.FechaRegistro);
                        parameters.Add("@Mensaje", bMensaje);
                        parameters.Add("@Referencia", bReferencia);

                        qry = DbSql.QryUpdateUsuarios();
                        qry += DbSql.QryAddBitacora();
                        await connection.ExecuteAsync(qry, parameters);
                    }
                    return usuario;
                }
                else
                    throw Validaciones.ExceptionCampoIncorrecto("UsuarioId");

            }
            catch (Exception ex)
            {
                mensaje += "|" + ex.Message;
                throw new InvalidOperationException(mensaje);
            }
        }

        public async Task<bool> DeleteAsync(int id)
        {
            string mensaje = "Error en Usuario-DeleteAsync";
            bool response = false;
            try
            {
                DateTime fechaBaja = DateTime.Now;
                var parameters = DbSql.ParametersUsuarios(new Usuario() { UsuarioId = id, FechaBaja = fechaBaja });
                using var connection = _context.CreateConnection();
                connection.Open();

                string qry = DbSql.QryDeleteUsuario();
                int affectedRow = await connection.ExecuteAsync(qry, parameters);
                if (affectedRow > 0)
                    response = true;
                return response;
            }
            catch (Exception ex)
            {
                mensaje += "|" + ex.Message;
                throw new InvalidOperationException(mensaje);
            }
        }

        public async Task<bool> UpdateEstatusAsync(UsuarioEstatusRequest usuario)
        {
            string mensaje = "Error en Usuario-UpdateEstatusAsync";
            bool response = false;
            try
            {
                using var connection = _context.CreateConnection();
                connection.Open();

                string qry = DbSql.QryGetUsuarioIdExistexUsuarioId(usuario.UsuarioId);
                _ = ConsultarCampoId(connection, qry, "UsuarioId");

                qry = DbSql.QryGetUsuarioIdxUsuarioId(usuario.UsuarioIdActualizo);
                _ = ConsultarCampoId(connection, qry, "UsuarioIdActualizo");

                usuario.FechaRegistro = DateTime.Now;
                var parameters = DbSql.ParametersUsuariosUpdateEstatus(usuario);

                string bMensaje = $"Usuario Estatus {(usuario.Activar ? "Activación" : "Desactivación")}";
                string bReferencia = $"UsuarioId:{usuario.UsuarioId}|{usuario.Motivo}";

                parameters.Add("@OrigenId", 1);
                parameters.Add("@TipoLogId", 2);
                parameters.Add("@Mensaje", bMensaje);
                parameters.Add("@Referencia", bReferencia);

                qry = DbSql.QryUpdateUsuariosEstatus(usuario.Activar);
                qry += DbSql.QryAddBitacora();
                int id = await connection.ExecuteAsync(qry, parameters);
                if (id > 0)
                    response = true;
                return response;
            }
            catch (Exception ex)
            {
                mensaje += "|" +ex.Message;
                throw new InvalidOperationException(mensaje);
            }
        }

        public async Task<UsuarioCuenta> LoginAsync(AutenticacionRequest usuario)
        {
            string mensaje = "Error en Usuario-LoginAsync";
            try
            {
                var parameters = new DynamicParameters();
                parameters.Add("@NombreUsuario", usuario.NombreUsuario);
                parameters.Add("@Password", usuario.Password);
                using var connection = _context.CreateConnection();
                connection.Open();

                string qry = DbSql.QryGetLogin();
                UsuarioCuenta data = await connection.QuerySingleOrDefaultAsync<UsuarioCuenta>(qry, parameters);
                if (data != null)
                {
                    if (usuario.NombreUsuario != data.NombreUsuario 
                        //|| Request.Password != data.Password
                        )
                        data = new UsuarioCuenta();
                }
                return data;
            }
            catch (Exception ex)
            {
                mensaje += "|" + ex.Message;
                throw new InvalidOperationException(mensaje);
            }
        }

        public async Task<bool> LogOutAsync(string nombreUsuario)
        {
            string mensaje = "Error en Usuario-LogOutAsync";
            bool resultado = false;
            try
            {
                var parameters = DbSql.ParametersUsuarios(new Usuario() { NombreUsuario = nombreUsuario });
                using var connection = _context.CreateConnection();
                connection.Open();

                string qry = DbSql.QryGetLogOut();
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

        public async Task<UsuarioCuenta> CredencialAsync(string nombreUsuario)
        {
            string mensaje = "Error en Usuario-CredencialAsync";
            try
            {
                var parameters = new DynamicParameters();
                parameters.Add("@NombreUsuario", nombreUsuario);

                using var connection = _context.CreateConnection();
                connection.Open();

                string qry = DbSql.QryGetCredencial();
                UsuarioCuenta data = await connection.QuerySingleOrDefaultAsync<UsuarioCuenta>(qry, parameters);
                if (data != null)
                    data.NombreUsuario = nombreUsuario;
                return data;
            }
            catch (Exception ex)
            {
                mensaje += "|" + ex.Message;
                throw new InvalidOperationException(mensaje);
            }
        }

        private static void ValidarUsuarioExiste(IDbConnection connection, Usuario usuario) {

            var parameters = DbSql.ParametersUsuarios(usuario);

            string qry = DbSql.QryGetUsuariosByUserNameOrCorreoOrNombre();
            IEnumerable<Usuario> data = connection.Query<Usuario>(qry, parameters);
            
            foreach (var item in data)
            {
                string nombreCampo;
                if (item.NombreUsuario == usuario.NombreUsuario)
                {
                    nombreCampo = "Usuario";
                    Validaciones.ValidarCampoExiste(nombreCampo, item.UsuarioId, usuario.UsuarioId);
                }

                if (item.CorreoElectronico == usuario.CorreoElectronico)
                {
                    nombreCampo = "Correo electrónico";
                    Validaciones.ValidarCampoExiste(nombreCampo, item.UsuarioId, usuario.UsuarioId);
                }

                if (item.NombreCompleto == usuario.NombreCompleto)
                {
                    nombreCampo = "Nombre completo";
                    Validaciones.ValidarCampoExiste(nombreCampo, item.UsuarioId, usuario.UsuarioId);
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
