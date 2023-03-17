using MercedesBenzModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MercedesBenzApiCatalogos.Contracts
{
    public interface IAgenciaRepository
    {
        public Task<IEnumerable<AgenciaSeleccion>> GetAllAsync();
        public Task<IEnumerable<AgenciaTipoSeleccion>> TipoAgenciaGetAllAsync();

    }
}
