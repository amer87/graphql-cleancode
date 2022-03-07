using System;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using MediatR;
using Com.Application.Common.Interfaces;
using Com.Domain.Entities;

namespace Com.Application.Items.Queries;

public class FilesListQuery : IRequest<IQueryable<CMFile>>
{
    public Guid OwnerId { get; set; }

    public class FilesListQueryHandler : IRequestHandler<FilesListQuery, IQueryable<CMFile>>
    {
        private readonly IRepository<CMFile> _repo;

        public FilesListQueryHandler(IRepository<CMFile> repo)
        {
            _repo = repo;
        }

        public async Task<IQueryable<CMFile>> Handle(FilesListQuery request, CancellationToken cancellationToken)
        {

            return _repo.Find(f => f.OwnerId.Equals(request.OwnerId));
        }
    }
}
