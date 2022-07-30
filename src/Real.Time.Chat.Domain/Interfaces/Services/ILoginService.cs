using Real.Time.Chat.Shared.Kernel.Entity;
using Real.Time.Chat.Domain.Entity;
using System;

namespace Real.Time.Chat.Domain.Interfaces.Services
{
    public interface ILoginService
    {
        User Authenticate(string email, string password);
        TokenJWT GetToken(Guid id, string email);
    }
}
