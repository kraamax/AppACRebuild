using AppAC.Domain.Contracts;
using System;
using MailKit.Net.Smtp;
using MimeKit;

namespace AppAC.Infrastructure.Systems
{
    public class MailServer : IMailServer
    {
        private NotificationMetadata _notificationMetadata;

        public MailServer(NotificationMetadata notificationMetadata)
        {
            _notificationMetadata = notificationMetadata;
        }
        public string Send(string subject, string body, string email)
        {
            EmailMessage message = new EmailMessage();
            message.Sender = new MailboxAddress("AppAC", _notificationMetadata.Sender);
            message.Reciever = new MailboxAddress("AppAC", email);
            message.Subject = subject;
            message.Content =body;
            var mimeMessage = CreateEmailMessage(message);
            using (SmtpClient smtpClient = new SmtpClient())
            {
                smtpClient.Connect(_notificationMetadata.SmtpServer,
                    _notificationMetadata.Port, true);
                smtpClient.Authenticate(_notificationMetadata.UserName,
                    _notificationMetadata.Password);
                smtpClient.Send(mimeMessage);
                smtpClient.Disconnect(true);
            }
            return "Email sent successfully";
        }
        public string Get()
        {
            EmailMessage message = new EmailMessage();
            message.Sender = new MailboxAddress("Self", _notificationMetadata.Sender);
            message.Reciever = new MailboxAddress("Self", _notificationMetadata.Reciever);
            message.Subject = "Welcome";
            message.Content = "Hello World!";
            var mimeMessage = CreateEmailMessage(message);
            using (SmtpClient smtpClient = new SmtpClient())
            {
                smtpClient.Connect(_notificationMetadata.SmtpServer,
                    _notificationMetadata.Port, true);
                smtpClient.Authenticate(_notificationMetadata.UserName,
                    _notificationMetadata.Password);
                smtpClient.Send(mimeMessage);
                smtpClient.Disconnect(true);
            }
            return "Email sent successfully";
        }
        private MimeMessage CreateEmailMessage(EmailMessage message)
        {
            var mimeMessage = new MimeMessage();
            mimeMessage.From.Add(message.Sender);
            mimeMessage.To.Add(message.Reciever);
            mimeMessage.Subject = message.Subject;
            mimeMessage.Body = new TextPart(MimeKit.Text.TextFormat.Text)
                { Text = message.Content };
            return mimeMessage;
        }
    }
}
