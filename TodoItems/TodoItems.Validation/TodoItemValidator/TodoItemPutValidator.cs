using FluentValidation;
using System.Threading;
using TodoItems.Models.DTO;
using TodoItems.Service.Validation;

namespace TodoItems.Validation
{
    public class TodoItemPutValidator : AbstractValidator<TodoItemDTO>
    {
        private readonly ITodoItemValidatorService _service;

        public TodoItemPutValidator(ITodoItemValidatorService service)
        {
            _service = service;

            RuleFor(m => m)
            .NotEmpty()
            .WithMessage("Data is empty");

            RuleFor(m => m)
                .MustAsync(async (TodoItemDTO model, CancellationToken cancellationToken) => await _service.VerifyIfExistsAsync(model.Id, model.TodoListId, cancellationToken))
                .WithMessage("Record not found to be modified");

            RuleFor(m => m.Name)
                .NotEmpty()
                .WithMessage("Invalid Name");
        }
    }
}