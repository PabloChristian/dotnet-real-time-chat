using Real.Time.Chat.Domain.Interfaces;
using Real.Time.Chat.Infra.Data.Context;

namespace Real.Time.Chat.Infrastructure.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly RealTimeChatContext _financialChatContext;

        public UnitOfWork(RealTimeChatContext financialChatContext)
        {
            _financialChatContext = financialChatContext;
        }

        public bool Commit() => _financialChatContext.SaveChanges() > 0;
    }
}
