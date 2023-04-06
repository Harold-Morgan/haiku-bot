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
            await HandleUpdateInternal(client, update, token);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Eror handling Update: ");
        }
    }

    private async Task HandleUpdateInternal(ITelegramBotClient client, Update update, CancellationToken token)
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

        var reply = string.Empty;
        if (text.StartsWith('/'))
            reply = _commandHadnler.ParseCommand(text);
        else
            reply = _poetryHandler.Handle(text);


        if (string.IsNullOrEmpty(reply))
            return;

        var chatId = message.Chat.Id;
        await client.SendTextMessageAsync(
            chatId: chatId,
            text: reply,
            cancellationToken: token,
            parseMode: ParseMode.Html);
    }
}