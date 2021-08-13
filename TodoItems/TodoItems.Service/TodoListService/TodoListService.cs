using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using TodoItems.Context.Context;
using TodoItems.Models.DTO;
using TodoItems.Models.Entities;

namespace TodoItems.Service.TodoListService
{
    public class TodoListService : ITodoListService
    {
        private readonly TodoContext _context;

        public TodoListService(TodoContext context)
        {
            _context = context;
        }

        public async Task<List<TodoList>> GetTodoListsAsync()
        {
            return await _context.TodoLists.ToListAsync();
        }

        public async Task<TodoList> GetTodoListAsync(long id)
        {
            return await _context.TodoLists.FindAsync(id);
        }

        public async Task<int> UpdateAsync(long id, TodoListPutDTO todoListPutDTO)
        {
            var todoList = await _context.TodoLists.FindAsync(id);
            todoList.Name = todoListPutDTO.Name;
            return await _context.SaveChangesAsync();
        }

        public async Task<long> Insert(TodoListPostDTO todoListPostDTO)
        {
            var todoList = new TodoList { Name = todoListPostDTO.Name };
            _context.TodoLists.Add(todoList);
            await _context.SaveChangesAsync();
            return todoList.Id;
        }

        public async Task<int> Delete(TodoList todoList)
        {
            _context.TodoLists.Remove(todoList);
            return await _context.SaveChangesAsync();
        }
    }
}