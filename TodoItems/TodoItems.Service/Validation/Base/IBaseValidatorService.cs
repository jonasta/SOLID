using System.Threading;
using System.Threading.Tasks;

namespace TodoItems.Service.Validation
{
    public interface IBaseValidatorService<TEntity> where TEntity : class
    {
        Task<bool> VerifyIfExistsAsync(long id, CancellationToken cancellationToken);
    }
}