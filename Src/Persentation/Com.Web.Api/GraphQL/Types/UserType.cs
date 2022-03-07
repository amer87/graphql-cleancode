using HotChocolate.Types;
using MediatR;
using Com.Domain.Entities;
using Com.Web.Api.GraphQL.Resolvers;
using Com.Application.Users.Queries;

namespace Com.Web.Api.GraphQL.Types;

public class UserType : ObjectType<User>
{
    readonly IMediator _mediator;

    public UserType(IMediator mediator) => _mediator = mediator;

    protected override void Configure(IObjectTypeDescriptor<User> descriptor)
    {
        descriptor
            .ImplementsNode()
            .IdField(t => t.Id)
            .ResolveNode((ctx, id) => _mediator.Send(new UserDetailsQuery { Id = id }));

        descriptor
            .Field(t => t.Items)
            .ResolveWith<ItemResolvers>(t => t.GetItemsAsync(default!, default!, default));

        descriptor
             .Field(t => t.Password)
             .Ignore();

        descriptor
            .Field(t => t.IsApproved)
            .Ignore();

        descriptor
            .Field(t => t.IsDeleted)
            .Ignore();
    }
}