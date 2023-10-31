using System.ComponentModel.DataAnnotations;

public class Poetry
{
    [Key]
    public long PoetryId { get; set; }

    public long ChatId { get; set; }

    public Chat Chat { get; set; } = null!;

    public long TelegramUserId { get; set; }

    public TelegramUser TelegramUser { get; set; } = null!;

    public string PoetryType { get; set; } = null!;

    public string HokkuText { get; set; } = null!;

    public DateTime CreationDate { get; set; }
}