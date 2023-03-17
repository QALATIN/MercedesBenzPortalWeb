using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MercedesBenzModel
{
    public class SemaforoPonderacion
    {
        public string Semaforo { get; set; }
        public float VerdePuntos { get; set; } = 0;
        public float Verde2Puntos { get; set; } = 0;
        public float AmarilloPuntos { get; set; } = 0;
        public float Amarillo2Puntos { get; set; } = 0;
        public float RojoPuntos { get; set; } = 0;
        public float Rojo2Puntos { get; set; } = 0;
        public float GrisPuntos { get; set; } = 0;
    }
}
