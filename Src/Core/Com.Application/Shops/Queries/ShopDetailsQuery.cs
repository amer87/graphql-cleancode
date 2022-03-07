using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Com.Application.Common.Interfaces;
using Com.Domain.Entities;

namespace Com.Application.Shops.Queries;

public class ShopDetailsQuery : IRequest<Shop>
{
    public Guid Id { get; set; }

    public class ShopDetailsQueryHandler : IRequestHandler<ShopDetailsQuery, Shop>
    {
        private readonly IRepository<Shop> _repository;

        public ShopDetailsQueryHandler(IRepository<Shop> repository)
        {
            _repository = repository;
        }

        public async Task<Shop> Handle(ShopDetailsQuery request, CancellationToken cancellationToken)
        {
            return await _repository.GetAsync(request.Id);
        }
    }
}