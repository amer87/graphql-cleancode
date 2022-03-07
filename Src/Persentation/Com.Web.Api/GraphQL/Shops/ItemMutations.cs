using System.Threading.Tasks;
using System.Threading;
using HotChocolate;
using HotChocolate.Types;
using MediatR;
using Com.Application.Items.Commands;
using Com.Application.Items;

namespace Com.Web.Api.GraphQL.Shops;

[ExtendObjectType(OperationTypeNames.Mutation)]
public class ShopMutations
{
    public async Task<ItemPayload> UpsertShopAsync(
        UpsertItemCommand input,
        [Service] IMediator mediator,
        CancellationToken cancellationToken) => await mediator.Send(input, cancellationToken);

    public async Task<ItemPayload> DeleteShopAsync(
        DeleteItemCommand input,
        [Service] IMediator mediator,
        CancellationToken cancellationToken) => await mediator.Send(input, cancellationToken);
}
