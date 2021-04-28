using System;
using System.Collections.Generic;
using System.Text;

namespace AppAC.Domain.Contracts
{
    public interface IMailServer
    {
        string Send(string subject, string body, string email);
    }
}
