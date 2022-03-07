using FluentValidation;

namespace Com.Application.Faculties.Commands
{
    public class DeleteShopCommandValidator : AbstractValidator<DeleteShopCommand>
    {
        public DeleteShopCommandValidator()
        {
            RuleFor(m => m.Id).NotEmpty();
        }
    }
}
