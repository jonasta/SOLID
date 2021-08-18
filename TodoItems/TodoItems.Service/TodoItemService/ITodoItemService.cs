using System.Collections.Generic;
using System.Threading.Tasks;
using TodoItems.Models.DTO;
using TodoItems.Models.Entities;

namespace TodoItems.Service.TodoItemService
{
    public interface ITodoItemService
    {
        Task<List<TodoItemDTO>> GetTodoItemsAsync();

        Task<List<TodoItemDTO>> GetTodoItemsByListIdAsync(long listId);

        Task<TodoItemDTO> GetTodoItemByListIdAsync(long listId, long id);

        Task<int> UpdateAsync(long id, TodoItemDTO todoItemPutDTO);

        Task<int> InsertAsync(TodoItemPostDTO todoItemPostDTO);

        Task<int> DeleteAsync(long id);
    }
}