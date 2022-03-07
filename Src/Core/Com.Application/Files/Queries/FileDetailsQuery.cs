using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Com.Application.Common.Interfaces;
using Com.Domain.Entities;

namespace Com.Application.Files.Queries;

public class FileDetailsQuery : IRequest<CMFile>
{
    public Guid Id { get; set; }

    public class FileDetailsQueryHandler : IRequestHandler<FileDetailsQuery, CMFile>
    {
        private readonly IRepository<CMFile> _repository;

        public FileDetailsQueryHandler(IRepository<CMFile> repository)
        {
            _repository = repository;
        }

        public async Task<CMFile> Handle(FileDetailsQuery request, CancellationToken cancellationToken)
        {
            return await _repository.GetAsync(request.Id);
        }
    }
}