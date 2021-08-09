using FluentValidation;
using TodoItems.Models.DTO;

namespace TodoItems.Validation.TodoItem
{
    public class TodoItemPostValidator : AbstractValidator<TodoItemPostDTO>
    {
        public TodoItemPostValidator()
        {
            RuleFor(m => m)
                .NotEmpty()
                .NotNull()
                .WithMessage("Modelo Inválido");

            RuleFor(m => m.Name)
                .NotEmpty()
                .WithMessage("Nome Inválido");
        }
    }
}