using Com.Application.Common.Exceptions;
using Com.Domain.Entities;
using System;

namespace Com.Application.Common.Guards;

class Guards
{
    internal static void IsAuthorized(User user, string message)
    {
        if (user == null) throw new UnauthorizedAccessException(message);
    }

    public static void IsNotZero(int argumentValue, string argumentName)
    {
        if (argumentValue == 0)
            throw new ArgumentException($"'{argumentName}' cannot be zero");
    }

    public static void IsNotZero(double argumentValue, string argumentName)
    {
        if (argumentValue == 0)
            throw new ArgumentException($"'{argumentName}' cannot be zero");
    }

    public static void AreEqual(double value1, double value2, string message)
    {
        if (value1 != value2)
            throw new ArgumentException(message);
    }

    public static void AreEqual(Guid value1, Guid value2, string message)
    {
        IsTrue(value1.Equals(value2), message);
    }

    public static void IsLessThan(double maxValue, double argumentValue, string argumentName)
    {
        if (maxValue < argumentValue)
            throw new ArgumentException($"'{argumentName}' cannot exceed '{maxValue}'");
    }
    
    public static void IsNotNullEntity(BaseEntity baseEntity, string entityName)
    {
        IsNotNullEntity(baseEntity, entityName, "is empty or not found");
    }

    public static void IsNotNullEntity(BaseEntity baseEntity, string entityName, string message)
    {
        if (baseEntity == null)
            throw new NotFoundException($"{entityName}: {message}");
    }

    public static void IsTrue(bool value, string message)
    {
        if (!value)
            throw new ArgumentException(message);
    }
}
