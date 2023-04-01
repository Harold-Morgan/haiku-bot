using System.Text;

public static class HokkuHandler
{
    public static bool TryFormPoetry(string input, out string? poetry)
    {
        poetry = null;

        if (input.Length <= 17)
            return false;

        var words = input.Split(' ').Select(word => word.Trim()).ToArray();

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

        poetry = sb.ToString();

        return true;
    }
}