using Dapper;
using MercedesBenzLibrary;
using Npgsql;
using System;
using System.Data;

namespace MercedesBenzDBContext
{
    public class ApplicationDBContext
    {
        private readonly string _connectionString;

        public ApplicationDBContext()
        {
            _connectionString = ApplicationSettings.GetConnectionString();
        }

        public IDbConnection CreateConnection()
        {
            return new NpgsqlConnection(_connectionString);
        }

        public bool TokenValido(string NombreUsuario, string Autorizacion)
        {
            string mensaje = "Error en DBContext-TokenValido";
            bool resultado = false;
            try
            {
                string[] autorizacion = Autorizacion.Split(' ');
                var parameters = new DynamicParameters();
                parameters.Add("@NombreUsuario", NombreUsuario);
                parameters.Add("@Token", autorizacion[1]);

                string qry = @"SELECT 
	                    Token_Vigencia 
                        FROM USUARIOS
                        WHERE Nombre_Usuario = @NombreUsuario AND Token = @Token AND Activo = true 
                ";

                using var connection = CreateConnection();
                connection.Open();
                DateTime tokenVigencia = connection.QuerySingleOrDefault<DateTime>(qry, parameters);
                if (tokenVigencia != DateTime.MinValue)
                    if(tokenVigencia.ToShortDateString() == DateTime.Now.ToShortDateString())
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
