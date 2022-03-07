using System;

namespace Com.Application.Common.Interfaces;

public interface ICurrentUserService
{
    Guid UserId { get; }

    bool IsAuthenticated { get; }

    Guid ShopId { get; }

    string Role { get; }
}
