using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Options;
using Com.Application.Common;
using Com.Application.Common.Guards;
using Com.Application.Common.Interfaces;
using Com.Domain.Entities;
using Com.Application.Common.Abstract;

namespace Com.Application.Users.Commands;

public class AuthenticationCommand : IRequest<UserPayload>
{
    public string UserName { get; set; }
    public string Password { get; set; }

    public class AuthenticationCommandHandler : BaseCommandHandler<User>, IRequestHandler<AuthenticationCommand, UserPayload>
    {
        private readonly IJwtProvider _jwtHelper;
        private readonly AppSettings _appSettings;

        public AuthenticationCommandHandler(IJwtProvider jwtHelper, IOptions<AppSettings> settings, IRepository<User> repository) : base(repository)
        {
            _jwtHelper = jwtHelper;
            _appSettings = settings.Value;
        }

        public async Task<UserPayload> Handle(AuthenticationCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var user = (await InitAsync(x => x.UserName.Equals(request.UserName))).FirstOrDefault();
                Guards.IsAuthorized(user, "User is not found");

                var hashedp = SecurePasswordUtil.Hash(request.Password);
                user = SecurePasswordUtil.Verify(request.Password, user.Password) ? user : null;
                Guards.IsAuthorized(user, "Password is wrong");

                user.Token = _jwtHelper.GetToken(user.Id, user.FirstName, user.Role, System.Guid.Empty, _appSettings.Secret);
                await HandleUpsertAsync(user, cancellationToken);

                return new UserPayload(user);
            }
            catch (System.Exception ex)
            {
                return new UserPayload(ex.Message, "NOT_AUTH");
            }
        }

    }
}
