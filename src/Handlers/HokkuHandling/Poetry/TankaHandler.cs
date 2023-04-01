using System.Text;

public class TankaHandler
{
    // 31 слог + пробелы = 62 - последний пробел
    public static bool TooShortForTanka(int charLength) => charLength < 61;

    public static bool TryFormPoetry(string[] words, out string? poetry)
    {
        poetry = null;

        var sb = new StringBuilder();

        if (!GrammarHelper.TryExtractLine(words, 5, out var firstLine, out var firstOffset))
            return false;

        sb.Append(firstLine).Append(Environment.NewLine);
        words = words.Skip(firstOffset).ToArray();

        if (!GrammarHelper.TryExtractLine(words, 7, out var secondLine, out var secondOffset))
            return false;

        sb.Append(secondLine).Append(Environment.NewLine);
        words = words.Skip(secondOffset).ToArray();

        if (!GrammarHelper.TryExtractLine(words, 5, out var thirdLine, out var thirdOffset))
            return false;

        sb.Append(thirdLine).Append(Environment.NewLine);
        words = words.Skip(thirdOffset).ToArray();

        if (!GrammarHelper.TryExtractLine(words, 7, out var fourthLine, out var fourthOffset))
            return false;

        sb.Append(fourthLine).Append(Environment.NewLine);
        words = words.Skip(fourthOffset).ToArray();

        if (!GrammarHelper.TryExtractLine(words, 7, out var fithLine, out var fithOffset))
            return false;

        sb.Append(fithLine).Append(Environment.NewLine);
        words = words.Skip(fithOffset).ToArray();

        poetry = sb.ToString();

        return true;
    }
}