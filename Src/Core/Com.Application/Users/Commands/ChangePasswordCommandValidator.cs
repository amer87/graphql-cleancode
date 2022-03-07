using Com.Domain.Constants;
using FluentValidation;
using FluentValidation.Validators;
using System.Linq;

namespace Com.Application.Users.Commands
{
    public class ChangePasswordCommandValidator : AbstractValidator<ChangePasswordCommand>
    {
        public ChangePasswordCommandValidator()
        {
            RuleFor(c => c.Id).NotEmpty();
            RuleFor(x => x.Password).NotEmpty().WithMessage("Password is required.");
            RuleFor(x => x.NewPassword).NotEmpty().WithMessage("New Password is required.");
            RuleFor(x => x.ConfrimPassword).NotEmpty().WithMessage("Confirm password is required.");
            RuleFor(x => x).Custom((x, context) =>
            {
                if (x.NewPassword != x.ConfrimPassword)
                {
                    context.AddFailure(nameof(x.NewPassword), "Passwords should match");
                }
            });
        }
    }
}
