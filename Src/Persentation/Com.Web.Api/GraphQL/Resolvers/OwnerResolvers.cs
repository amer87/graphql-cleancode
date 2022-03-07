using Com.Application.Users.Queries;
using Com.Domain.Entities;
using HotChocolate;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Com.Web.Api.GraphQL.Resolvers;

internal class OwnerResolvers
{
    public async Task<User> GetOwnerAsync(
           [Parent] Shop shop,
           [Service] IMediator _mediator,
           CancellationToken cancellationToken) => await _mediator.Send(new UserDetailsQuery { Id = shop.OwnerId.GetValueOrDefault() }, cancellationToken);
}
