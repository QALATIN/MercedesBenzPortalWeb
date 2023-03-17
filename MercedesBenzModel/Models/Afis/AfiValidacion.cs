using System.Collections.Generic;

namespace MercedesBenzModel
{
    public class AfiValidacion
    {
        public int PaqueteId { get; set; }
        public string NombreCompleto { get; set; }
        public List<HuellaValidacion> LstHuellas { get; set; }
        public string ProyectoNombre { get; set;}
        public string ProyectoPrefijo { get; set; }

    }
}
