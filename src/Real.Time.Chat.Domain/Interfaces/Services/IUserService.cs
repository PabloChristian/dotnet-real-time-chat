using Real.Time.Chat.Domain.Entity;
using Real.Time.Chat.Shared.Kernel.Entity;
using System;
using System.Collections.Generic;

namespace Real.Time.Chat.Domain.Interfaces.Services
{
    public interface IUserService
    {
        List<UserDto> GetUsers();
        UserDto GetUser(Guid id);
        List<Messages> GetMessages();
        List<Messages> GetMessages(string email);
    }
}
