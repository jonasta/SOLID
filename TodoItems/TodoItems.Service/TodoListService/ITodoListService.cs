using System.Collections.Generic;
using System.Threading.Tasks;
using TodoItems.Models.DTO;
using TodoItems.Models.Entities;

namespace TodoItems.Service.TodoListService
{
    public interface ITodoListService
    {
        Task<List<TodoList>> GetTodoListsAsync();

        Task<TodoList> GetTodoListAsync(long id);

        Task<int> UpdateAsync(long id, TodoListPutDTO TodoListPutDTO);

        Task<long> Insert(TodoListPostDTO TodoListPostDTO);

        Task<int> Delete(TodoList TodoList);
    }
}