using Real.Time.Chat.Shared.Kernel.Entity;

namespace Real.Time.Chat.Application.Interfaces
{
    public interface IQueueMessageService
    {
        Task SendMessageAsync(MessageDto messageDto);
    }
}
