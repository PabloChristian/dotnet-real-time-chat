using Real.Time.Chat.Shared.Kernel.Commands;
using System.Threading.Tasks;

namespace Real.Time.Chat.Shared.Kernel.Handler
{
    public interface IMediatorHandler
    {
        Task<TResult> SendCommandResult<TResult>(ICommandResult<TResult> command);
        Task RaiseEvent<T>(T @event) where T : class;
    }
}
