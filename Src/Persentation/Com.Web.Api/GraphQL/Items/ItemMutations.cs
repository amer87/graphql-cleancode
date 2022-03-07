using System.Threading.Tasks;
using System.Threading;
using HotChocolate;
using HotChocolate.Types;
using MediatR;
using Com.Application.Items.Commands;
using Com.Application.Items;

namespace Com.Web.Api.GraphQL.Items;

[ExtendObjectType(OperationTypeNames.Mutation)]
public class ItemMutations
{
    public async Task<ItemPayload> UpsertItemAsync(
        UpsertItemCommand input,
        [Service] IMediator mediator,
        CancellationToken cancellationToken) => await mediator.Send(input, cancellationToken);

    public async Task<ItemPayload> DeleteItemAsync(
        DeleteItemCommand input,
        [Service] IMediator mediator,
        CancellationToken cancellationToken) => await mediator.Send(input, cancellationToken);
}
