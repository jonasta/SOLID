using TodoItems.Models.Entities;
using TodoItems.Validation.Service;

namespace TodoItems.Service.TodoItemValidatorService
{
    public interface ITodoItemValidatorService : IBaseValidatorService<TodoItem>
    {
    }
}