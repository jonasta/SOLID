using System.Threading;
using System.Threading.Tasks;

namespace TodoItems.Validation.Service
{
    public interface IBaseValidatorService<TEntity> where TEntity : class
    {
        Task<bool> VerifyIfExists(long id, CancellationToken cancellationToken);
    }
}