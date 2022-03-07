using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using HotChocolate;
using MediatR;
using Com.Application.Items.Queries;
using Com.Domain.Entities;

namespace Com.Web.Api.GraphQL.Resolvers;

internal class ItemResolvers
{
    public async Task<IQueryable<Item>> GetItemsAsync(
           [Parent] Shop shop,
           [Service] IMediator _mediator,
           CancellationToken cancellationToken) => await _mediator.Send(new ItemsListQuery { ShopId = shop.Id }, cancellationToken);
}
