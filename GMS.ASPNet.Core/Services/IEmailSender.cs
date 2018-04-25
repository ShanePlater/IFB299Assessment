using System.Threading.Tasks;

namespace GMS.ASPNet.Core.Services
{
    public interface IEmailSender
    {
        Task SendEmailAsync(string email, string subject, string message);
    }
}
