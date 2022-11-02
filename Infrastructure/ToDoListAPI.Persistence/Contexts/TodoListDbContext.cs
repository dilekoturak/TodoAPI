using System;
using Microsoft.EntityFrameworkCore;
using ToDoListAPI.Domain.Entities;

namespace ToDoListAPI.Persistence.Contexts
{
    public class TodoListDbContext : DbContext
    {
        public TodoListDbContext(DbContextOptions options) : base(options)
        {
            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
        }

        public DbSet<TodoItem> TodoItems { get; set; }
    }
}

