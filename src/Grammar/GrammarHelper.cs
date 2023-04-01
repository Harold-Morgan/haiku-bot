using System.Text;

public static class GrammarHelper
{
    public static char[] RussianVowels = new char[] { 'а', 'я', 'у', 'ю', 'о', 'е', 'ё', 'э', 'и', 'ы' };

    public static int CountSyllables(string input)
    {
        var trimmedInput = input.Trim().ToLowerInvariant();

        int count = 0;

        foreach (var symbol in trimmedInput)
        {
            if (RussianVowels.Contains(symbol))
                count++;
        }

        return count;
    }

    public static bool TryExtractLine(string[] input, int expectedSyllables, out string? line, out int wordsConsumed)
    {
        line = null;
        wordsConsumed = 0;

        if (expectedSyllables == 0 || input.Length == 0)
            return false;

        var sb = new StringBuilder();
        var syllableCount = 0;

        foreach (var word in input)
        {
            var syllables = CountSyllables(word);
            syllableCount += syllables;
            wordsConsumed++;

            if (syllableCount > expectedSyllables)
                return false;

            sb.Append(word + ' ');

            if (syllableCount == expectedSyllables)
            {
                line = sb.ToString();
                return true;
            }
        }

        return false;
    }
}