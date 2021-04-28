using AppAC.Domain.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace AppAC.Application.Test.Dobles
{
    class MailServerFake : IMailServer
    {
        public string Send(string subject, string body, string email)
        {
            return "";
        }
    }
}
