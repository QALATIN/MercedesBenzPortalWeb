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

namespace MercedesBenzApiAfis.Contracts
{
    public class AfiRepository : IAfiRepository
    {
        private readonly ApplicationDBContext _context;
        private readonly string _repositoryPath;

        public AfiRepository(ApplicationDBContext context)
        {
            _context = context;
            _repositoryPath = ApplicationSettings.GetRepositoryPath();
        }

        public async Task<AfiValidacion> GetAsync()
        {
            string mensaje = "Error en Afis-GetAsync";
            try
            {
                using var connection = _context.CreateConnection();
                connection.Open();

                string qry = DbSql.QryGetAfisValidar();
                AfiValidacion data = await connection.QuerySingleOrDefaultAsync<AfiValidacion>(qry);
                if(data != null)
                    if (data.PaqueteId > 0)
                    {
                        var afisRequest = new AfisRequest() { PaqueteId = data.PaqueteId, AfisId = 0 };
                        var parameters = DbSql.ParametersAfis(afisRequest);

                        qry = DbSql.QryGetDatosProyecto();
                        qry += DbSql.QryGetAfisHuellas();
                        var reader = await connection.QueryMultipleAsync(qry, parameters);
                        data.ProyectoNombre = reader.Read<string>().FirstOrDefault();
                        data.ProyectoPrefijo = reader.Read<string>().FirstOrDefault();
                        data.LstHuellas = (List<HuellaValidacion>)reader.Read<HuellaValidacion>();
                    }
                return data;
            }
            catch (Exception ex)
            {
                mensaje += "|" + ex.Message;
                throw new InvalidOperationException(mensaje);
            }
        }

        public async Task<bool> AddAsync(AfisRequest request)
        {
            string mensaje = "Error en Afis-AddPostAsync";
            try
            {
                using var connection = _context.CreateConnection();
                connection.Open();

                string qry = DbSql.QryGetNombreClientexSolicitanteId(request.PaqueteId);
                string nombreCliente = ConsultarCampoTexto(connection, qry, "PaqueteId");

                DateTime fechaValidacion = DateTime.Now;
                var parameters = DbSql.ParametersAfis(request);
                parameters.Add("@AfisFecha", fechaValidacion);

                using var transaction = connection.BeginTransaction();
                try
                {
                    qry = DbSql.QryUpdateAfis();
                    _ = await connection.ExecuteAsync(qry, parameters);

                    string repositoryPath = Validaciones.GenerarPathFile(_repositoryPath, fechaValidacion);
                    string pathFile = Validaciones.GenerarPathFileHuellasValidacion(repositoryPath, request.PaqueteId);

                    await DbValidaciones.HuellasValidar(connection, request.PaqueteId, request.AfisId, fechaValidacion, nombreCliente, pathFile);

                    transaction.Commit();
                    return true;
                }
                catch (Exception ex)
                {
                    _ = ex.Message;
                    transaction.Rollback();
                    throw new InvalidOperationException("Error inesperado");
                }
            }
            catch (Exception ex)
            {
                mensaje += "|" + ex.Message;
                throw new InvalidOperationException(mensaje);
            }
        }

        private static string ConsultarCampoTexto(IDbConnection connection, string qry, string nombreCampo)
        {
            string texto = connection.QuerySingleOrDefault<string>(qry);
            if (!string.IsNullOrEmpty(texto))
                return texto;

            throw Validaciones.ExceptionCampoIncorrecto(nombreCampo);
        }

    }
}
