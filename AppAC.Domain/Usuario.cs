using AppAC.Domain.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace AppAC.Domain
{
    public class Usuario: Entity<int>, IAggregateRoot
    {
        public Usuario(string identificacion, string nombres, string apellidos, string email, string telefono, string sexo)
        {
            Identificacion = identificacion;
            Nombres = nombres;
            Apellidos = apellidos;
            Email = email;
            Telefono = telefono;
            Sexo = sexo;
            UserName = Email;
            Password = identificacion;
        }
        public string Identificacion { get; private set; }
        public string Nombres { get; private set; }
        public string Apellidos { get; private set; }
        public string UserName { get; private set; }
        public string Password { get; private set; }
        public string Email { get; private set; }
        public string Telefono { get; private set; }
        public string Sexo { get; private set; }
    }
}
