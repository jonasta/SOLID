using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using TodoItems.Context.Context;
using TodoItems.Models.DTO;
using TodoItems.Models.Entities;

namespace TodoItems.Service.TodoItemService
{
    public class TodoItemService : ITodoItemService
    {
        private readonly TodoContext _context;

        public TodoItemService(TodoContext context)
        {
            _context = context;
        }

        public async Task<List<TodoItem>> GetTodoItemsAsync()
        {
            return await _context.TodoItems.ToListAsync();
        }

        public async Task<TodoItem> GetTodoItemAsync(long id)
        {
            return await _context.TodoItems.FindAsync(id);
        }

        public async Task<int> UpdateAsync(TodoItem todoItem, TodoItemPutDTO todoItemPutDTO)
        {
            todoItem.Name = todoItemPutDTO.Name;
            todoItem.IsComplete = todoItemPutDTO.IsComplete;
            return await _context.SaveChangesAsync();
        }

        public async Task<int> Insert(TodoItemPostDTO todoItemPostDTO)
        {
            var todoItem = new TodoItem { Name = todoItemPostDTO.Name, IsComplete = todoItemPostDTO.IsComplete };
            _context.TodoItems.Add(todoItem);
            return await _context.SaveChangesAsync();
        }

        public async Task<int> Delete(TodoItem todoItem)
        {
            _context.TodoItems.Remove(todoItem);
            return await _context.SaveChangesAsync();
        }
    }
}