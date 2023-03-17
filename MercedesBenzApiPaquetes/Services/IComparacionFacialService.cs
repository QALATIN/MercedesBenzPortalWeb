using MercedesBenzModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MercedesBenzApiPaquetes.Services
{
    public interface IComparacionFacialService
    {
        Task<(string, ComparacionfacialResponse)> ValidarComparacionFacial(string objectSerialize); 
    }
}
