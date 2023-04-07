public interface ICommand
{
    public string HandleCommand(params string[] parameters);
}