using System;
using System.Collections.Generic;
using System.Text;

namespace AppAC.Domain
{
    public class Usuario
    {
        public Usuario(string id, string nombres, string apellidos, string email, string telefono, string sexo)
        {
            Id = id;
            Nombres = nombres;
            Apellidos = apellidos;
            Email = email;
            Telefono = telefono;
            Sexo = sexo;
            UserName = Email;
            Password = id;
        }
        public string Id { get; private set; }
        public string Nombres { get; private set; }
        public string Apellidos { get; private set; }
        public string UserName { get; private set; }
        public string Password { get; private set; }
        public string Email { get; private set; }
        public string Telefono { get; private set; }
        public string Sexo { get; private set; }
    }
}
