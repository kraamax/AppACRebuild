﻿using System;
using System.Collections.Generic;
using System.Text;

namespace AppAC.Domain
{
    public class Usuario
    {
        public int Id { get; set; }
        public string Nombres { get; set; }
        public string Apellidos { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string Telefono { get; set; }
        public string Sexo { get; set; }
    }
}
