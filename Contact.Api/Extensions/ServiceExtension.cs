using Contact.Api.Middlewares;
using Contact.Api.Variables;
using Contact.Application;
using Contact.Application.Infrastructure.Mapster;
using Contact.Application.Services.Contracts;
using Contact.Application.Services.Implementations;
using Contact.Data;
using Contact.Data.Repository.Contracts;
using Contact.Data.Repository.Implementations;
using Contact.Data.UnitOfWork;
using FluentValidation;
using Mapster;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace Contact.Api.Extensions;

public static class ServiceExtension
{
    public static IServiceCollection ConfigureMsSqlContext(this IServiceCollection services, IConfiguration config)
    {
        var connectionString = config["ConnectionStrings:SqlServerConnection"];

        services.AddDbContext<ContactDataContext>(options => options.UseSqlServer(connectionString));

        return services;
    }

    public static async Task<IApplicationBuilder> MigrateDbAsync(this IApplicationBuilder app)
    {
        using var scope = app.ApplicationServices.CreateScope();

        var context = scope.ServiceProvider.GetRequiredService<ContactDataContext>();

        var pendingMigrations = await context.Database.GetPendingMigrationsAsync();

        if (pendingMigrations.Any())
        {
            Console.WriteLine("Applying pending migrations...");
            await context.Database.MigrateAsync();
            Console.WriteLine("Migrations applied.");
        }
        else
        {
            Console.WriteLine("Database is already up to date.");
        }

        return app;
    }

    public static IServiceCollection ConfigureRepositories(this IServiceCollection services)
    {
        services.AddScoped<IRepositoryWrapper, RepositoryWrapper>();
        services.AddScoped<IContactRepository, ContactRepository>();

        return services;
    }

    public static IServiceCollection ConfigureServices(this IServiceCollection services)
    {
        services.AddScoped<IContactService, ContactService>();

        return services;
    }

    public static IServiceCollection ConfigureMapster(this IServiceCollection services)
    {
        services.AddMapster();
        MappingProfiles.Configure();

        return services;
    }

    public static IServiceCollection ConfigureNewtonsoftJson(this IServiceCollection services)
    {
        services.AddControllersWithViews()
            .AddNewtonsoftJson(opt =>
                opt.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore);

        return services;
    }

    public static IServiceCollection AddFluentValidators(this IServiceCollection services)
    {
        services.AddValidatorsFromAssemblyContaining<IApplicationMarker>(ServiceLifetime.Singleton);

        return services;
    }

    public static IApplicationBuilder UseGlobalExceptionHandler(this IApplicationBuilder app)
    {
        app.UseMiddleware<GlobalExceptionHandler>();

        return app;
    }

    public static IServiceCollection ConfigureCors(this IServiceCollection services)
    {
        services.AddCors(options =>
        {
            options.AddPolicy(CorsValues.PolicyName, policy =>
            {
                policy.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader();
            });
        });

        return services;
    }

    public static IApplicationBuilder UseConfiguredCors(this IApplicationBuilder app)
    {
        app.UseCors(CorsValues.PolicyName);

        return app;
    }
}