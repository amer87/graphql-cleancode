using FluentValidation;

namespace Com.Application.Items.Queries;

public class ItemsListQueryCommandValidator : AbstractValidator<ItemsListQuery>
{
    public ItemsListQueryCommandValidator()
    {
        RuleFor(c => c.Type).NotEmpty().IsInEnum();
    }
}
