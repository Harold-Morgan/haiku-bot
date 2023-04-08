using System.Reflection;
using Haiku.Bot.Handlers;
using Haiku.Bot.Services;
using Microsoft.Extensions.Options;
using Serilog;
using Telegram.Bot;

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
        services.AddScoped<MainHandler>();

        AddCommands(services);
        services.AddScoped<CommandHandler>();

        services.AddTransient<PoetryHandler>();
        services.AddTransient<PrefixService>();

        services.AddHostedService<TelegramWorker>();

        services.AddHttpClient("telegram_bot_client")
            .AddTypedClient<ITelegramBotClient>((httpClient, sp) =>
            {
                var botConfig = sp.GetRequiredService<IOptions<TelegramSettings>>();
                TelegramBotClientOptions options = new(botConfig.Value.Token);
                return new TelegramBotClient(options, httpClient);
            });

        return services;
    }

    private static IServiceCollection AddCommands(IServiceCollection services)
    {
        services.Scan(
            scan => scan.FromAssemblyOf<ICommand>()
                .AddClasses(classes => classes.AssignableTo<ICommand>())
                    .As<ICommand>()
                    .WithScopedLifetime());

        return services;
    }
}