using FluentValidation;
using TodoItems.Models.DTO;

namespace TodoItems.Validation
{
    public class TodoListPostValidator : AbstractValidator<TodoListPostDTO>
    {
        public TodoListPostValidator()
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