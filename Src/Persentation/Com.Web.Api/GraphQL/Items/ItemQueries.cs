using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using HotChocolate;
using HotChocolate.Types;
using MediatR;
using Com.Domain.Entities;
using Com.Application.Items.Queries;

namespace Com.Web.Api.GraphQL.Items;

[ExtendObjectType("Query")]
public class ItemQueries
{
    public async Task<IQueryable<Item>> GetItems(
        [Service] IMediator mediator,
        ItemsListQuery? itemsListQuery,
        CancellationToken cancellationToken) => await mediator.Send(itemsListQuery ?? new  ItemsListQuery(), cancellationToken);

    public async Task<IQueryable<Item>> GetItem(
        [Service] IMediator mediator,
        ItemDetailsQuery itemDetailsQuery,
        CancellationToken cancellationToken) => await mediator.Send(itemDetailsQuery, cancellationToken);
}
