using System.Threading.Tasks;
using TodoItems.Context.Context;
using TodoItems.Models.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using TodoItems.Models.DTO;
using System;

namespace TodoItems.Service.TodoItemService
{
    public class TodoItemService: ITodoItemService
    {
        private readonly TodoContext _context;

        public TodoItemService(TodoContext context)
        {
            _context = context;
        }

        public async ValueTask<List<TodoItem>> GetTodoItemsAsync()
        {
            return await _context.TodoItems.ToListAsync();
        }
        public async ValueTask<TodoItem> GetTodoItemAsync(long id)
        {
            return await _context.TodoItems.FindAsync(id);
        }

        public async ValueTask<bool> UpdateAsync(TodoItem todoItem, TodoItemDTO todoItemDTO)
        {
            todoItem.Name = todoItemDTO.Name;
            todoItem.IsComplete = todoItemDTO.IsComplete;
            await _context.SaveChangesAsync();
            return true;
        }

        public async ValueTask<bool> Insert(TodoItem todoItem)
        {
            _context.TodoItems.Add(todoItem);
            await _context.SaveChangesAsync();
            return true;
        }

        public async ValueTask<bool> Delete(TodoItem todoItem)
        {
            _context.TodoItems.Remove(todoItem);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
