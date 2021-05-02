using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AppAC.Domain
{
    public class Administrador : Usuario
    {
        public Administrador(string identificacion, string nombres, string apellidos, string email, string sexo) : base(identificacion, nombres, apellidos, email, sexo)
        {
        }
        public IReadOnlyList<string> CanDeliver(string identificacion, string nombres, string apellidos, string email, string sexo) {
            var errors = new List<string>();
            if (string.IsNullOrWhiteSpace(identificacion))
                errors.Add("Debe ingresar un codigo");

            if (string.IsNullOrWhiteSpace(nombres))
                errors.Add("Debe ingresar los nombres");
            
            if (string.IsNullOrWhiteSpace(apellidos))
                errors.Add("Debe ingresar los apellidos");
            
            if (string.IsNullOrWhiteSpace(email))
                errors.Add("Debe ingresar un email");
            
            if (string.IsNullOrWhiteSpace(sexo))
                errors.Add("Debe ingresar un sexo");

            return errors;
        }
        public void Deliver(string identificacion, string nombres, string apellidos, string email, string sexo)
        {
            if (CanDeliver(identificacion, nombres, apellidos, email, sexo).Any())
                throw new InvalidOperationException();
            Identificacion = identificacion;
            Nombres = nombres;
            Apellidos = apellidos;
            Email = email;
            Sexo = sexo;
        }
    }
}
