using Microsoft.EntityFrameworkCore;
using Telegram.Bot.Types;

public class DbService
{
    private readonly HaikuDbContext _dbContext;

    public DbService(HaikuDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task TryAddUser(User? user, CancellationToken token)
    {
        if (user == null)
            return;

        var existingUser = _dbContext.TelegramUsers.AsNoTracking().FirstOrDefault(x => x.UserId == user.Id);

        if (existingUser != null)
            return;

        _dbContext.TelegramUsers.Add(new TelegramUser
        {
            UserId = user.Id,
            IsBot = user.IsBot,
            FirstName = user.FirstName,
            LastName = user.LastName,
            Username = user.Username,
            EntryCreationDate = DateTime.UtcNow
        });

        await _dbContext.SaveChangesAsync(token);
    }

    public async Task AddPoetry(Update update, string poetryType, string poetry, CancellationToken token)
    {
        var chat = update.Message?.Chat ?? update.EditedMessage!.Chat;
        var message = update.Message ?? update.EditedMessage!;

        var existingChat = _dbContext.Chats.AsNoTracking().FirstOrDefault(x => x.ChatId == chat.Id);
        if (existingChat == null)
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

        _dbContext.Poetries.Add(new Poetry
        {
            ChatId = chat.Id,
            TelegramUserId = message.From!.Id,
            PoetryType = poetryType,
            HokkuText = poetry,
            CreationDate = DateTime.UtcNow
        });

        await _dbContext.SaveChangesAsync();
    }

    public async Task<UserStat[]> GetStats(long chatId, DateTime startTime, DateTime endTime, CancellationToken token)
    {
        var stats = await _dbContext.Poetries
        .AsNoTracking()
        .Where(x => x.ChatId == chatId && x.CreationDate >= startTime.ToUniversalTime() && x.CreationDate <= endTime.ToUniversalTime())
        .GroupBy(y => y.TelegramUser)
        .Select(group => new UserStat
        {
            User = group.Key,
            Count = group.Count()
        })
        .OrderByDescending(x => x.Count)
        .Take(10)
        .ToArrayAsync();

        return stats;
    }
}