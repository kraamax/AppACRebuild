using System;
using System.Collections.Generic;
using System.Text;

namespace AppAC.Domain
{
    public class Departamento
    {
        public Departamento(string codigoDpto, string nombreDpto)
        {
            CodigoDpto = codigoDpto;
            NombreDpto = nombreDpto;
        }
        public string CodigoDpto { get; set; }
        public string NombreDpto { get; set; }
    }
}
