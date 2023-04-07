using System.Text;

class Help : ICommand
{
    public string HandleCommand(params string[] parameters)
    {
        var sb = new StringBuilder();
        sb.Append(Environment.NewLine);
        sb.Append(Environment.NewLine);

        foreach (var command in parameters)
        {
            sb.Append("/");
            sb.Append(command);
            sb.Append(Environment.NewLine);
        }

        return "Список команд, что я умею выполнять: " + sb.ToString();
    }
}