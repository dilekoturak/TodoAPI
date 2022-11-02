using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ToDoListAPI.Application;
using ToDoListAPI.Application.Models;
using ToDoListAPI.Application.Repositories;
using ToDoListAPI.Domain.Entities;

namespace ToDoListAPI.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoListController : ControllerBase
    {
        private readonly ITodoItemRepository _todoItemRepository;

        public TodoListController(ITodoItemRepository todoItemRepository)
        {
            _todoItemRepository = todoItemRepository;
        }

        [HttpGet("Pending")]
        public async Task<IActionResult> GetPendingItems([FromQuery]Pagination pagination)
        {
            var pendingTotalCount = _todoItemRepository.GetAll().Where(t => (t.DueDate == null || t.DueDate > DateTime.UtcNow) && t.Completed == false).Count();
            var pendingItems = _todoItemRepository.GetAll()
                .Select(t => new
                {
                    t.Id,
                    t.Title,
                    t.DueDate,
                    t.Completed
                }).Where(t => (t.DueDate == null || t.DueDate > DateTime.UtcNow) && t.Completed == false)
                .Skip(pagination.Page * pagination.Size)
                .Take(pagination.Size);

            return Ok(new { pendingTotalCount, pendingItems });
        }

        [HttpGet("Overdue")]
        public async Task<IActionResult> GetOverdueItems([FromQuery] Pagination pagination)
        {
            var overdueTotalCount = _todoItemRepository.GetAll().Where(t => (t.DueDate != null && t.DueDate < DateTime.UtcNow) && t.Completed == false).Count();
            var overdueItems = _todoItemRepository.GetAll()
                .Select(t => new
                {
                    t.Id,
                    t.Title,
                    t.DueDate,
                    t.Completed
                }).Where(t => (t.DueDate != null && t.DueDate < DateTime.UtcNow) && t.Completed == false)
                .Skip(pagination.Page * pagination.Size)
                .Take(pagination.Size);

            return Ok(new { overdueTotalCount, overdueItems });
        }

        [HttpPost]
        public async Task<IActionResult> Post(TodoItem todoItem)
        {
            try
            {
                if (todoItem == null)
                {
                    return BadRequest();
                }

                TodoItem item = _todoItemRepository.GetWhere(t => t.Title == todoItem.Title).FirstOrDefault();

                if (item == null)
                {
                    await _todoItemRepository.Add(new()
                    {
                        Id = Guid.NewGuid(),
                        Title = todoItem.Title,
                        DueDate = todoItem.DueDate
                    });
                    await _todoItemRepository.SaveAsync();
                    return StatusCode((int)HttpStatusCode.Created);
                }
            }
            catch (Exception)
            {
                throw;
            }
            return StatusCode(409, $"Title: '{todoItem.Title}' already exists.");
        }

        [HttpPut]
        public async Task<IActionResult> Put(TodoItemModel todoItem)
        {
            if (todoItem == null)
            {
                return BadRequest();
            }

            TodoItem item = await _todoItemRepository.GetByIdAsync(todoItem.Id);

            if (item == null)
            {
                return NotFound();
            }

            item.Title = todoItem.Title;
            item.DueDate = todoItem.DueDate;
            item.Completed = todoItem.Completed;
            await _todoItemRepository.SaveAsync();

            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            await _todoItemRepository.Remove(id);
            await _todoItemRepository.SaveAsync();

            return Ok();
        }
    }
}

