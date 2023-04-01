public class PoetryHandler
{
    internal string? Handle(string input)
    {
        if (HokkuHandler.TryFormPoetry(input, out var hokku))
            return "Я сформировал хокку: "
            + Environment.NewLine
            + Environment.NewLine
            + hokku;

        return string.Empty;
    }
}