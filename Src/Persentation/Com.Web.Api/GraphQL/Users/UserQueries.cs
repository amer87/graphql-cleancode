using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using HotChocolate;
using HotChocolate.AspNetCore.Authorization;
using HotChocolate.Types;
using MediatR;
using Com.Domain.Entities;
using Com.Application.Users.Queries;

namespace Com.Web.Api.GraphQL.Users;

[ExtendObjectType("Query")]
public class UserQueries
{
    [Authorize]
    public async Task<IQueryable<User>> GetUsers(
        [Service] IMediator mediator,
        UsersListQuery? usersListQuery,
        CancellationToken cancellationToken) => await mediator.Send(usersListQuery ?? new UsersListQuery(), cancellationToken);

    public async Task<User> GetUser(
        [Service] IMediator mediator,
        UserDetailsQuery userDetailsQuery,
        CancellationToken cancellationToken) => await mediator.Send(userDetailsQuery, cancellationToken);
}
