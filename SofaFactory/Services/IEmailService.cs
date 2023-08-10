using System;
namespace SofaFactory.Services
{
    public interface IEmailService
    {
        Task SendEmailAsyn(string fromAddress, string toAddress, string subject, string message);
    }
}

