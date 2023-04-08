using Telegram.Bot.Types;
using Telegram.Bot;
using Haiku.Bot.Services;
using Telegram.Bot.Types.Enums;

namespace Haiku.Bot.Handlers;

public class MainHandler
{
    private readonly ILogger<TelegramWorker> _logger;
    private readonly CommandHandler _commandHadnler;
    private readonly PoetryHandler _poetryHandler;

    public MainHandler(ILogger<TelegramWorker> logger, CommandHandler commandHandler, PoetryHandler hokkuHandler)
    {
        _logger = logger;
        _commandHadnler = commandHandler;
        _poetryHandler = hokkuHandler;
    }


    public async Task HandleUpdateAsync(ITelegramBotClient client, Update update, CancellationToken token)
    {
        _logger.LogInformation("Update recieved");

        try
        {
            await HandleUpdateInternal(update, token);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Eror handling Update: ");
        }
    }

    private async Task HandleUpdateInternal(Update update, CancellationToken token)
    {
        if (update.Type != UpdateType.Message && update.Type != UpdateType.EditedMessage)
            return;

        var message = update.Message;

        if (message == null || message.Type != MessageType.Text)
            return;

        var text = message.Text;

        if (string.IsNullOrEmpty(text))
            return;

        text = text.Trim();


        if (text.StartsWith('/'))
            await _commandHadnler.ParseAndHandleCommand(update, text, token);
        else
            await _poetryHandler.Handle(update, text, token);
    }
}