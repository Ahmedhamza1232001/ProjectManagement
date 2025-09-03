using ProjectManagement.Application.Abstractions.Services;
using System.Net.Mail;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace ProjectManagement.Infrastructure.Services
{
    public class EmailService : IEmailService
    {
        private readonly string _smtpHost;
        private readonly int _smtpPort;
        private readonly string _smtpUser;
        private readonly string _smtpPass;
        private readonly string _fromEmail;

        public EmailService()
        {
            // For demo purposes, hard-coded values
            _smtpHost = "smtp.example.com";
            _smtpPort = 587;
            _smtpUser = "yourusername@example.com";
            _smtpPass = "yourpassword";
            _fromEmail = "no-reply@example.com";
        }

        public async Task SendEmailAsync(string to, string subject, string body, CancellationToken cancellationToken = default)
        {
            using var message = new MailMessage();
            message.From = new MailAddress(_fromEmail);
            message.To.Add(to);
            message.Subject = subject;
            message.Body = body;
            message.IsBodyHtml = true;

            using var client = new SmtpClient(_smtpHost, _smtpPort)
            {
                Credentials = new NetworkCredential(_smtpUser, _smtpPass),
                EnableSsl = true
            };

            // Send asynchronously
            await client.SendMailAsync(message, cancellationToken);
        }
    }
}
