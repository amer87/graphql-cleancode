using HotChocolate.Types;
using MediatR;
using Com.Application.Shops.Queries;
using Com.Domain.Entities;
using Com.Web.Api.GraphQL.Resolvers;

namespace Com.Web.Api.GraphQL.Types;

public class ShopType : ObjectType<Shop>
{
    readonly IMediator _mediator;

    public ShopType(IMediator mediator) => _mediator = mediator;

    protected override void Configure(IObjectTypeDescriptor<Shop> descriptor)
    {
        descriptor
            .ImplementsNode()
            .IdField(t => t.Id)
            .ResolveNode((ctx, id) => _mediator.Send(new ShopDetailsQuery { Id = id }));

        descriptor
             .Field(t => t.Owner)
             .ResolveWith<OwnerResolvers>(t => t.GetOwnerAsync(default!, default!, default));

        descriptor
            .Field(t => t.Items)
            .ResolveWith<ItemResolvers>(t => t.GetItemsAsync(default!, default!, default));

        descriptor
            .Field(t => t.Files)
            .ResolveWith<FileResolvers>(t => t.GetFileAsync(default!, default!, default));

        descriptor
            .Field(t => t.IsApproved)
            .Ignore();

        descriptor
            .Field(t => t.IsDeleted)
            .Ignore();

        descriptor
            .Field(t => t.OwnerId)
            .Ignore();
    }
}