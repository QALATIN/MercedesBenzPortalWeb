﻿using System;

namespace MercedesBenzModel
{
    public class ReporteBitacora
    {
        public byte[] ClienteImagen { get; set; }
        public string ClienteNombre { get; set; }
        public string Folio { get; set; }
        public string EliminacionMotivo { get; set; }
        public string TieneAfis { get; set; }
        public DateTime EliminacionFecha { get; set; }
        public string EliminacionUsuario { get; set; }
        public string AgenciaNombre { get; set; }

    }
}
