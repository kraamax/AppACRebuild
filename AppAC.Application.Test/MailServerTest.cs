using AppAc.Application;
using AppAC.Application.Test.Dobles;
using NUnit.Framework;
using AppAC.Domain;
using AppAC.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using AppAC.Infrastructure.Systems;
using System;
using AppAC.Domain.Contracts;
using FluentAssertions;
using Moq;

namespace AppAC.Application.Test
{
    public class MailServerTest
    {
        private IMailServer _mailServer;
        [SetUp]
        public void Setup()
        {
            var metadata = new NotificationMetadata();
            metadata.Sender = "sebastianonatetest@gmail.com";
            metadata.SmtpServer = "smtp.gmail.com";
            metadata.Port = 465;
            metadata.UserName = "sebastianonatetest@gmail.com";
            metadata.Password = "sebastiantest12345";
            _mailServer = new MailServer(metadata);
        }

        [Test]
        public void SendEmailTest()
        {
            var response=_mailServer.Send("prueba email","Provando servicio", "sebastianonatetest@gmail.com");
            response.Should().Be("Email sent successfully");
        }
    }
}