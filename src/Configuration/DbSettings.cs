using System.ComponentModel.DataAnnotations;

public class DbSettings
{
    [Required]
    public string ConnectionString { get; set; } = null!;

    public void Validate()
    {
        if (ConnectionString == null)
            throw new NullReferenceException("Settings parameter Telegram:Token is null");
    }
}