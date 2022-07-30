using Real.Time.Chat.Shared.Kernel.Entity;

namespace Real.Time.Chat.Domain.Interfaces.Messaging
{
    public interface IQueueMessageService
    {
        Task SendMessageAsync(MessageDto messageDto);
    }
}
