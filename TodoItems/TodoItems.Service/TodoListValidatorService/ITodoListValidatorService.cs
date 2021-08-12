using TodoItems.Models.Entities;
using TodoItems.Validation.Service;

namespace TodoItems.Service.TodoListValidatorService
{
    public interface ITodoListValidatorService : IBaseValidatorService<TodoList>
    {
    }
}