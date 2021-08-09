using System.Collections.Generic;
using System.Threading.Tasks;
using TodoItems.Models.DTO;
using TodoItems.Models.Entities;

namespace TodoItems.Service.TodoItemService
{
    public interface ITodoItemService
    {
        ValueTask<List<TodoItem>> GetTodoItemsAsync();

        ValueTask<TodoItem> GetTodoItemAsync(long id);

        ValueTask<int> UpdateAsync(TodoItem todoItem, TodoItemPutDTO todoItemPutDTO);

        ValueTask<int> Insert(TodoItemPostDTO todoItemPostDTO);

        ValueTask<int> Delete(TodoItem todoItem);
    }
}