using Microsoft.Extensions.Options;

public static class Configuration
{
    public static IServiceCollection AddConfiguration(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddOptions();

        services.AddOptions<TelegramSettings>()
            .Bind(configuration.GetSection(nameof(TelegramSettings)))
            .ValidateDataAnnotations()
            .ValidateOnStart();

        return services;
    }

    public static IServiceProvider ValidateCongifuration(this IServiceProvider provider)
    {
        // Форсит резолв Options инстансов из контейнера для их валидации. Потому что ValidateOnStart иначе не пашет.
        _ = provider.GetRequiredService<IOptions<TelegramSettings>>().Value;

        return provider;
    }
}