using MercedesBenzModel;
using System.Threading.Tasks;

namespace MercedesBenzApiAfis.Contracts
{
    public interface IAfiRepository
    {
        public Task<AfiValidacion> GetAsync();

        public Task<bool> AddAsync(AfisRequest request);

    }
}
