using FluentValidation;

namespace Com.Application.Faculties.Commands
{
    public class UpdateShopCommandValidator : AbstractValidator<UpsertShopCommand>
    {
        public UpdateShopCommandValidator()
        {
            RuleFor(c => c.Name).MaximumLength(100).WithMessage("Name Exceeded max length").NotEmpty();
            RuleFor(c => c.Email).MaximumLength(150).WithMessage("Email Exceeded max length");
            RuleFor(c => c.Description).MaximumLength(200).WithMessage("Description Exceeded max length");
            RuleFor(c => c.PhoneNumber).MaximumLength(25).WithMessage("Phone Exceeded max length");
        }
    }
}
