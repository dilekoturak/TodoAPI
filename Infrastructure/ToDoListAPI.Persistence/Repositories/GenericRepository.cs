using System;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using ToDoListAPI.Application.Repositories;
using ToDoListAPI.Persistence.Contexts;

namespace ToDoListAPI.Persistence.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly TodoListDbContext _context;

        public GenericRepository(TodoListDbContext context)
        {
            _context = context;
        }

        public DbSet<T> Table => _context.Set<T>();

        public async Task<bool> Add(T model)
        {
            EntityEntry<T> entityEntry = await Table.AddAsync(model);
            return entityEntry.State == EntityState.Added;
        }

        public IQueryable<T> GetAll()
        {
            var query = Table.AsQueryable();
            return query;
        }

        public bool Remove(T model)
        {
            EntityEntry<T> entityEntry = Table.Remove(model);
            return entityEntry.State == EntityState.Deleted;
        }

        public async Task<bool> Remove(string id)
        {
            var _id = Guid.Parse(id);
            T model = await Table.FindAsync(_id);
            return Remove(model);
        }

        public async Task<T> GetByIdAsync(string id)
        {
            var _id = Guid.Parse(id);
            return await Table.FindAsync(_id);
        }

        public async Task<int> SaveAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public IQueryable<T> GetWhere(Expression<Func<T, bool>> method)
        {
            var query = Table.Where(method);
            return query;
        }
    }
}

