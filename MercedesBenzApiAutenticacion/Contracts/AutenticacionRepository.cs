using Dapper;
using MercedesBenzDBContext;
using MercedesBenzDBValidaciones;
using MercedesBenzModel;
using System;
using System.Threading.Tasks;

namespace MercedesBenzApiAutenticacion.Contracts
{
    public class AutenticacionRepository : IAutenticacionRepository
    {

        private readonly ApplicationDBContext _context;

        public AutenticacionRepository(ApplicationDBContext context)
        {
            _context = context;
        }

        public async Task<UsuarioResultado> AuthenticationAsync(AutenticacionRequest User)
        {
            string mensaje = "Error en Autenticacion-AuthenticationAsync";
            try
            {
                var parameters = new DynamicParameters();
                parameters.Add("@NombreUsuario", User.NombreUsuario);

                string qry = DbSql.QryGetAutentication();
                using var connection = _context.CreateConnection();
                connection.Open();
                UsuarioResultado data = await connection.QuerySingleOrDefaultAsync<UsuarioResultado>(qry, parameters);
                if (data != null)
                {
                    if (User.NombreUsuario != data.NombreUsuario || User.Password != data.Password)
                        data = new();
                }
                return data;
            }
            catch (Exception ex)
            {
                mensaje += "|" + ex.Message;
                throw new InvalidOperationException(mensaje);
            }
        }

        public async Task<bool> CreateTokenUsuarioAsync(UsuarioAutenticado Usuario)
        {
            string mensaje = "Error en Autenticacion-CreateTokenUsuarioAsync";
            bool resultado = false;
            try
            {
                var parameters = new DynamicParameters();
                parameters.Add("@NombreUsuario", Usuario.NombreUsuario);
                parameters.Add("@Token", Usuario.Token);
                parameters.Add("@TokenVigencia", Usuario.TokenVigencia);

                string qry = DbSql.QryCreateTokenUsuario();

                using var connection = _context.CreateConnection();
                connection.Open();
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

    }
}
