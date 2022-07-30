namespace Real.Time.Chat.Shared.Kernel.Notifications
{
    public class DomainNotification : Event.Event
    {
        public DomainNotification(string key, string value)
        {
            Key = key;
            Value = value;
        }

        public string Key { get; set; }
        public string Value { get; set; }
    }
}
