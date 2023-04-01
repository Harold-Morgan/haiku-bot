using Haiku.Bot.Handlers;
using Haiku.Bot.Services;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = Host.CreateDefaultBuilder(args);

        var host = builder
            .ConfigureServices((context, services) =>
            {
                services.AddConfiguration(context.Configuration);

                AddServices(services);
            })
            .Build();

        host.Run();
    }

    private static IServiceCollection AddServices(IServiceCollection services)
    {
        services.AddSingleton<MainHandler>();

        services.AddHostedService<TelegramWorker>();

        return services;
    }
}