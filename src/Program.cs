using Haiku.Bot.Handlers;
using Haiku.Bot.Services;
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

                    AddServices(services);
                })
                .Build();

            host.Services.ValidateCongifuration();

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

    private static IServiceCollection AddServices(IServiceCollection services)
    {
        services.AddSingleton<MainHandler>();
        services.AddSingleton<CommandHandler>();
        services.AddSingleton<PoetryHandler>();

        services.AddHostedService<TelegramWorker>();

        return services;
    }
}