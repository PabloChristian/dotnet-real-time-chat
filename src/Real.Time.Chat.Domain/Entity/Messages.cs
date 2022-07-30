using Real.Time.Chat.Shared.Kernel.Entity;
using System;

namespace Real.Time.Chat.Domain.Entity
{
    public class Messages : EntityBase
    {
        public string Message { get;set; }
        public string Sender { get; set; }
        public string Consumer { get; set; }
        public DateTime Date { get; set; }
    }
}
