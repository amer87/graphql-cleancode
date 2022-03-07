using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Com.Application.Common.Guards;
using Com.Application.Common.Interfaces;
using Com.Domain.Entities;

namespace Com.Application.Faculties.Commands
{
    public class DeleteShopCommand : IRequest
    {
        public Guid Id { get; set; }
    }

    public class DeleteFacultyCommandHandler : IRequestHandler<DeleteShopCommand>
    {
        private readonly IRepository<Shop> _repository;
        public DeleteFacultyCommandHandler(IRepository<Shop> repo)
        {
            _repository = repo;
        }

        public async Task<Unit> Handle(DeleteShopCommand request, CancellationToken cancellationToken)
        {
            var entity =  await _repository
                .GetAsync(request.Id);

            Guards.IsNotNullEntity(entity, "Faculty");

            await _repository.DeleteAsync(entity, cancellationToken);

            return Unit.Value;
        }
    }
}
