using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;
using TodoItems.Context.Context;

namespace TodoItems.Service.Validation
{
    public class BaseValidatorService<TEntity> : IBaseValidatorService<TEntity> where TEntity : class
    {
        protected readonly DbSet<TEntity> dbSet;

        public BaseValidatorService(TodoContext context)
        {
            dbSet = context.Set<TEntity>();
        }

        public async Task<bool> VerifyIfExistsAsync(long id, CancellationToken cancellationToken)
        {
            return await dbSet.FindAsync(id) != null;
        }
    }
}