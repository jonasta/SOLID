using System.Collections.Generic;
using System.Threading.Tasks;
using TodoItems.Models.DTO;
using TodoItems.Models.Entities;

namespace TodoItems.Service.TodoListService
{
    public interface ITodoListService
    {
        Task<List<TodoListDTO>> GetTodoListsAsync();

        Task<TodoListDTO> GetTodoListAsync(long id);

        Task<int> UpdateAsync(long id, TodoListDTO todoListPutDTO);

        Task<long> Insert(TodoListPostDTO todoListPostDTO);

        Task<int> Delete(long id);
    }
}