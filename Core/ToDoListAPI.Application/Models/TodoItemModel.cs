using System;
using System.ComponentModel.DataAnnotations;

namespace ToDoListAPI.Application.Models
{
    public class TodoItemModel
    {
        [Required]
        public string Id { get; set; }

        [Required]
        public string Title { get; set; }

        public DateTime? DueDate { get; set; } = null;

        public bool Completed { get; set; }
    }
}

