using TodoItems.Context.Context;
using TodoItems.Models.Entities;
using TodoItems.Validation.Service;

namespace TodoItems.Service.TodoItemValidatorService
{
    public class TodoItemValidatorService : BaseValidatorService<TodoItem>, ITodoItemValidatorService
    {
        public TodoItemValidatorService(TodoContext context) : base(context)
        {
        }
    }
}