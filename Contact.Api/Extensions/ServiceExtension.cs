using Contact.Data;
using Contact.Data.Repository.Contracts;
using Contact.Data.Repository.Implementations;
using Contact.Data.UnitOfWork;
using Microsoft.EntityFrameworkCore;

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
}