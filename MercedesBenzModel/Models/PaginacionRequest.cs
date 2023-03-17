using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MercedesBenzModel
{
    public class PaginacionRequest
    {
        public int? Pagina { get; set; } = null;
        public int? RegistrosPagina { get; set; } = null;
    }
}
