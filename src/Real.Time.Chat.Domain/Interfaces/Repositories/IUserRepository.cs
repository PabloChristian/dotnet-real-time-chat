using Real.Time.Chat.Domain.Entity;
using Real.Time.Chat.Domain.Interfaces.Repositories;
using System.Collections.Generic;
using System.Linq;

namespace Real.Time.Chat.Domain.Interfaces
{
    public interface IUserRepository : IRepositoryBase<User>
    {
        void Add(Messages messages);
        Task AddAsync(Messages messages, CancellationToken cancellationToken);
        IEnumerable<Messages> GetMessages();
    }
}
