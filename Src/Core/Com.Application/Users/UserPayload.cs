using Com.Application.Common.GraphQL;
using Com.Domain.Entities;

namespace Com.Application.Users;

public class UserPayload : Payload<User>
{
    public UserPayload(User s) : base(s)  { }

    public UserPayload(UserError error) : base(new[] { error }) {}

    public UserPayload(string message, string code): base(message, code) { }
}
