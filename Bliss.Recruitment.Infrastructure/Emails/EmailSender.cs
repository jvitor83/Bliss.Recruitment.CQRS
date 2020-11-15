using Bliss.Recruitment.Application.Configuration.Emails;
using Microsoft.Extensions.Configuration;
using System.Net.Mail;
using System.Threading.Tasks;

namespace Bliss.Recruitment.Infrastructure.Emails
{
    public class EmailSender : IEmailSender
    {
        public readonly EmailsSettings _emailsSettings;
        public EmailSender(EmailsSettings emailsSettings)
        {
            this._emailsSettings = emailsSettings;
        }
        public async Task SendEmailAsync(EmailMessage message)
        {
            string from = message.From ?? _emailsSettings.FromAddressEmail;
            SmtpClient client = new SmtpClient(_emailsSettings.Host, _emailsSettings.Port);
            // TODO: Fix Subject
            client.Send(from, message.To, "Share", message.Content);
            return;
        }
    }
}