using FluentValidation;

namespace Com.Application.Users.Commands
{
    public class UpsertUserCommandValidator : AbstractValidator<UpsertUserCommand>
    {
        public UpsertUserCommandValidator()
        {
            RuleFor(c => c.UserName).MaximumLength(64);
            RuleFor(c => c.FirstName).NotEmpty();
        }
    }
}
