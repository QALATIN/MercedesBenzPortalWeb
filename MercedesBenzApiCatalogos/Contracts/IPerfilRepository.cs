using MercedesBenzModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MercedesBenzApiCatalogos.Contracts
{
    public interface IPerfilRepository
    {
        public Task<IEnumerable<Perfil>> GetAllAsync();

    }
}
