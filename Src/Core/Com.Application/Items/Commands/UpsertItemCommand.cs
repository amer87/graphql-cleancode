using System;
using Com.Application.Common.Enums;
using MediatR;

namespace Com.Application.Items.Commands;

public record class UpsertItemCommand : IRequest<ItemPayload>
{
    public Guid? Id { get; set; }
    public string Name { get; set; }
    public string? Description { get; set; }
    public double Price { get; set; }
    public string Code { get; set; }
    public Guid ShopId { get; set; }
    public string? SystemReference { get; set; }
    public Guid? UserId { get; set; }
    public ItemTypes Type { get; set; } = ItemTypes.PRODUCT;
    public int AvailableQuantity { get; set; }
    public Guid? EntryId { get; set; }
    public Guid[] VariationIds { get; set; }
}
