using System.ComponentModel.DataAnnotations;

public class TelegramUser
{
    [Key]
    public long UserId { get; set; }

    public bool IsBot { get; set; }

    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    public string? Username { get; set; }

    public DateTime EntryCreationDate { get; set; }
}