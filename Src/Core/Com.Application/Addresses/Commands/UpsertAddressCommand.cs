using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Com.Application.Common.Abstract;
using Com.Application.Common.Constants;
using Com.Application.Common.Enums;
using Com.Application.Common.Interfaces;
using Com.Domain.Entities;

namespace Com.Application.Addresses.Commands;

public class UpsertAddressCommand : IRequest<AddressPayload>
{
    public Guid Id { get; set; }

    public string Title { get; set; }

    public string AddressLine1 { get; set; }

    public string AddressLine2 { get; set; }

    public string Country { get; set; }

    public string State { get; set; }

    public string City { get; set; }

    public Guid BelongsTo { get; set; }

    public AddressTypes Type { get; set; } = AddressTypes.Shop;

    public class UpsertAddressCommandHandler : BaseCommandHandler<Address>, IRequestHandler<UpsertAddressCommand, AddressPayload>
    {
        public UpsertAddressCommandHandler(IRepository<Address> repository, ICurrentUserService currentUserService) : base(repository, currentUserService) { }

        public async Task<AddressPayload> Handle(UpsertAddressCommand request, CancellationToken cancellationToken)
        {
            try
            {
                Address address = await InitAsync(request.Id, cancellationToken);

                address.Title = request.Title;
                address.AddressLine1 = request.AddressLine1;
                address.AddressLine2 = request.AddressLine2;
                address.Country = request.Country;
                address.State = request.State;
                address.City = request.City;
                return new AddressPayload(address);
            }
            catch (Exception ex)
            {
                return new AddressPayload(ex.Message, Constants.StringResults.NOK);
            }
        }

    }
}

