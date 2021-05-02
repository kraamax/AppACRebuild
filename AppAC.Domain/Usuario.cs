using AppAC.Domain.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace AppAC.Domain
{
    public class Usuario: Entity<int>, IAggregateRoot
    {
        public Usuario(string identificacion, string nombres, string apellidos, string email, string sexo)
        {
            Identificacion = identificacion;
            Nombres = nombres;
            Apellidos = apellidos;
            Email = email;
            Sexo = sexo;
            UserName = Email;
            Password = identificacion;
        }
        public string Identificacion { get; protected set; }
        public string Nombres { get; protected set; }
        public string Apellidos { get; protected set; }
        public string UserName { get; protected set; }
        public string Password { get; protected set; }
        public string Email { get; protected set; }
        public string Sexo { get; protected set; }
    }
}
