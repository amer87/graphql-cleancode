using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.Data.SqlClient;
using MediatR;
using Com.Application.Common.Interfaces;
using Com.Domain.Entities;
using Com.Application.Common.Enums;

namespace Com.Application.Items.Queries;

public class ItemsListQuery : IRequest<IQueryable<Item>>
{
    public string Name { get; set; }
    public string Code { get; set; }
    public Guid? ShopId { get; set; }
    public Guid? CategoryId { get; set; }
    public ItemTypes Type { get; set; } = ItemTypes.PRODUCT;

    public class ItemsListQueryHandler : IRequestHandler<ItemsListQuery, IQueryable<Item>>
    {
        private readonly IRepository<Item> _repo;

        public ItemsListQueryHandler(IRepository<Item> repo)
        {
            _repo = repo;
        }

        public async Task<IQueryable<Item>> Handle(ItemsListQuery request, CancellationToken cancellationToken)
        {

            var sql = "SELECT i.* FROM Items i " +
                "WHERE i.Id != @EmptyGuid ";

            var parameters = new List<SqlParameter>() { new SqlParameter("@EmptyGuid", Guid.Empty) };
            if (request.ShopId.HasValue)
            {
                sql += "AND i.ShopId = @ShopId ";
                parameters.Add(new SqlParameter("@ShopId", request.ShopId));
            }

            if (!string.IsNullOrEmpty(request.Name))
            {
                sql += "AND i.Name LIKE %@Name% ";
                parameters.Add(new SqlParameter("@Name", request.Name));
            }

            return _repo.Find(sql, parameters.ToArray());
        }
    }
}
