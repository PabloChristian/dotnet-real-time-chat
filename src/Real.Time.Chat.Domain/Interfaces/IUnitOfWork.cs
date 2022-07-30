namespace Real.Time.Chat.Domain.Interfaces
{
    public interface IUnitOfWork
    {
        bool Commit();
    }
}
