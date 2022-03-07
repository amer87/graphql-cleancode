using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using MediatR;
using Com.Application.Common.Interfaces;
using Com.Domain.Entities;
using Com.Application.Common.Enums;

namespace Com.Application.Addresses.Queries;

public class AddressDetailsQuery : IRequest<Address>
{
    public Guid BelongTo { get; set; }
    public AddressTypes Type { get; set; }

    public class AddressDetailsQueryQueryHandler : IRequestHandler<AddressDetailsQuery, Address>
    {
        private readonly IRepository<Address> _repository;

        public AddressDetailsQueryQueryHandler(IMapper mapper, IRepository<Address> repository)
        {
            _repository = repository;
        }

        public async Task<Address> Handle(AddressDetailsQuery request, CancellationToken cancellationToken)
        {
            return await _repository.Find(a => a.BelongsTo == request.BelongTo && a.Type == (short) request.Type).AsQueryable()
                .SingleOrDefaultAsync(cancellationToken);
        }
    }
}

