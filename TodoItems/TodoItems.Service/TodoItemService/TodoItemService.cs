using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoItems.Context.Context;
using TodoItems.Models.DTO;
using TodoItems.Models.Entities;

namespace TodoItems.Service.TodoItemService
{
    public class TodoItemService : ITodoItemService
    {
        private readonly TodoContext _context;
        private readonly IMapper _mapper;

        public TodoItemService(TodoContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<TodoItemDTO>> GetTodoItemsAsync()
        {
            var todoItems = await _context.TodoItems.ToListAsync();

            return _mapper.Map<List<TodoItemDTO>>(todoItems);
        }

        public async Task<List<TodoItemDTO>> GetTodoItemsByListIdAsync(long listId)
        {
            var todoItems = await _context.TodoItems
                .Where(i => i.TodoListId == listId).ToListAsync();
            return _mapper.Map<List<TodoItemDTO>>(todoItems);
        }

        public async Task<TodoItemDTO> GetTodoItemByListIdAsync(long listId, long id)
        {
            var todoItem = await _context.TodoItems
                .Where(i => i.TodoListId == listId && i.Id == id)
                .SingleOrDefaultAsync();
            return _mapper.Map<TodoItemDTO>(todoItem);
        }

        public async Task<int> UpdateAsync(long id, TodoItemDTO todoItemDTO)
        {
            var todoItem = await _context.TodoItems.FindAsync(id);
            todoItem = _mapper.Map<TodoItem>(todoItemDTO);
            todoItem.Id = id;
            return await _context.SaveChangesAsync();
        }

        public async Task<int> InsertAsync(TodoItemPostDTO todoItemDTO)
        {
            var todoItem = _mapper.Map<TodoItem>(todoItemDTO);
            _context.TodoItems.Add(todoItem);
            return await _context.SaveChangesAsync();
        }

        public async Task<int> DeleteAsync(long id)
        {
            var todoItem = await _context.TodoItems.FindAsync(id);
            _context.TodoItems.Remove(todoItem);
            return await _context.SaveChangesAsync();
        }
    }
}