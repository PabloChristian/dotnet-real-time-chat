using Real.Time.Chat.Shared.Kernel.Entity;

namespace Real.Time.Chat.Domain.Entity
{
    public class User : EntityBase
    {
        public string Name { get; set; } = string.Empty;
        public string UserName { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }
}
