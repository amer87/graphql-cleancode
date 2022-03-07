using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using HotChocolate;
using HotChocolate.AspNetCore.Authorization;
using HotChocolate.Types;
using MediatR;
using Com.Application.Shops.Queries;
using Com.Domain.Entities;

namespace Com.Web.Api.GraphQL.Shops;

[ExtendObjectType("Query")]
public class ShopQueries
{
    [Authorize]
    public async Task<IQueryable<Shop>> GetShops(
        [Service] IMediator mediator,
        ShopsListQuery? shopsListQuery,
        CancellationToken cancellationToken) => await mediator.Send(shopsListQuery ?? new ShopsListQuery(), cancellationToken);

    public async Task<Shop> GetShop(
        [Service] IMediator mediator,
        ShopDetailsQuery shopDetailsQuery,
        CancellationToken cancellationToken) => await mediator.Send(shopDetailsQuery, cancellationToken);
}
