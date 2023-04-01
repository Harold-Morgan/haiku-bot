using System.Text;

public class PoetryHandler
{
    internal string? Handle(string input)
    {
        var words = input.Split(' ')
            .Select(word => word.Trim())
            .Where(word => !string.IsNullOrEmpty(word))
            .ToArray();

        var answerBuilder = new StringBuilder();

        if (!HokkuHandler.TooShortForHokku(input.Length) && HokkuHandler.TryFormPoetry(words, out var hokku))
        {
            answerBuilder.Append("Я сформировал Хокку: ");
            answerBuilder.Append(Environment.NewLine);
            answerBuilder.Append(Environment.NewLine);
            answerBuilder.Append(hokku);
            answerBuilder.Append(Environment.NewLine);
        }

        if (!TankaHandler.TooShortForTanka(input.Length) && TankaHandler.TryFormPoetry(words, out var tanka))
        {
            answerBuilder.Append("А ещё тут есть Танка: ");
            answerBuilder.Append(Environment.NewLine);
            answerBuilder.Append(Environment.NewLine);
            answerBuilder.Append(tanka);
            answerBuilder.Append(Environment.NewLine);
        }

        return answerBuilder.ToString();
    }
}