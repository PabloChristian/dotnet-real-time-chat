using System;
using System.Collections.Generic;
using System.Text;

namespace Real.Time.Chat.Shared.Kernel.Entity
{
    public class MessageDto
    {
        public string Sender { get; set; }
        public string Consumer { get; set; }
        public string Message { get; set; }
        public DateTime Date { get; set; }
    }
}
