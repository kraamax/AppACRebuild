﻿using MimeKit;

namespace AppAC.Infrastructure.Systems
{
    public class EmailMessage
    {
        public MailboxAddress Sender { get; set; }
        public MailboxAddress Reciever { get; set; }
        public string Subject { get; set; }
        public string Content { get; set; }
        
        
    }
    
}