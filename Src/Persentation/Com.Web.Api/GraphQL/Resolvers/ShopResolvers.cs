using System.Threading;
using System.Threading.Tasks;
using HotChocolate;
using MediatR;
using Com.Application.Shops.Queries;
using Com.Domain.Entities;

namespace Com.Web.Api.GraphQL.Resolvers;

internal class ShopResolvers
{
    public async Task<Shop> GetShopAsync(
           [Parent] Item item,
           [Service] IMediator _mediator,
           CancellationToken cancellationToken) => await _mediator.Send(new ShopDetailsQuery { Id = item.ShopId }, cancellationToken);
}