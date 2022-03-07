using System.Linq;
using HotChocolate.Types;
using MediatR;
using Com.Application.Items.Queries;
using Com.Domain.Entities;
using Com.Web.Api.GraphQL.Resolvers;
using static Com.Domain.Constants.Constants;

namespace Com.Web.Api.GraphQL.Types;

public class ItemType : ObjectType<Item>
{
    readonly IMediator _mediator;

    public ItemType(IMediator mediator) => _mediator = mediator;

    protected override void Configure(IObjectTypeDescriptor<Item> descriptor)
    {
        descriptor
            .ImplementsNode()
            .IdField(t => t.Id)
            .ResolveNode(async (ctx, id) => {
                var itemLines = await _mediator.Send(new ItemDetailsQuery { Id = id });
                return itemLines
                    .Where(i => i.Id.Equals(id))
                    .Where(i => i.LineNumber == Entry.HEADERLINE)
                    .FirstOrDefault();
            });

        descriptor
            .Field(t => t.Shop)
            .ResolveWith<ShopResolvers>(t => t.GetShopAsync(default!, default!, default));

        descriptor
            .Field(t => t.Files)
            .ResolveWith<FileResolvers>(t => t.GetFileAsync(default!, default!, default));

        descriptor
            .Field(t => t.IsDeleted)
            .Ignore();

        descriptor
            .Field(t => t.UserId)
            .Ignore();

        descriptor
            .Field(t => t.ShopId)
            .Ignore();
    }
}