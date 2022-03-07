using System;

namespace Com.Application.Common.Exceptions;

public class CMSException : Exception
{
    public CMSException(string message) : base(message) {}
}