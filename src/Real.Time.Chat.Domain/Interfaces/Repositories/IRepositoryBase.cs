using Real.Time.Chat.Shared.Kernel.Entity;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace Real.Time.Chat.Domain.Interfaces.Repositories
{
    public interface IRepositoryBase<T> where T : EntityBase
    {
        void Add(T entity);
        void Update(T entity);
        void Remove(T entity);
        IQueryable<T> GetAll();
        IQueryable<T> GetByExpression(Expression<Func<T, bool>> predicate);
        T GetById(Guid id);
    }
}
