using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using MediatR;
using Com.Application.Common.Interfaces;
using Com.Domain.Entities;

namespace Com.Application.Shops.Queries;

public record class ShopsListQuery : IRequest<IQueryable<Shop>>
{
    public string? Name { get; set; }
    public Guid? OwnerId { get; set; }
}

public class ShopsListQueryHandler : IRequestHandler<ShopsListQuery, IQueryable<Shop>>
{
    private readonly IRepository<Shop> _repository;

    public ShopsListQueryHandler(IRepository<Shop> repository)
    {
        _repository = repository;
    }

    public async Task<IQueryable<Shop>> Handle(ShopsListQuery request, CancellationToken cancellationToken)
    {
        var qb = "SELECT s.* FROM Shops s WHERE s.Id <> '00000000-0000-0000-0000-000000000000' ";
        var parameters = new List<SqlParameter>() {};


        if (!string.IsNullOrEmpty(request.Name))
        {
            qb += "AND s.name LIKE %@Name% ";
            parameters.Add(new SqlParameter("@Name", request.Name));
        }

        if (request.OwnerId.HasValue && request.OwnerId != Guid.Empty)
        {
            qb += "AND s.ownerId = @OwnerId ";
            parameters.Add(new SqlParameter("@OwnerId", request.OwnerId));
        }

        return _repository.Find(qb, parameters.ToArray());
    }

}
