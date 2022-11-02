using System;
using System.Linq.Expressions;

namespace ToDoListAPI.Application.Repositories
{
    public interface IGenericRepository<T> : IRepository<T> where T : class
    {
        Task<bool> Add(T model);
        bool Remove(T model);
        Task<bool> Remove(string id);
        IQueryable<T> GetAll();
        Task<int> SaveAsync();
        Task<T> GetByIdAsync(string id);
        IQueryable<T> GetWhere(Expression<Func<T, bool>> method);

    }
}

