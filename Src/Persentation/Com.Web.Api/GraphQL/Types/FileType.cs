using HotChocolate.Types;
using MediatR;
using Com.Domain.Entities;
using Com.Application.Files.Queries;

namespace Com.Web.Api.GraphQL.Types;

public class FileType : ObjectType<CMFile>
{
    readonly IMediator _mediator;

    public FileType(IMediator mediator) => _mediator = mediator;

    protected override void Configure(IObjectTypeDescriptor<CMFile> descriptor)
    {
        descriptor
            .ImplementsNode()
            .IdField(t => t.Id)
            .ResolveNode((ctx, id) => _mediator.Send(new FileDetailsQuery { Id = id }));

        descriptor
            .Field(t => t.IsDeleted)
            .Ignore();
    }
}