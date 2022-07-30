using Real.Time.Chat.Shared.Kernel.Entity;

namespace Real.Time.Chat.Domain.Entity
{
    public class User : EntityBase
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
