namespace Haiku.Bot.Services;

using Telegram.Bot;
using Microsoft.Extensions.Options;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Polling;
using Haiku.Bot.Handlers;

public class TelegramWorker : BackgroundService
{
    private readonly ILogger<TelegramWorker> _logger;
    private readonly TelegramBotClient _client;
    private readonly MainHandler _handler;

    public TelegramWorker(ILogger<TelegramWorker> logger, IOptions<TelegramSettings> settings, MainHandler handler)
    {
        _logger = logger;

        _client = new TelegramBotClient(settings.Value.Token);
        _handler = handler;
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
            updateHandler: _handler.HandleUpdateAsync,
            pollingErrorHandler: _handler.HandlePollingErrorAsync,
            receiverOptions: receiverOptions,
            cancellationToken: ct
        );
    }
}
