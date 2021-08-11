using TodoItems.Context.Context;
using TodoItems.Models.Entities;
using TodoItems.Validation.Service;
using TodoItems.Validation.Service.TodoItemValidator;

namespace TodoItems.Validation.TodoItemValidator
{
    public class TodoItemValidatorService : BaseValidatorService<TodoItem>, ITodoItemValidatorService
    {
        public TodoItemValidatorService(TodoContext context) : base(context)
        {
        }
    }
}