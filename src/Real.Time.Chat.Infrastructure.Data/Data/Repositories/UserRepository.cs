using Real.Time.Chat.Domain.Entity;
using Real.Time.Chat.Domain.Interfaces;
using Real.Time.Chat.Infra.Data.Context;
using System.Collections.Generic;
using System.Linq;

namespace Real.Time.Chat.Infra.Data.Repositories
{
    public class UserRepository : RepositoryBase<User>, IUserRepository
    {
        public UserRepository(RealTimeChatContext financialChatContext) : base(financialChatContext)
        {
        }

        public void Add(Messages messages)
        {
            Db.Messages.Add(messages);
        }

        public IEnumerable<Messages> GetMessages() => Db.Messages.Take(50);
    }
}
