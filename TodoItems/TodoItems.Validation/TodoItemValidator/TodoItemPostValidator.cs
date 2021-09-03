using FluentValidation;
using System.Threading;
using TodoItems.Models.DTO;
using TodoItems.Service.Validation;

namespace TodoItems.Validation
{
    public class TodoItemPostValidator : AbstractValidator<TodoItemPostDTO>
    {
        private readonly ITodoListValidatorService _listService;

        public TodoItemPostValidator(ITodoListValidatorService listService)
        {
            _listService = listService;
            RuleFor(m => m)
                .NotEmpty()
                .NotNull()
                .WithMessage("Data is empty");

            RuleFor(m => m.Name)
                .NotEmpty()
                .WithMessage("Invalid Name");

            RuleFor(m => m.TodoListId)
                    .MustAsync(async (long id, CancellationToken cancellationToken) => await _listService.VerifyIfExistsAsync(id, cancellationToken))
                    .WithMessage("List not found");
        }
    }
}