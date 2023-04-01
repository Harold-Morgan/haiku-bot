public static class Configuration
{
    public static IServiceCollection AddConfiguration(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddOptions();

        services.Configure<TelegramSettings>(configuration.GetSection(nameof(TelegramSettings)));

        return services;
    }
}