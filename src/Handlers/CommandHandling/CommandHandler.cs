public class CommandHandler
{
    internal string ParseCommand(string input)
    {
        var command = string.Empty;

        string result = input switch
        {
            "/help" => Help.CommandHandler(),

            _ => "Команда не распознана"
        };


        return result;
    }
}