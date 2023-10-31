using Haiku.Bot.Startup;
using Microsoft.EntityFrameworkCore;
using Serilog;

internal class Program
{
    private static void Main(string[] args)
    {
        Log.Logger = new LoggerConfiguration()
        .WriteTo.Console()
        .CreateBootstrapLogger();

        try
        {
            Log.Information("Startup");

            var builder = Host.CreateDefaultBuilder(args);

            builder.UseSerilog();

            var host = builder
                .ConfigureServices((context, services) =>
                {
                    services.AddConfiguration(context.Configuration);

                    services.AddServices();
                })
                .Build();

            host.Services.ValidateCongifuration();

            ApplyMigration(host);

            host.Run();
        }
        catch (Exception ex)
        {
            Log.Fatal(ex, "Startup terminated");
        }
        finally
        {
            Log.CloseAndFlush();
        }
    }

    private static void ApplyMigration(IHost host)
    {
        using var scope = host.Services.CreateScope();

        var db = scope.ServiceProvider.GetRequiredService<HaikuDbContext>();
        db.Database.Migrate();
    }
}