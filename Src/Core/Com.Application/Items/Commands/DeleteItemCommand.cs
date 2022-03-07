using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Com.Application.Common.Guards;
using Com.Application.Common.Interfaces;
using Com.Domain.Entities;
using Com.Domain.Constants;
using Com.Application.Common.GraphQL;

namespace Com.Application.Items.Commands;

public class DeleteItemCommand : IRequest<ItemPayload>
{
    public Guid Id { get; set; }

    public class DeleteItemCommandCommandHandler : IRequestHandler<DeleteItemCommand, ItemPayload>
    {
        private readonly ICurrentUserService _currentUserService;
        private readonly IRepository<Item> _repository;

        public DeleteItemCommandCommandHandler(ICurrentUserService currentUserService, IRepository<Item> repository)
        {
            _currentUserService = currentUserService;
            _repository = repository;
        }

        public async Task<ItemPayload> Handle(DeleteItemCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var entry = await _repository.GetAsync(request.Id);

                ValidateItemDeleteAsync(entry);

                await _repository.DeleteAsync(entry, cancellationToken);

                return new ItemPayload(entry);
            }
            catch (Exception ex)
            {
                return new ItemPayload(new UserError(ex.Message, Constants.StringResults.NOK));
            }
        }

        private void ValidateItemDeleteAsync(Item entry)
        {
            Guards.IsNotNullEntity(entry, "Item");

            if (entry.LineNumber == Constants.Entry.HEADERLINE)
            {
                var lines = _repository.Find(x => x.EntryId.Equals(entry.EntryId) && !x.Id.Equals(entry.Id)).ToList();
                Guards.IsTrue(lines.Count == 0, "Multiple Lines");
            }

           Guards.IsTrue(new string[] { Constants.Roles.Admin, Constants.Roles.Superuser }.Contains(_currentUserService.Role), "Current user cannot delete this item");
        }
    }
}
