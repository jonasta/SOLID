using FluentValidation;
using System.Threading;
using TodoItems.Models.DTO;
using TodoItems.Service.Validation;

namespace TodoItems.Validation
{
    public class TodoListPutValidator : AbstractValidator<TodoListDTO>
    {
        private readonly ITodoListValidatorService _service;

        public TodoListPutValidator(ITodoListValidatorService service)
        {
            _service = service;

            RuleFor(m => m)
                .NotEmpty()
                .NotNull()
                .WithMessage("Data is empty");

            RuleFor(m => m.Id)
                .MustAsync(async (long id, CancellationToken cancellationToken) => await _service.VerifyIfExistsAsync(id, cancellationToken))
                .WithMessage("Item not found");

            RuleFor(m => m.Name)
                .NotEmpty()
                .WithMessage("Invalid Name");
        }
    }
}