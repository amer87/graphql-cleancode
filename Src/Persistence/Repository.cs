using System;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Com.Application.Common.Interfaces;
using Com.Domain.Entities;
using System.Threading.Tasks;
using System.Threading;

namespace Com.Persistence;
public class Repository<TEntity> : IRepository<TEntity> where TEntity : BaseEntity
{
    private readonly IDbContextFactory<ComDbContext> _contextFactory;

    public Repository(IDbContextFactory<ComDbContext> contextFactory)
    {
        _contextFactory = contextFactory;
    }

    public virtual TEntity Get(Guid id) => _contextFactory.CreateDbContext().Set<TEntity>().Find(id);

    public virtual TEntity Get<TType>(TType id) => _contextFactory.CreateDbContext().Set<TEntity>().Find(id);

    public virtual async Task<TEntity> GetAsync(Guid id, CancellationToken cancellationToken) => await _contextFactory.CreateDbContext().Set<TEntity>().FindAsync(new object[] { id }, cancellationToken);

    public virtual async Task<TEntity> GetAsync<TType>(TType id) => await _contextFactory.CreateDbContext().Set<TEntity>().FindAsync(id);

    public virtual async Task<TEntity> GetWithAsync(Expression<Func<TEntity, bool>> where, params Expression<Func<TEntity, object>>[] includes)
    {
        var query = _contextFactory.CreateDbContext().Set<TEntity>().Where(where);
        return await includes.Aggregate(query, (current, includeProperty) => current.Include(includeProperty)).FirstOrDefaultAsync();
    }

    public virtual IQueryable<TEntity> GetAll() => _contextFactory.CreateDbContext().Set<TEntity>().AsQueryable();

    public virtual IQueryable<TEntity> Find(string sql, object[] parameters) => _contextFactory.CreateDbContext().Set<TEntity>().FromSqlRaw(sql, parameters);

    public virtual IQueryable<TEntity> Find(Expression<Func<TEntity, bool>> predicate) => _contextFactory.CreateDbContext().Set<TEntity>().Where(predicate);

    public virtual async Task<int> AddAsync(TEntity entity, CancellationToken cancellationToken)
    {
        var context = _contextFactory.CreateDbContext();
        await context.Set<TEntity>().AddAsync(entity, cancellationToken);
        return await context.SaveChangesAsync(cancellationToken);
    }
    
    public async Task<int> UpdateAsync(TEntity entity, CancellationToken cancellationToken)
    {
        var context = _contextFactory.CreateDbContext();
        context.Set<TEntity>().Update(entity);
        return await context.SaveChangesAsync(cancellationToken);
    }

    public virtual async Task<int> DeleteAsync(TEntity entity, CancellationToken cancellationToken)
    {
        var context = _contextFactory.CreateDbContext();
        context.Set<TEntity>().Remove(entity);
        return await context.SaveChangesAsync(cancellationToken);
    }

}
