using FluentValidation;
using System.Threading;
using TodoItems.Models.Interfaces;
using TodoItems.Service.Validation;

namespace TodoItems.Validation.GeneralValidator
{
    public class GenericValidator<TEntity> : AbstractValidator<IBaseEntity>
    {
        private readonly IBaseValidatorService<IBaseEntity> _service;

        public GenericValidator(IBaseValidatorService<IBaseEntity> service)
        {
            _service = service;
            RuleFor(e => e.Id)
                .MustAsync(async (long id, CancellationToken cancellationToken) => await _service.VerifyIfExistsAsync(id, cancellationToken));
        }
    }
}