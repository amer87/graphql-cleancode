using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Com.Application.Common.Interfaces;
using Com.Domain.Entities;

namespace Com.Application.Items.Queries;

public class ItemDetailsQuery : IRequest<IQueryable<Item>>
{
    public Guid EntryId { get; set; }
    public Guid Id { get; set; }

    public class ItemDetailsQueryHandler : IRequestHandler<ItemDetailsQuery, IQueryable<Item>>
    {
        private readonly IRepository<Item> _repository;

        public ItemDetailsQueryHandler(IRepository<Item> repository)
        {
            _repository = repository;
        }

        public async Task<IQueryable<Item>> Handle(ItemDetailsQuery request, CancellationToken cancellationToken)
        {
            var qb = _repository.Find(x => x.Id == request.Id).AsQueryable();

            if (!Guid.Empty.Equals(request.EntryId))
                qb = qb.Where(i => i.EntryId == request.EntryId);

            if (!Guid.Empty.Equals(request.Id))
                qb = qb.Where(i => i.Id == request.Id);

            return qb;
        }
    }
}

