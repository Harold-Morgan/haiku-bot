using Haiku.Bot.Handlers.CommandHandling;
using System.Text;
using Telegram.Bot;

class Help : ICommand
{
    private readonly ITelegramBotClient _botClient;

    public Help(ITelegramBotClient botClient)
    {
        _botClient = botClient;
    }

    public string Description => "Помочь разобраться с командами";

    public async Task HandleCommand(CommandParameters @params, CancellationToken token = default)
    {
        var sb = new StringBuilder();
        sb.Append(Environment.NewLine);
        sb.Append(Environment.NewLine);

        foreach (var command in @params.TextParams)
        {
            sb.Append("/");
            sb.Append(command);
            sb.Append(Environment.NewLine);
            sb.Append(Environment.NewLine);
        }

        var response = "Список команд, что я умею выполнять: " + sb.ToString();

        var update = @params.Update;
        var message = update.Message!;

        await _botClient.SendTextMessageAsync(
            chatId: message.Chat.Id,
            text: response,
            cancellationToken: token);
    }
}