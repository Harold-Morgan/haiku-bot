using Microsoft.Extensions.Options;

public static class Configuration
{
    public static IServiceCollection AddConfiguration(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddOptions();

        services.AddConfig<TelegramSettings>(configuration);

        services.AddConfig<DbSettings>(configuration);

        return services;
    }

    private static void AddConfig<T>(this IServiceCollection services, IConfiguration configuration) where T : class
    {
        services
            .AddOptions<T>()
            .Bind(configuration.GetSection(typeof(T).Name))
            .ValidateDataAnnotations()
            .ValidateOnStart();
    }

    public static IServiceProvider ValidateCongifuration(this IServiceProvider provider)
    {
        // Форсит резолв Options инстансов из контейнера для их валидации. Потому что ValidateOnStart иначе не пашет.
        _ = provider.GetRequiredService<IOptions<TelegramSettings>>().Value;
        _ = provider.GetRequiredService<IOptions<DbSettings>>().Value;

        return provider;
    }
}