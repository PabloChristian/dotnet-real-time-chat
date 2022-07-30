using Real.Time.Chat.Domain.Interfaces;
using Real.Time.Chat.Infrastructure.Data.Context;

namespace Real.Time.Chat.Infrastructure.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly RealTimeChatContext _realtimeChatContext;

        public UnitOfWork(RealTimeChatContext realtimeChatContext)
        {
            _realtimeChatContext = realtimeChatContext;
        }

        public bool Commit() => _realtimeChatContext.SaveChanges() > 0;
    }
}
