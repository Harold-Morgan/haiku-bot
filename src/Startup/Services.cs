using Haiku.Bot.Handlers;
using Haiku.Bot.Services;
using Microsoft.Extensions.Options;
using Telegram.Bot;

namespace Haiku.Bot.Startup
{
    public static class Services
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddScoped<MainHandler>();

            services.AddCommands();
            services.AddScoped<CommandHandler>();
            services.AddScoped<TgMessageHandler>();

            services.AddTransient<PoetryHandler>();
            services.AddTransient<PrefixService>();

            services.AddHostedService<TelegramWorker>();

            services.AddSingleton<GlobalContext>();

            services.AddHttpClient("telegram_bot_client")
                .AddTypedClient<ITelegramBotClient>((httpClient, sp) =>
                {
                    var botConfig = sp.GetRequiredService<IOptions<TelegramSettings>>();
                    TelegramBotClientOptions options = new(botConfig.Value.Token);
                    return new TelegramBotClient(options, httpClient);
                });

            services.AddDbContext<HaikuDbContext>();

            return services;
        }

        public static IServiceCollection AddCommands(this IServiceCollection services)
        {
            services.Scan(
                scan => scan.FromAssemblyOf<ICommand>()
                    .AddClasses(classes => classes.AssignableTo<ICommand>())
                        .As<ICommand>()
                        .WithScopedLifetime());

            return services;
        }
    }
}
