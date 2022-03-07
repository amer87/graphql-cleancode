using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using MediatR;
using Com.Application.Common.Interfaces;
using Com.Application.Common.Behaviours;

namespace Com.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddAutoMapper(Assembly.GetExecutingAssembly());
        services.AddMediatR(Assembly.GetExecutingAssembly()); 
        //services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestValidationBehavior<,>));
        services.AddScoped<IJwtProvider, JwtProvider>();
        return services;
    }
}
