using System;
using System.Linq;
using System.Threading;
using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Com.Web.Api;

var host = CreateHostBuilder(args).Build();

using (var scope = host.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    try
    {
        var allSeeaders = AppDomain.CurrentDomain.GetAssemblies()
            .Where(s => s.FullName.StartsWith("Com.Application")).FirstOrDefault()?.GetTypes()
            .Where(t=> t.IsClass && t.Name.StartsWith("Seed") && t.Name.EndsWith("Command"))
            .ToList();

        var mediator = services.GetRequiredService<IMediator>();
        foreach(var seeder in allSeeaders)
        {
            await mediator.Send(Activator.CreateInstance(seeder), CancellationToken.None);
        }
        //await mediator.Send(new SeedValueObjectsCommand(), CancellationToken.None);
        //mediator.
    }
    catch (Exception ex)
    {
        var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "An error occurred while migrating or initializing the database.");
    }
}

host.Run();

IHostBuilder CreateHostBuilder(string[] args) =>
    Host.CreateDefaultBuilder(args)
        .ConfigureWebHostDefaults(webBuilder =>
        {
            webBuilder.UseStartup<Startup>();
        });
