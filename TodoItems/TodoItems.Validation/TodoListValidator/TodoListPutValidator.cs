using FluentValidation;
using System.Threading;
using TodoItems.Models.DTO;
using TodoItems.Service.TodoListValidatorService;

namespace TodoItems.Validation.TodoItemValidator
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