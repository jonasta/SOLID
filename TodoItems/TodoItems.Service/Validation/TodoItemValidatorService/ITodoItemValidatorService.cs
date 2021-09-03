using System.Threading;
using System.Threading.Tasks;
using TodoItems.Models.Entities;

namespace TodoItems.Service.Validation
{
    public interface ITodoItemValidatorService : IBaseValidatorService<TodoItem>
    {
        Task<bool> VerifyIfExistsAsync(long id, long listId, CancellationToken cancellationToken);
    }
}