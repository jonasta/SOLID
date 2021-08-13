using TodoItems.Context.Context;
using TodoItems.Models.Entities;
using TodoItems.Validation.Service;

namespace TodoItems.Service.TodoListValidatorService
{
    public class TodoListValidatorService : BaseValidatorService<TodoList>, ITodoListValidatorService
    {
        public TodoListValidatorService(TodoContext context) : base(context)
        {
        }
    }
}