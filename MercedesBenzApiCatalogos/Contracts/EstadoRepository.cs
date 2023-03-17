using Dapper;
using MercedesBenzDBContext;
using MercedesBenzModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MercedesBenzApiCatalogos.Contracts
{
    public class EstadoRepository : IEstadoRepository
    {
        private readonly ApplicationDBContext _context;

        public EstadoRepository(ApplicationDBContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Estado>> GetAllAsync()
        {
            string mensaje = "Error en Catálogo Perfil-GetAllAsync";
            IEnumerable<Estado> responseEstados = null;
            try
            {
                using (var connection = _context.CreateConnection())
                {
                    connection.Open();
                    string qry = "SELECT Estado_Id EstadoId, Nombre_Estado NombreEstado FROM ESTADOS ORDER BY Nombre_Estado;";
                    responseEstados = await connection.QueryAsync<Estado>(qry);
                }
                return responseEstados.ToList();
            }
            catch (Exception ex)
            {
                mensaje += "|" + ex.Message;
                throw new InvalidOperationException(mensaje);
            }
        }
    }
}
