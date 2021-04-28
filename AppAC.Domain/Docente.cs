using System;
using System.Collections.Generic;
using System.Text;

namespace AppAC.Domain
{
    public class Docente : Usuario
    {
        public Docente(string identificacion, string nombres, string apellidos, string email, string telefono, string sexo) : base(identificacion, nombres, apellidos, email, telefono, sexo)
        {
        }

        public Docente(string identificacion, string nombres, string apellidos, string email, string telefono, string sexo, Departamento departamento) : base(identificacion, nombres, apellidos, email, telefono, sexo)
        {
            Departamento = departamento;
        }

        public Departamento Departamento { get; set; }

    }
}
