using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using FluentValidation;
using GraphQL.Server.Ui.Voyager;
using Com.Application;
using Com.Application.Common;
using Com.Infrastructure;
using Com.Persistence;
using Com.Web.Api.GraphQL.Items;
using Com.Web.Api.GraphQL.Shops;
using Com.Web.Api.GraphQL.Types;
using Com.Application.Common.Interfaces;
using Com.Web.Api.Services;
using Com.Web.Api.GraphQL.Users;
using System;
using Microsoft.OpenApi.Models;

namespace Com.Web.Api;

public class Startup
{
    public Startup(IConfiguration configuration, IWebHostEnvironment environment)
    {
        Configuration = configuration;
        Environment = environment;
    }

    public IConfiguration Configuration { get; }
    public IWebHostEnvironment Environment { get; }
    private AppSettings _appSettings;

    public void ConfigureServices(IServiceCollection services)
    {
        var appSettingsSection = Configuration.GetSection("AppSettings");
        services.Configure<AppSettings>(appSettingsSection);
        _appSettings = appSettingsSection.Get<AppSettings>();

        services.AddInfrastructure(_appSettings)
            .AddPersistence(Configuration)
            .AddApplication();


        services.AddScoped<ICurrentUserService, CurrentUserService>();
        services.AddValidatorsFromAssemblyContaining<Application.Users.Commands.UpsertUserCommandValidator>();

        services.AddHttpContextAccessor();
        services
            .AddGraphQLServer()
            .AddFairyBread()
            .AddAuthorization()
            .AddQueryType()
                .AddTypeExtension<ItemQueries>()
                .AddTypeExtension<ShopQueries>()
                .AddTypeExtension<UserQueries>()
            .AddMutationType()
                .AddTypeExtension<ItemMutations>()
                .AddTypeExtension<ShopMutations>()
                .AddTypeExtension<UserMutations>()
            .AddType<FileType>()
            .AddType<ItemType>()
            .AddType<ShopType>()
            .AddType<UserType>();
        //.AddErrorFilter<GraphQLExceptionHandlerFilter>();

        services.AddControllers();
        services.AddCors();
        services.AddSwaggerGen(c => { //<-- NOTE 'Add' instead of 'Configure'
            c.SwaggerDoc("v3", new OpenApiInfo
            {
                Title = "GTrackAPI",
                Version = "v3"
            });
        });
    }

    public void Configure(IApplicationBuilder app)
    {
        if (Environment.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }
        else
        {
            app.UseExceptionHandler("/Error");
            // app.UseHsts();
        }
        // Shows UseCors with CorsPolicyBuilder.
        app.UseCors(builder =>
        {
            builder
            .AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader();
        });

        //app.UseIpRateLimiting();
        /*app.UseCustomExceptionHandler();
        */
        app.UseStaticFiles();

        app.UseSwagger();
        app.UseSwaggerUI(c =>
        {
            c.DefaultModelsExpandDepth(-1); 
            c.SwaggerEndpoint("/swagger/v1/swagger.json", $"{_appSettings.AppName} API V1");
        });


        
        app.UseRouting();
        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllerRoute(
                name: "default",
                pattern: "{controller}/{action=Index}/{id?}");
            endpoints.MapControllers();
        });

        app.UseAuthentication();
        app.UseWebSockets();
        app.UseRouting();
        app.UseEndpoints(endpoints =>
        {
            endpoints.MapGraphQL();
        });
        app.UseGraphQLVoyager(new VoyagerOptions()
        {
            GraphQLEndPoint = "/graphql"
        }, "/graphql-voyager");

    }
}
