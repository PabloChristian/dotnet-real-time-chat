using MediatR;

namespace Real.Time.Chat.Shared.Kernel.Commands
{
    public interface ICommandResult<T> : IRequest<T>
    {
        bool IsValid();
    }
}
