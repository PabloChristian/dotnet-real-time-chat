using System;
using System.Collections.Generic;
using System.Text;

namespace Real.Time.Chat.Shared.Kernel.Entity
{
    public class UserDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string UserName { get; set; } = string.Empty;
    }
}
