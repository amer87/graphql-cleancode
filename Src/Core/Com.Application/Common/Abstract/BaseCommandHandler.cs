using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Com.Application.Common.Interfaces;
using Com.Domain.Entities;

namespace Com.Application.Common.Abstract;

public abstract class BaseCommandHandler<T> where T : BaseEntity
{
    internal readonly ICurrentUserService _currentUserService;
    internal readonly IRepository<T> _repo;

    public BaseCommandHandler(IRepository<T> repository, ICurrentUserService currentUser)
    {
        _currentUserService = currentUser;
        _repo = repository;
    }

    public BaseCommandHandler(IRepository<T> repository): this(repository, null) { }

    public virtual async Task<T> InitAsync(Guid id, CancellationToken cancellationToken)
    {
        if (id.Equals(Guid.Empty)) return (T)Activator.CreateInstance(typeof(T));
        return await _repo.GetAsync(id, cancellationToken);
    }

    public virtual async Task<IQueryable<T>> InitAsync(Expression<Func<T, bool>> where)
    {
        return _repo.Find(where);
    }

    public virtual async Task<int> HandleUpsertAsync(T entity, CancellationToken cancellationToken)
    {
        var currentUserId = _currentUserService is not null ? _currentUserService.UserId : Guid.Empty;

        if (entity.Id.Equals(Guid.Empty))
        {
            entity.Id = Guid.NewGuid();
            entity.Creator = currentUserId;
            return await _repo.AddAsync(entity, cancellationToken);
        }
        else {
            entity.Modifier = currentUserId;            
            return await _repo.UpdateAsync(entity, cancellationToken);
        }
    }

    public virtual async Task<int> HandleDeleteAsync(T entity, CancellationToken cancellationToken)
    {
        return await _repo.DeleteAsync(entity, cancellationToken);
    }
}
