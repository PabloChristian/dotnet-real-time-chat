using System;
using System.Collections.Generic;
using System.Text;

namespace Real.Time.Chat.Shared.Kernel.Entity
{
    public class MessageDto
    {
        public string Sender { get; set; } = string.Empty;
        public string Consumer { get; set; } = string.Empty;
        public string Message { get; set; } = string.Empty;
        public DateTime Date { get; set; }
    }
}
