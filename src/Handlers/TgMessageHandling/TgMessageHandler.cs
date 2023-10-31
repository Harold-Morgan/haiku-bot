using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

public class TgMessageHandler
{
    private readonly ILogger<TgMessageHandler> _logger;
    private readonly ITelegramBotClient _botClient;

    public TgMessageHandler(ILogger<TgMessageHandler> logger, ITelegramBotClient botClient)
    {
        _logger = logger;
        _botClient = botClient;
    }

    public async Task HandleBotAdded(Update update, CancellationToken token)
    {
        var message = update.Message!;

        _logger.LogDebug($"Bot added to chat. ChatName: {message.Chat.Title} Id: {message.Chat.Id}");

        await _botClient.SendTextMessageAsync(
            chatId: message.Chat.Id,
            text: "Привет! Я бот, распознающий хайку и хокку. Напишите /about для того что-бы узнать обо мне побольше!",
            cancellationToken: token,
            parseMode: ParseMode.Html);
    }
}