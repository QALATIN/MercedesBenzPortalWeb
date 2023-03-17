using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MercedesBenzModel
{
    public class FechaRequest
    {
        [Required(ErrorMessage = "La Fecha inicial es obligatoria")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd MMMM yyyy}")]
        public DateTime FechaInicial { get; set; }
        [Required(ErrorMessage = "La Fecha final es obligatoria")]
        public DateTime FechaFinal { get; set; }
    }
}
