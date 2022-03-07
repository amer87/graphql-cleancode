using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace Com.Application.Common.Interfaces;

public interface IRepository<TEntity>
{
    TEntity Get(Guid id);

    TEntity Get<TType>(TType id);

    Task<TEntity> GetAsync(Guid id, CancellationToken cancellationToken);

    Task<TEntity> GetAsync<TType>(TType id);

    Task<TEntity> GetWithAsync(Expression<Func<TEntity, bool>> where, params Expression<Func<TEntity, object>>[] includes);

    IQueryable<TEntity> GetAll();

    IQueryable<TEntity> Find(Expression<Func<TEntity, bool>> where);

    IQueryable<TEntity> Find(string sql, object[] parameters);

    Task<int> AddAsync(TEntity entity, CancellationToken cancellationToken);

    Task<int> UpdateAsync(TEntity entity, CancellationToken cancellationToken);

    Task<int> DeleteAsync(TEntity entity, CancellationToken cancellationToken);
}
