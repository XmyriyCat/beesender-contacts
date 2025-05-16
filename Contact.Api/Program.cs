using Contact.Api.Extensions;

namespace Contact.Api;

public class Program
{
    public static async Task Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddControllers();
        builder.Services.AddOpenApi();

        builder.Services
            .ConfigureMsSqlContext(builder.Configuration)
            .ConfigureRepositories()
            .ConfigureServices()
            .ConfigureMapster()
            .ConfigureNewtonsoftJson()
            .AddFluentValidators();

        var app = builder.Build();

        app.UseGlobalExceptionHandler();
        
        await app.MigrateDbAsync();

        if (app.Environment.IsDevelopment())
        {
            app.MapOpenApi();
        }

        app.MapControllers();
        app.UseHttpsRedirection();

        await app.RunAsync();
    }
}