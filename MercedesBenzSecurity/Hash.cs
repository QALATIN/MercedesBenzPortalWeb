using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MercedesBenzSecurity.Clases
{
    public static class Hash
    {
        public static bool CheckTime(DateTime hash)
        {
            DateTime fechaActual = DateTime.Now;
            DateTime fechaOperacion = (hash);
            TimeSpan ts = fechaOperacion - fechaActual;

            if (Math.Abs(ts.Minutes) < 5)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
