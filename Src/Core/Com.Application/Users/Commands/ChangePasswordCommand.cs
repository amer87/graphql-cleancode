using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Com.Application.Common;
using Com.Application.Common.Guards;
using Com.Application.Common.Interfaces;
using Com.Domain.Entities;
using Com.Application.Common.Constants;

namespace Com.Application.Users.Commands;

public class ChangePasswordCommand : IRequest<UserPayload>
{
    public Guid Id { get; set; }
    public string Password { get; set; }
    public string NewPassword { get; set; }
    public string ConfrimPassword { get; set; }

    public class ChangePasswordCommandHandler : IRequestHandler<ChangePasswordCommand, UserPayload>
    {
        private readonly ICurrentUserService _currentUserService;
        private readonly IRepository<User> _repository;

        public ChangePasswordCommandHandler(ICurrentUserService currentUserService, IRepository<User> repository)
        {
            _currentUserService = currentUserService;
            _repository = repository;
        }

        public async Task<UserPayload> Handle(ChangePasswordCommand request, CancellationToken cancellationToken)
        {
            Guards.IsTrue(_currentUserService.UserId.Equals(request.Id), "User logged in can change password");
            Guards.IsTrue(request.NewPassword.Equals(request.ConfrimPassword), "Confrim password does not match");
            var user = await _repository.GetAsync(request.Id);

            Guards.IsTrue(SecurePasswordUtil.Verify(request.Password, user.Password), "Password is wrong");

            user.Password = SecurePasswordUtil.Hash(request.NewPassword);
            try
            {
                await _repository.UpdateAsync(user, cancellationToken);
                return new UserPayload(user);
            }catch (Exception ex)
            {
                return new UserPayload(ex.Message, Constants.StringResults.NOK);
            }
        }
    }
}
