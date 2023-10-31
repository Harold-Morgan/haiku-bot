namespace Haiku.Bot.Services;

using Telegram.Bot;
using Microsoft.Extensions.Options;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Polling;
using Haiku.Bot.Handlers;
using Telegram.Bot.Exceptions;

public class TelegramWorker : BackgroundService
{
    private readonly ILogger<TelegramWorker> _logger;
    private readonly TelegramBotClient _client;
    private readonly IServiceProvider _serviceProvider;


    public TelegramWorker(ILogger<TelegramWorker> logger,
      IOptions<TelegramSettings> settings,
      IServiceProvider serviceProvider)
    {
        _logger = logger;
        _serviceProvider = serviceProvider;

        _client = new TelegramBotClient(settings.Value.Token);
    }

    protected override async Task ExecuteAsync(CancellationToken ct)
    {
        var info = await _client.GetMeAsync();
        _logger.LogInformation($"Client connected. Username: {info.Username} Id: {info.Id}");

        var receiverOptions = new ReceiverOptions()
        {
            AllowedUpdates = new UpdateType[]{
                UpdateType.Message
            },
        };

        _client.StartReceiving(
            updateHandler: async (client, update, token) =>
            {
                using (var scope = _serviceProvider.CreateScope())
                {
                    var handler = scope.ServiceProvider.GetRequiredService<MainHandler>();

                    await handler.HandleUpdateAsync(update, token);
                }
            },
            pollingErrorHandler: HandlePollingErrorAsync,
            receiverOptions: receiverOptions,
            cancellationToken: ct
        );
    }

    private async Task HandlePollingErrorAsync(ITelegramBotClient client, Exception exception, CancellationToken token)
    {
        var error = exception switch
        {
            ApiRequestException apiRequestException
                => $"Telegram API Error: [{apiRequestException.ErrorCode}] {apiRequestException.Message}",
            _ => exception.ToString()
        };

        _logger.LogError(error);
    }
}
