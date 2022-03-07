using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Com.Application.Common.Interfaces;
using Com.Domain.Entities;

namespace Com.Application.Users.Queries;

class UsersListQueryHandler : IRequestHandler<UsersListQuery, IQueryable<User>>
{
    private readonly IRepository<User> _repository;

    public UsersListQueryHandler(IRepository<User> repository)
    {
        _repository = repository;
    }

    public async Task<IQueryable<User>> Handle(UsersListQuery request, CancellationToken cancellationToken)
    {
        var qb = _repository.GetAll();

        if (!string.IsNullOrEmpty(request.FirstName))
            qb = qb.Where(u => u.FirstName.Contains(request.FirstName));
        if (!string.IsNullOrEmpty(request.LastName))
            qb = qb.Where(u => u.LastName.Contains(request.LastName));
        if (!string.IsNullOrEmpty(request.Email))
            qb = qb.Where(u => u.Email.Contains(request.Email));
        if (request.IsApproved.HasValue)
            qb = qb.Where(u => u.IsApproved == request.IsApproved.Value);
        if (!string.IsNullOrEmpty(request.Role))
            qb = qb.Where(u => u.Role.Equals(request.Role));

        return qb.OrderByDescending(u => u.AddedDate);
    }
}

