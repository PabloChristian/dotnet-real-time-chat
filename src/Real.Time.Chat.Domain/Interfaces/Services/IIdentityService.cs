using Real.Time.Chat.Shared.Kernel.Entity;
using Real.Time.Chat.Domain.Entity;

namespace Real.Time.Chat.Domain.Interfaces.Services
{
    public interface IIdentityService
    {
        User Authenticate(string username, string password);
        TokenJWT GetToken(Guid id, string username);
    }
}
