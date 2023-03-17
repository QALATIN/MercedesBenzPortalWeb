using Dapper;
using MercedesBenzDBContext;
using MercedesBenzDBValidaciones;
using MercedesBenzModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MercedesBenzApiBusquedas.Contracts
{
    public class BusquedaRepository : IBusquedaRepository
    {
        private readonly ApplicationDBContext _context;

        public BusquedaRepository(ApplicationDBContext context)
        {
            _context = context;
        }

        public async Task<(int, IEnumerable<BusquedaResultado>)> BusquedaAsync(BusquedaRequest busqueda, PaginacionRequest paginacion)
        {
            string mensaje = "Error en Búsqueda-BusquedaAsync";
            try
            {
                var parameters = DbSql.ParametersBusquedas(busqueda);

                string qryFiltro = DbSql.QryFilterBusquedas(busqueda);

                if (string.IsNullOrEmpty(qryFiltro))
                {
                    mensaje = "Debe ingresar un campo de búsqueda";
                    throw new InvalidOperationException(mensaje);
                }
                else
                {
                    int totalRegistros = 0;
                    string qry;
                    string qrySelect = DbSql.QryGetBusquedas(false, qryFiltro);
                    if (paginacion == null)
                        qry = qrySelect;
                    else
                    {
                        string qryCount = DbSql.QryGetCountBusquedas(qryFiltro);
                        qry = qryCount + qrySelect + DbSql.QryPaginacion(paginacion);
                    }

                    using var connection = _context.CreateConnection();
                    connection.Open();
                    var reader = await connection.QueryMultipleAsync(qry, parameters);
                    if (paginacion != null)
                        totalRegistros = reader.Read<int>().FirstOrDefault();
                    IEnumerable<BusquedaResultado> data = reader.Read<BusquedaResultado>().ToList();
                    return (totalRegistros, data.ToList());
                }
            }
            catch (Exception ex)
            {
                mensaje += "|" + ex.Message;
                throw new InvalidOperationException(mensaje);
            }
        }

    }
}
