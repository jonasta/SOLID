using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
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

        public async Task<List<TodoListDTO>> GetTodoListsAsync()
        {
            return (await _context.TodoLists.ToListAsync())
                .Select(todoList => new TodoListDTO() { Name = todoList.Name, Id = todoList.Id }).ToList();
        }

        public async Task<TodoListDTO> GetTodoListAsync(long id)
        {
            var todoList = await _context.TodoLists.FindAsync(id);
            return todoList != null ? new TodoListDTO() { Name = todoList.Name, Id = todoList.Id } : null;
        }

        public async Task<int> UpdateAsync(long id, TodoListDTO todoListPutDTO)
        {
            var todoList = await _context.TodoLists.FindAsync(id);
            todoList.Name = todoListPutDTO.Name;
            return await _context.SaveChangesAsync();
        }

        public async Task<TodoList> InsertAsync(TodoListPostDTO todoListPostDTO)
        {
            var todoList = new TodoList { Name = todoListPostDTO.Name };
            _context.TodoLists.Add(todoList);
            await _context.SaveChangesAsync();
            return todoList;
        }

        public async Task<int> DeleteAsync(long id)
        {
            var todoList = await _context.TodoLists.FindAsync(id);
            _context.TodoLists.Remove(todoList);
            return await _context.SaveChangesAsync();
        }
    }
}