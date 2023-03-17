using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MercedesBenzModel.Models
{
    public class Paginacion
    {

        public int Pagina { get; set; }
        public bool Habilitada { get; set; }
        public bool Activa { get; set; }
        public string Texto { get; set; }

        public Paginacion(int pagina) 
            : this (pagina, true)
        {
        }

        public Paginacion(int pagina, bool habilitada) 
            : this (pagina, habilitada, pagina.ToString())
        {

        }

        public Paginacion(int pagina, bool habilitada, string texto) 
        {
            Pagina = pagina;
            Habilitada = habilitada;
            Texto = texto;
        }
    }
}
