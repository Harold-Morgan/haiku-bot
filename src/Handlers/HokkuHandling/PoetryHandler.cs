using System.Text;

public class PoetryHandler
{
    private readonly PrefixService _prefixer;

    public PoetryHandler(PrefixService prefix)
    {
        _prefixer = prefix;
    }

    public string? Handle(string input)
    {
        var words = input
            .Split(new string[] { "\r\n", "\r", "\n", " " }, StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries)
            .Select(word => word.Trim())
            .Where(word => !string.IsNullOrEmpty(word))
            .ToArray();

        var answerBuilder = new StringBuilder();

        if (!HokkuHandler.TooShortForHokku(input.Length) && HokkuHandler.TryFormPoetry(words, out var hokku))
        {
            answerBuilder.Append(_prefixer.TryAddPrefix());

            answerBuilder.Append("Найдено Хокку: ");
            answerBuilder.Append(Environment.NewLine);
            answerBuilder.Append(Environment.NewLine);
            answerBuilder.Append(hokku);
            answerBuilder.Append(Environment.NewLine);
        }

        if (!TankaHandler.TooShortForTanka(input.Length) && TankaHandler.TryFormPoetry(words, out var tanka))
        {
            answerBuilder.Append(_prefixer.TryAddPrefix());

            answerBuilder.Append("Найдена Танка: ");
            answerBuilder.Append(Environment.NewLine);
            answerBuilder.Append(Environment.NewLine);
            answerBuilder.Append(tanka);
            answerBuilder.Append(Environment.NewLine);
        }

        return answerBuilder.ToString();
    }
}