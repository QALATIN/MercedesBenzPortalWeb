using MercedesBenzModel;
using System.Threading.Tasks;

namespace MercedesBenzApiIneValidacion.Contracts
{
    public interface IIneRepository
    {
        public Task<bool> AddPostAsync(IneValidacionRequest Request);
        public Task<bool> AddPostAsync2(IneValidacionRequest Request);
    }
}
