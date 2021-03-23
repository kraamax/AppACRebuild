using System;
using System.Collections.Generic;
using System.Text;

namespace AppAC.Domain
{
    public class PlazoApertura
    {
        public int IdApertura { get; set; }
        public string FechaInicio { get; set; }
        public string FechaFin { get; set; }
        public bool Activo { get; set; }

    }
}
