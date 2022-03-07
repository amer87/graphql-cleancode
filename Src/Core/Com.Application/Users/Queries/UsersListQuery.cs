using System;
using System.Linq;
using Com.Domain.Entities;
using MediatR;

namespace Com.Application.Users.Queries;

public record UsersListQuery : IRequest<IQueryable<User>>
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public bool? IsApproved { get; set; }
    public DateTime AddedDate { get; set; }
    public string Role { get; set; }
}
