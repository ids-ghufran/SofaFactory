using Microsoft.Extensions.Options;
using MimeKit;
using MimeKit.Text;
using MailKit.Security;
using MailKit.Net.Smtp;

namespace SofaFactory.Services
{
    public class EmailService : IEmailService
    {
        private readonly IOptions<SmtpOptions> _options;

        public EmailService(IOptions<SmtpOptions> options)
        {
            _options = options;
        }

        public async Task SendEmailAsyn(string fromAddress, string toAddress, string subject, string message)
        {
            var email = new MimeMessage();
            email.From.Add(MailboxAddress.Parse(fromAddress));
            email.To.Add(MailboxAddress.Parse(toAddress));
            email.Subject = subject;

            email.Body = new TextPart(TextFormat.Html) { Text = message };

            using var smtp = new SmtpClient();
            await smtp.ConnectAsync(_options.Value.Host, _options.Value.Port, SecureSocketOptions.Auto);
            await smtp.AuthenticateAsync(_options.Value.UserName, _options.Value.Password);
            await smtp.SendAsync(email);
            await smtp.DisconnectAsync(true);
        }
    }
}

