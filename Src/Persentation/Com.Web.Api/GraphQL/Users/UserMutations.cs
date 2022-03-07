using System.Threading.Tasks;
using System.Threading;
using HotChocolate;
using HotChocolate.Types;
using MediatR;
using Com.Application.Users.Commands;
using Com.Application.Users;

namespace Com.Web.Api.GraphQL.Users;

[ExtendObjectType(OperationTypeNames.Mutation)]
public class UserMutations
{
    public async Task<UserPayload> AuthenticateAsync(
        AuthenticationCommand input,
        [Service] IMediator mediator,
        CancellationToken cancellationToken) => await mediator.Send(input, cancellationToken);

    public async Task<UserPayload> UpsertUserAsync(
        UpsertUserCommand input,
        [Service] IMediator mediator,
        CancellationToken cancellationToken) => await mediator.Send(input, cancellationToken);

    public async Task<UserPayload> ChangePasswordAsync(
        ChangePasswordCommand input,
        [Service] IMediator mediator,
        CancellationToken cancellationToken) => await mediator.Send(input, cancellationToken);
}
