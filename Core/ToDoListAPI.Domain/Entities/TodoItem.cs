using System;
using System.ComponentModel.DataAnnotations;

namespace ToDoListAPI.Domain.Entities
{
    public class TodoItem
    {
        public Guid Id { get; set; }

        public string Title { get; set; }

        public DateTime? DueDate { get; set; }

        public bool Completed { get; set; } = false;
    }
}

