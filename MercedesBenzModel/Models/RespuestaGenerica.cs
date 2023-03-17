using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MercedesBenzModel
{
    public class RespuestaGenerica
    {
        public string Status { get; set; }
        public string Message { get; set; }
        public dynamic Data { get; set; }

    }
}
