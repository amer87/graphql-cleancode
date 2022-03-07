using Com.Application.Common.Exceptions;
using Com.Application.Common.Extentions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Threading.Tasks;

namespace Com.Web.Api.Common;

public class CustomExceptionHandlerMiddleware
{
    private readonly RequestDelegate _next;

    public CustomExceptionHandlerMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(context, ex);
        }
    }

    private Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        var code = HttpStatusCode.InternalServerError;

        string result;
        switch (exception)
        {
            case UnauthorizedAccessException _:
                code = HttpStatusCode.Unauthorized;
                result = JsonConvert.SerializeObject(exception.Message);
                break;
            case ValidationException validationException:
                code = HttpStatusCode.BadRequest;
                result = JsonConvert.SerializeObject(validationException.Failures);
                break;
            case BadRequestException badRequestException:
                code = HttpStatusCode.BadRequest;
                result = badRequestException.Message;
                break;
            case NotFoundException _:
                code = HttpStatusCode.NotFound;
                result = exception.Message;
                break;
            default:
                result = exception.GetFullMessage();
                break;
        }

        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)code;
        return context.Response.WriteAsync(JsonConvert.SerializeObject(new { error = result }));
    }
}

public static class CustomExceptionHandlerMiddlewareExtensions
{
    public static IApplicationBuilder UseCustomExceptionHandler(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<CustomExceptionHandlerMiddleware>();
    }
}
