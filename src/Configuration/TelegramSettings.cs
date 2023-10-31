using System.ComponentModel.DataAnnotations;

public class TelegramSettings
{
    [Required]
    public string Token { get; set; } = null!;

    public void Validate()
    {
        if (Token == null)
            throw new NullReferenceException("Settings parameter Telegram:Token is null");
    }
}