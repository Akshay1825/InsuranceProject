using MailKit.Net.Smtp;
using MimeKit;

namespace InsuranceProject.Services
{
    public class EmailService
    {
        private readonly string _smtpServer = "smtp.gmail.com"; // Update based on your SMTP provider
        private readonly int _smtpPort = 587; // Port for TLS
        private readonly string _smtpUsername = "brandbhai46@gmail.com";
        private readonly string _smtpPassword = "xwkatgoffqkcloin"; // Use environment variables in production

        public void SendEmail(string toEmail, string subject, string body)
        {
            var email = new MimeMessage();
            email.From.Add(new MailboxAddress("Shubham Pawar", _smtpUsername));
            email.To.Add(new MailboxAddress("", toEmail));
            email.Subject = subject;

            email.Body = new TextPart("html")
            {
                Text = body
            };

            using var smtpClient = new SmtpClient();
            try
            {
                smtpClient.Connect(_smtpServer, _smtpPort, MailKit.Security.SecureSocketOptions.StartTls);
                smtpClient.Authenticate(_smtpUsername, _smtpPassword);
                smtpClient.Send(email);
            }
            finally
            {
                smtpClient.Disconnect(true);
            }
        }
    }
}
