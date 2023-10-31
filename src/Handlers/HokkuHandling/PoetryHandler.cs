using System.Text;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

public class PoetryHandler
{
    private readonly PrefixService _prefixer;
    private readonly ITelegramBotClient _botClient;
    private readonly DbService _dbService;

    public PoetryHandler(PrefixService prefix, ITelegramBotClient botClient, DbService dbService)
    {
        _prefixer = prefix;
        _botClient = botClient;
        _dbService = dbService;
    }

    public async Task Handle(Update update, string input, CancellationToken token)
    {
        var user = update.Message?.From ?? update.EditedMessage!.From;
        await _dbService.TryAddUser(user, token);

        var words = input
            .Split(new string[] { "\r\n", "\r", "\n", " " }, StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries)
            .Select(word => word.Trim())
            .Where(word => !string.IsNullOrEmpty(word))
            .ToArray();

        var responseBuilder = new StringBuilder();

        if (!HokkuHandler.TooShortForHokku(input.Length) && HokkuHandler.TryFormPoetry(words, out var hokku))
        {
            responseBuilder.Append(_prefixer.TryAddPrefix());

            responseBuilder.Append("Найдено Хокку: ");
            responseBuilder.Append(Environment.NewLine);
            responseBuilder.Append(Environment.NewLine);
            responseBuilder.Append(hokku);
            responseBuilder.Append(Environment.NewLine);

            await _dbService.AddPoetry(update, "Hokku", hokku!, token);
        }

        if (!TankaHandler.TooShortForTanka(input.Length) && TankaHandler.TryFormPoetry(words, out var tanka))
        {
            responseBuilder.Append(_prefixer.TryAddPrefix());

            responseBuilder.Append("Найдена Танка: ");
            responseBuilder.Append(Environment.NewLine);
            responseBuilder.Append(Environment.NewLine);
            responseBuilder.Append(tanka);
            responseBuilder.Append(Environment.NewLine);

            await _dbService.AddPoetry(update, "Tanka", tanka!, token);
        }

        var response = responseBuilder.ToString();

        if (string.IsNullOrEmpty(response))
            return;

        var message = update.Message!;

        await _botClient.SendTextMessageAsync(
            chatId: message.Chat.Id,
            text: response,
            cancellationToken: token,
            parseMode: ParseMode.Html);
    }
}