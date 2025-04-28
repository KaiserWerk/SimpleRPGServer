using SimpleRPGServer.Persistence.Models.Auth;
using System.Threading.Tasks;

namespace SimpleRPGServer.Service;

public interface IEmailService
{
    public Task SendRegistrationConfirmationMail(RegistrationRequest req, string confirmationCode);
}
