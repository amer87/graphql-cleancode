using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Com.Application.Common.Interfaces;
using Com.Domain.Entities;
using Com.Application.Common.Abstract;
using Com.Application.Common.Constants;

namespace Com.Application.Items.Commands;

public class UpsertItemCommandHandler : BaseCommandHandler<Item>,  IRequestHandler<UpsertItemCommand, ItemPayload>
{
    public UpsertItemCommandHandler(ICurrentUserService currentUserService, IRepository<Item> repository) : base(repository, currentUserService) { }

    public async Task<ItemPayload> Handle(UpsertItemCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var entity = await InitAsync(request.Id.GetValueOrDefault(), cancellationToken);
            entity.Name = request.Name;
            entity.Description = request.Description;
            entity.Price = request.Price;
            entity.Code = request.Code;
            entity.SystemReference = string.IsNullOrEmpty(request.SystemReference) ? " " : request.SystemReference;
            entity.ShopId = request.ShopId;
            entity.Type = (short) request.Type;
            entity.AvailableQuantity = request.AvailableQuantity;
            await HandleUpsertAsync(entity, cancellationToken);
            return new ItemPayload(entity);
        }
        catch (Exception ex)
        {
            return new ItemPayload(ex.Message, Constants.StringResults.NOK);
        }
    }

}
