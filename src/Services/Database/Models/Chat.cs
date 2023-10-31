using Telegram.Bot.Types.Enums;

class Chat
{
    public long ChatId { get; set; }
    public ChatType Type { get; set; }
    public string? Title { get; set; }
    public DateTime BotAdded { get; set; }
    public DateTime BotDeleted { get; set; }
}