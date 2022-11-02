using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using ToDoListAPI.Persistence.Contexts;

namespace ToDoListAPI.Persistence
{
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<TodoListDbContext>
    {
        public TodoListDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<TodoListDbContext>();
            optionsBuilder.UseNpgsql(Configuration.ConnectionString);

            return new TodoListDbContext(optionsBuilder.Options);
        }
    }
}

