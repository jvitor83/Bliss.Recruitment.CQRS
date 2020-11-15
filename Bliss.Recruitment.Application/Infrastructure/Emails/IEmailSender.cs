using Bliss.Recruitment.Application.Configuration.Emails;
using System.Threading.Tasks;

namespace Bliss.Recruitment.Application.Configuration.Emails
{
    public interface IEmailSender
    {
        Task SendEmailAsync(EmailMessage message);
    }
}