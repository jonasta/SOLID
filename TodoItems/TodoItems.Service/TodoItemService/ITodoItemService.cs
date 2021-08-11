using System.Collections.Generic;
using System.Threading.Tasks;
using TodoItems.Models.DTO;
using TodoItems.Models.Entities;

namespace TodoItems.Service.TodoItemService
{
    public interface ITodoItemService
    {
        Task<List<TodoItem>> GetTodoItemsAsync();

        Task<TodoItem> GetTodoItemAsync(long id);

        Task<int> UpdateAsync(TodoItem todoItem, TodoItemPutDTO todoItemPutDTO);

        Task<int> Insert(TodoItemPostDTO todoItemPostDTO);

        Task<int> Delete(TodoItem todoItem);
    }
}