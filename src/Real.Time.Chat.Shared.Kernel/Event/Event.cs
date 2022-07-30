using MediatR;
using System;

namespace Real.Time.Chat.Shared.Kernel.Event
{
    public abstract class Event : IRequest<bool>, INotification
    {
        public DateTime DateAndTime { get; private set; }
        protected Event()
        {
            DateAndTime = DateTime.Now;
        }
        
    }
}
