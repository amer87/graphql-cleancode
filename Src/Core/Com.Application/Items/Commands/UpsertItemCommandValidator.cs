using FluentValidation;

namespace Com.Application.Items.Commands;

public class UpsertItemCommandValidator : AbstractValidator<UpsertItemCommand>
{
    public UpsertItemCommandValidator()
    {
        RuleFor(c => c.Name).NotEmpty();
        RuleFor(c => c.Code).NotEmpty().MaximumLength(10).WithMessage("Code Exceeded max length");
        RuleFor(c => c.ShopId).NotEmpty();
        RuleFor(c => c.Price).NotEmpty().GreaterThan(0);
        RuleFor(c => c.AvailableQuantity).NotEmpty().GreaterThan(0).LessThan(1000).When(c => c.Type == Common.Enums.ItemTypes.PRODUCT);
    }
}
