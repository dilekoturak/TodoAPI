using System;
using Microsoft.EntityFrameworkCore;

namespace ToDoListAPI.Application.Repositories
{
    public interface IRepository<T> where T : class
    {
        DbSet<T> Table { get; }
    }
}

