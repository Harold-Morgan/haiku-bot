using System.ComponentModel.DataAnnotations;
using Telegram.Bot.Types.Enums;

public class Chat
{
    [Key]
    public long ChatId { get; set; }
    public ChatType Type { get; set; }
    public string? Title { get; set; }
    public DateTime BotAdded { get; set; }
    public DateTime? BotDeleted { get; set; }
}