using System;
using System.Collections.Generic;
using System.Text;

namespace AppAC.Domain
{
    public class Administrador : Usuario
    {
        public Administrador(string identificacion, string nombres, string apellidos, string email, string telefono, string sexo) : base(identificacion, nombres, apellidos, email, telefono, sexo)
        {
        }
    }
}
