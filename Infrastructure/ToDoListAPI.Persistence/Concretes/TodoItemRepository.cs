using System;
using ToDoListAPI.Application.Repositories;
using ToDoListAPI.Domain.Entities;
using ToDoListAPI.Persistence.Contexts;

namespace ToDoListAPI.Persistence.Repositories
{
    public class TodoItemRepository : GenericRepository<TodoItem>, ITodoItemRepository
    {
        public TodoItemRepository(TodoListDbContext context) : base(context)
        {
        }
    }
}

