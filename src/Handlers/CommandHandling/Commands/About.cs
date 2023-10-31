using Haiku.Bot.Handlers.CommandHandling;
using Telegram.Bot;
using Telegram.Bot.Types.Enums;

public class About : ICommand
{
    private readonly ITelegramBotClient _botClient;

    public About(ITelegramBotClient botClient)
    {
        _botClient = botClient;
    }

    public async Task HandleCommand(CommandParameters @params, CancellationToken token = default)
    {
        var response = "Я - бот распознающий хокку и танка во введенных пользователями в чатах (или написанных лично мне) сообщениях. " +
        Environment.NewLine + Environment.NewLine +
        "Обычно я молчу, но если я увижу сообщение, начало которого можно переформотировать в свойственный хокку формат \"трёхстрочного 5-7-5 стихотворения\", " +
        "то я это непременно отмечу " +
        Environment.NewLine + Environment.NewLine +
        "Больше о поэззии <a href=\"https://ru.wikipedia.org/wiki/%D0%A5%D0%B0%D0%B9%D0%BA%D1%83\">хокку</a> " +
        "или <a href=\"https://ru.wikipedia.org/wiki/%D0%A2%D0%B0%D0%BD%D0%BA%D0%B0\">танка</a> можно узнать кликнув по соответствующим ссылкам. " +
        Environment.NewLine + Environment.NewLine +
        "Ах да - я работаю только на русском языке. На других, к сожалению, не планирую";


        var update = @params.Update;
        var message = update.Message!;

        await _botClient.SendTextMessageAsync(
        chatId: message.Chat.Id,
            text: response,
            cancellationToken: token,
            parseMode: ParseMode.Html);
    }
}