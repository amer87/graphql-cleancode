using System;

namespace Com.Application.Common.Extentions;

public static class ExceptionExt
{
    public static string GetFullMessage(this Exception ex)
    {
        return ex.InnerException == null
             ? ex.Message
             : ex.InnerException.GetFullMessage();
    }
}
