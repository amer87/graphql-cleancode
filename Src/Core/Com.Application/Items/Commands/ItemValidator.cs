using FluentValidation;
using Com.Domain.Entities;
using Com.Application.Common.Enums;

namespace Com.Application.Items.Commands;

public class ItemValidator : AbstractValidator<Item>
{
    public ItemValidator()
    {
        RuleFor(c => c.Name).NotEmpty();
        RuleFor(c => c.Code).NotEmpty().MaximumLength(10).WithMessage("Code Exceeded max length");
        RuleFor(c => c.ShopId).NotEmpty();
        RuleFor(c => c.Price).NotEmpty().GreaterThan(0);
        RuleFor(c => c.AvailableQuantity).NotEmpty().GreaterThan(0).LessThan(1000).When(c => c.Type == (short) ItemTypes.PRODUCT);
    }
}
