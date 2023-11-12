using System.Text;
using Haiku.Bot.Handlers.CommandHandling;
using Telegram.Bot;
using Telegram.Bot.Types.Enums;

public class Stats : ICommand
{
    private readonly ITelegramBotClient _botClient;
    private readonly DbService _dbService;

    public Stats(ITelegramBotClient botClient, DbService dbService)
    {
        _botClient = botClient;
        _dbService = dbService;
    }

    public async Task HandleCommand(CommandParameters @params, CancellationToken token = default)
    {
        var update = @params.Update;
        var message = update.Message!;

        var (startDate, endDate) = GetInterval(@params.TextParams);

        var stats = await _dbService.GetStats(update!.Message!.Chat.Id, startDate, endDate, token);

        var sb = new StringBuilder();
        sb.AppendLine($"Статистика для данного чата ");
        sb.AppendLine($"(я считаю от {startDate.Date.ToString("dd.MM.yyyy")} по {endDate.Date.ToString("dd.MM.yyyy")} включительно)");
        for (int i = 0; i < stats.Length; i++)
        {
            UserStat? entry = stats[i];
            sb.AppendLine($"{i + 1}. {entry.User.Username} - {entry.Count}");
        }

        await _botClient.SendTextMessageAsync(
        chatId: message.Chat.Id,
            text: sb.ToString(),
            cancellationToken: token,
            parseMode: ParseMode.Html);
    }

    private (DateTime, DateTime) GetInterval(string[]? commandParams = null)
    {
        var now = DateTime.Now;
        var defaultStartDate = new DateTime(now.Year, now.Month, 1);
        var defaultEndDate = defaultStartDate.AddMonths(1).AddDays(-1);

        if (commandParams == null || commandParams.Length != 2)
            return (defaultStartDate, defaultEndDate);

        if (!DateTime.TryParse(commandParams[0], out var startDate) || !DateTime.TryParse(commandParams[1], out var endDate))
            return (defaultStartDate, defaultEndDate);

        return (startDate, endDate);
    }
}