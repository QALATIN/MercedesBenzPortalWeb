using Dapper;
using MercedesBenzDBContext;
using MercedesBenzModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MercedesBenzApiCatalogos.Contracts
{
    public class PerfilRepository : IPerfilRepository
    {
        private readonly ApplicationDBContext _context;

        public PerfilRepository(ApplicationDBContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Perfil>> GetAllAsync()
        {
            string mensaje = "Error en Catálogo Perfil-GetAllAsync";
            IEnumerable<Perfil> responsePerfiles = null;
            try
            {
                using var connection = _context.CreateConnection();
                connection.Open();
                string qry = "SELECT Perfil_Id PerfilId, Nombre_Perfil NombrePerfil FROM PERFILES WHERE Activo = true ORDER BY Nombre_Perfil;";
                responsePerfiles = await connection.QueryAsync<Perfil>(qry);
                return responsePerfiles.ToList();
            }
            catch (Exception ex)
            {
                mensaje += "|" + ex.Message;
                throw new InvalidOperationException(mensaje);
            }
        }
    }
}
