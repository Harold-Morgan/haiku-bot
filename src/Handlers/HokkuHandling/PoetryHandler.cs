using Newtonsoft.Json.Linq;
using System.Text;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

public class PoetryHandler
{
    private readonly PrefixService _prefixer;
    private readonly ITelegramBotClient _botClient;

    public PoetryHandler(PrefixService prefix, ITelegramBotClient botClient)
    {
        _prefixer = prefix;
        _botClient = botClient;
    }

    public async Task Handle(Update update, string input, CancellationToken token)
    {
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
        }

        if (!TankaHandler.TooShortForTanka(input.Length) && TankaHandler.TryFormPoetry(words, out var tanka))
        {
            responseBuilder.Append(_prefixer.TryAddPrefix());

            responseBuilder.Append("Найдена Танка: ");
            responseBuilder.Append(Environment.NewLine);
            responseBuilder.Append(Environment.NewLine);
            responseBuilder.Append(tanka);
            responseBuilder.Append(Environment.NewLine);
        }

        var response =  responseBuilder.ToString();

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