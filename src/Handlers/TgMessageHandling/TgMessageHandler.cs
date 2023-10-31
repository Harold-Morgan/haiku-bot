using Microsoft.EntityFrameworkCore;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

public class TgMessageHandler
{
    private readonly ILogger<TgMessageHandler> _logger;
    private readonly ITelegramBotClient _botClient;
    private readonly HaikuDbContext _dbContext;
    private readonly GlobalContext _globalContext;

    public TgMessageHandler(ILogger<TgMessageHandler> logger, ITelegramBotClient botClient, HaikuDbContext dbContext, GlobalContext globalContext)
    {
        _logger = logger;
        _botClient = botClient;
        _dbContext = dbContext;
        _globalContext = globalContext;
    }

    public async Task HandleBotAdded(Update update, CancellationToken token)
    {
        var message = update.Message!;
        var chat = message.Chat;

        if (message.NewChatMembers!.All(x => x.Id != _globalContext.BotInfo.Id))
            return;

        _logger.LogDebug($"Bot added to chat. ChatName: {chat.Title} Id: {chat.Id}");

        var chatEntry = _dbContext.Chats.FirstOrDefault(x => x.ChatId == chat.Id);
        if (chatEntry == null)
        {
            _dbContext.Chats.Add(new Chat
            {
                ChatId = chat.Id,
                Type = chat.Type,
                Title = chat.Title,
                BotAdded = message.Date,
                BotDeleted = null
            });
        }
        else
        {
            chatEntry.BotAdded = message.Date;
            chatEntry.BotDeleted = null;
        }

        await _dbContext.SaveChangesAsync(token);

        await _botClient.SendTextMessageAsync(
            chatId: message.Chat.Id,
            text: "Привет! Я бот, распознающий хайку и хокку. Напишите /about для того что-бы узнать обо мне побольше!",
            cancellationToken: token,
            parseMode: ParseMode.Html);
    }

    public async Task BotDeleted(Update update, CancellationToken token)
    {
        var message = update.Message!;
        var chat = message.Chat;

        if (message?.LeftChatMember?.Id != _globalContext.BotInfo.Id)
            return;

        _logger.LogDebug($"Bot deleted from chat. ChatName: {chat.Title} Id: {chat.Id}");

        var chatEntry = _dbContext.Chats.FirstOrDefault(x => x.ChatId == chat.Id);
        if (chatEntry == null)
        {
            _dbContext.Chats.Add(new Chat
            {
                ChatId = chat.Id,
                Type = chat.Type,
                Title = chat.Title,
                BotAdded = message.Date,
                BotDeleted = message.Date
            });
        }
        else
            chatEntry.BotDeleted = message.Date;

        await _dbContext.SaveChangesAsync(token);
    }
}