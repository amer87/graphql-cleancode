using FluentValidation;

namespace Com.Application.Addresses.Commands;

public class UpsertAddressCommandValidator : AbstractValidator<UpsertAddressCommand>
{
    public UpsertAddressCommandValidator()
    {
        RuleFor(m => m.BelongsTo).NotEmpty();
        RuleFor(m => m.Type).IsInEnum().NotEmpty();
    }
}
