using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using HotChocolate;
using MediatR;
using Com.Domain.Entities;
using Com.Application.Items.Queries;

namespace Com.Web.Api.GraphQL.Resolvers;

internal class FileResolvers
{
    public async Task<IQueryable<CMFile>> GetFileAsync(
        [Parent] BaseEntity parent,
        [Service] IMediator _mediator,
        CancellationToken cancellationToken) 
    {
            var idProperty = parent.GetType().GetProperty("Id").GetValue(parent, null);
            return await _mediator.Send(new FilesListQuery { OwnerId = new Guid(idProperty.ToString()) }, cancellationToken);
    }
}