using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Com.Application.Common.Interfaces;
using Com.Domain.Entities;

namespace Com.Application.Users.Queries;

public record UserDetailsQuery : IRequest<User>
{
    public Guid Id { get; set; }
}

public class UserDetailsQueryHandler : IRequestHandler<UserDetailsQuery, User>
{
    private readonly IRepository<User> _repository;

    public UserDetailsQueryHandler(IRepository<User> repository)
    {
        _repository = repository;
    }

    public async Task<User> Handle(UserDetailsQuery request, CancellationToken cancellationToken)
    {
        return await _repository.GetAsync(request.Id);
    }
}
