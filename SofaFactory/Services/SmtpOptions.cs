using System;
namespace SofaFactory.Services
{
    public class SmtpOptions
    {
        public string Host { get; set; }

        public string UserName { get; set; }

        public string Password { get; set; }

        public int Port { get; set; }

        public string SendTo { get; set; }

        public string EnquiryEmail { get; set; }
    }
}

