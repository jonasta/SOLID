using System.Threading;
using System.Threading.Tasks;
using TodoItems.Context.Context;
using TodoItems.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace TodoItems.Service.Validation
{
    public class TodoItemValidatorService : BaseValidatorService<TodoItem>, ITodoItemValidatorService
    {
        public TodoItemValidatorService(TodoContext context) : base(context)
        {
        }

        public async Task<bool> VerifyIfExistsAsync(long id, long listId, CancellationToken cancellationToken)

        {
            return (await dbSet.FirstOrDefaultAsync(i => i.Id == id && i.TodoListId == listId)) != null;
        }
    }
}