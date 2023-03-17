using Dapper;
using MercedesBenzDBContext;
using MercedesBenzModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MercedesBenzApiCatalogos.Contracts
{
    public class AgenciaRepository : IAgenciaRepository
    {
        private readonly ApplicationDBContext _context;

        public AgenciaRepository(ApplicationDBContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<AgenciaSeleccion>> GetAllAsync()
        {
            string mensaje = "Error en Catálogo Agencia-GetAllAsync";
            IEnumerable<AgenciaSeleccion> responseEstados = null;
            try
            {
                using (var connection = _context.CreateConnection())
                {
                    connection.Open();
                    string qry = "SELECT Agencia_Id AgenciaId, Nombre_Agencia NombreAgencia FROM Agencias Where Activo = true ORDER BY nombre_agencia;";
                    responseEstados = await connection.QueryAsync<AgenciaSeleccion>(qry);
                }
                return responseEstados.ToList();
            }
            catch (Exception ex)
            {
                mensaje += "|" + ex.Message;
                throw new InvalidOperationException(mensaje);
            }
        }

        public async Task<IEnumerable<AgenciaTipoSeleccion>> TipoAgenciaGetAllAsync()
        {
            string mensaje = "Error en Catálogo Agencia-TipoAgenciaGetAllAsync";
            IEnumerable<AgenciaTipoSeleccion> responseEstados = null;
            try
            {
                using (var connection = _context.CreateConnection())
                {
                    connection.Open();
                    string qry = "SELECT Tipo_Agencia_Id TipoAgenciaId, Tipo_Agencia_Nombre TipoAgenciaNombre FROM AgenciasTipo Where Activo = true ORDER BY Tipo_Agencia_Nombre;";
                    responseEstados = await connection.QueryAsync<AgenciaTipoSeleccion>(qry);
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
