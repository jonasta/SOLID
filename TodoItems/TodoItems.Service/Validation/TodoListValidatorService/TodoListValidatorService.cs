using TodoItems.Context.Context;
using TodoItems.Models.Entities;

namespace TodoItems.Service.Validation
{
    public class TodoListValidatorService : BaseValidatorService<TodoList>, ITodoListValidatorService
    {
        public TodoListValidatorService(TodoContext context) : base(context)
        {
        }
    }
}