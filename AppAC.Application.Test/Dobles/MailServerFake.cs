using AppAC.Domain.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace AppAC.Application.Test.Dobles
{
    class MailServerFake : IMailServer
    {
        public void Send(string v, string email)
        {
            Console.WriteLine("Se enviar el email");
        }
    }
}
