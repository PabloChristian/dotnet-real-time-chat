using Real.Time.Chat.Domain.Entity;
using Real.Time.Chat.Domain.Interfaces;
using Real.Time.Chat.Infrastructure.Data.Context;

namespace Real.Time.Chat.Infrastructure.Data.Repositories
{
    public class UserRepository : RepositoryBase<User>, IUserRepository
    {
        public UserRepository(RealTimeChatContext realtimeChatContext) : base(realtimeChatContext) {}

        public void Add(Messages messages) => Db.Messages.Add(messages);
        public async Task AddAsync(Messages messages, CancellationToken cancellationToken) => await Db.Messages.AddAsync(messages, cancellationToken);
        public IEnumerable<Messages> GetMessages() => Db.Messages.OrderByDescending(x => x.Date).Take(50);
    }
}
