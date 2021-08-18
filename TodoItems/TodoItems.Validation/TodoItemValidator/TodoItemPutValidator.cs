using FluentValidation;
using System.Threading;
using TodoItems.Models.DTO;
using TodoItems.Service.TodoItemValidatorService;

namespace TodoItems.Validation.TodoItemValidator
{
    public class TodoItemPutValidator : AbstractValidator<TodoItemDTO>
    {
        private readonly ITodoItemValidatorService _service;

        public TodoItemPutValidator(ITodoItemValidatorService service)
        {
            _service = service;

            RuleFor(m => m)
                .NotEmpty()
                .NotNull()
                .WithMessage("Dados enviados Inválidos");

            RuleFor(m => m.Id)
                .MustAsync(async (long id, CancellationToken cancellationToken) => await _service.VerifyIfExists(id, cancellationToken))
                .WithMessage("Registro não existe para ser modificado");

            RuleFor(m => m.Name)
                .NotEmpty()
                .WithMessage("Nome Inválido");
        }
    }
}