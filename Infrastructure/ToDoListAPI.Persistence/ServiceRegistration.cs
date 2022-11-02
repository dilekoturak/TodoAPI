using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ToDoListAPI.Application;
using ToDoListAPI.Application.Repositories;
using ToDoListAPI.Persistence.Contexts;
using ToDoListAPI.Persistence.Repositories;

namespace ToDoListAPI.Persistence
{
    public static class ServiceRegistration
    {
        public static void AddPersistenceServices(this IServiceCollection services)
        {
            services.AddDbContext<TodoListDbContext>(options => options.UseNpgsql(Configuration.ConnectionString));
            services.AddScoped<ITodoItemRepository, TodoItemRepository>();
        }
    }
}

