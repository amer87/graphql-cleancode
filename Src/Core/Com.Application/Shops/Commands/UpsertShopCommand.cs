using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Com.Application.Common.Guards;
using Com.Application.Common.Interfaces;
using Com.Domain.Entities;
using Com.Domain.Constants;
using Com.Application.Common.Abstract;
using Com.Application.Shops;

namespace Com.Application.Faculties.Commands;

public class UpsertShopCommand : IRequest<ShopPayload>
{
    public Guid? Id { get; set; }
    public string Name { get; set; }
    public string Code { get; set; }
    public string? Description { get; set; }
    public string? PhoneNumber { get; set; }
    public string? Email { get; set; }
    public Guid? UserId { get; set; }

    public class UpsertShopCommandHandler : BaseCommandHandler<Shop>, IRequestHandler<UpsertShopCommand, ShopPayload>
    {
        public UpsertShopCommandHandler(ICurrentUserService currentUserService, IRepository<Shop> repository): base(repository, currentUserService) { }

        public async Task<ShopPayload> Handle(UpsertShopCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var entity = await InitAsync(request.Id.GetValueOrDefault(), cancellationToken);
                entity.Code = request.Code;
                entity.Name = request.Name;
                entity.Description = request.Description;
                entity.PhoneNumber = request.PhoneNumber;
                entity.Email = request.Email;

                ValidateUser(entity);

                return new ShopPayload(entity);
            }
            catch (Exception ex)
            {

                return new ShopPayload(ex.Message, Constants.StringResults.NOK);
            }
        }

        private void ValidateUser(Shop entity)
        {
            if (_currentUserService.Role == Constants.Roles.Admin) return;
            
            Guards.IsTrue(!entity.Id.Equals(Guid.Empty) && _currentUserService.UserId.Equals(entity.OwnerId), "This user is not the shop owner");
        }
    }
}
