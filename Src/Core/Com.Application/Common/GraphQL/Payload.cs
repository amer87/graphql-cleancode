using Com.Domain.Entities;
using System.Collections.Generic;

namespace Com.Application.Common.GraphQL;

public abstract class Payload<T> where T : BaseEntity
{ 
    public T? Entity { get; }

    public IReadOnlyList<UserError>? Errors { get; }

    public Payload(IReadOnlyList<UserError>? errors = null)
    {
        Errors = errors;
    }

    public Payload(string message, string code) : this(new[] { new UserError(message, code) }) { }

    public Payload(T entity)
    {
        Entity = entity;
    }

}