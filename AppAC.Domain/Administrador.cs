using System;
using System.Collections.Generic;
using System.Text;

namespace AppAC.Domain
{
    class Administrador : Usuario
    {
        public Administrador(string id, string nombres, string apellidos, string email, string telefono, string sexo) : base(id, nombres, apellidos, email, telefono, sexo)
        {
        }
    }
}
