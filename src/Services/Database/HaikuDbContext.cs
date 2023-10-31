using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

class HaikuDbContext : DbContext
{
    private DbSettings _settings;

    public DbSet<Chat> Chats { get; set; }

    public HaikuDbContext(IOptions<DbSettings> settings)
    {
        _settings = settings.Value;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql(_settings.ConnectionString);
    }
}