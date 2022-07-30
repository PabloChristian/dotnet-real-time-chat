using Real.Time.Chat.Domain.Interfaces.Repositories;
using Real.Time.Chat.Shared.Kernel.Entity;
using Real.Time.Chat.Infrastructure.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace Real.Time.Chat.Infrastructure.Data.Repositories
{
    public class RepositoryBase<T> : IRepositoryBase<T> where T : EntityBase
    {
        protected readonly RealTimeChatContext Db;
        protected readonly DbSet<T> DbSet;

        public RepositoryBase(RealTimeChatContext realtimeChatContext)
        {
            Db = realtimeChatContext;
            DbSet = Db.Set<T>();
        }

        public void Add(T entity) => DbSet.Add(entity);

        public IQueryable<T> GetAll() => DbSet;

        public IQueryable<T> GetByExpression(System.Linq.Expressions.Expression<Func<T, bool>> predicate) => DbSet.Where(predicate);

        public T GetById(Guid id) => DbSet?.Find(id);

        public void Remove(T entity) => DbSet.Remove(entity);

        public void Update(T entity) => DbSet.Update(entity);
    }
}
