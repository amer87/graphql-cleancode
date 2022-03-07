using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Options;
using Com.Application.Common;
using Com.Application.Common.Abstract;
using Com.Application.Common.Interfaces;
using Com.Domain.Entities;
using static Com.Application.Common.Constants.Constants;
using Com.Application.Common.Constants;

namespace Com.Application.Users.Commands;

public class UpsertUserCommand : IRequest<UserPayload>
{
    public Guid? Id { get; set; }
    public string UserName { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public string Password { get; set; }
    public string Role { get; set; } = Roles.Guest;
    public bool IsApproved { get; set; } = false;

    public class UpsertUserCommandHandler : BaseCommandHandler<User>, IRequestHandler<UpsertUserCommand, UserPayload>
    {
        private readonly IJwtProvider _jwtHelper;
        private readonly AppSettings _appsettings;

        public UpsertUserCommandHandler(IJwtProvider jwtHelper, IOptions<AppSettings> appsettings, IRepository<User> repository) : base(repository)
        {
            _jwtHelper = jwtHelper;
            _appsettings = appsettings.Value;
        }

        public async Task<UserPayload> Handle(UpsertUserCommand request, CancellationToken cancellationToken)
        {
            try
            {
                User entity = await InitAsync(request.Id.GetValueOrDefault(), cancellationToken);

                if (entity.Id.Equals(Guid.Empty))
                {
                    entity.Password = SecurePasswordUtil.Hash(request.Password);
                    entity.UserName = request.UserName;
                    entity.Role = request.Role;
                }

                entity.FirstName = request.FirstName;
                entity.LastName = request.LastName;
                entity.PhoneNumber = request.PhoneNumber;
                entity.Email = request.UserName;
                entity.Token = _jwtHelper.GetToken(entity.Id, entity.FirstName, entity.Role, Guid.Empty, _appsettings.Secret);
                entity.IsApproved = request.IsApproved;
                await HandleUpsertAsync(entity, cancellationToken);
                return new UserPayload(entity);
            }
            catch (Exception ex)
            {

                return new UserPayload(ex.Message, Constants.StringResults.NOK);
            }
        }
    }
}
