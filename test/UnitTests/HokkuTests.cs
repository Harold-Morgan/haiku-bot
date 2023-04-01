namespace Haiku.Bot.Tests;

public class HokkuTests
{
    [Fact]
    public void RecognizeHokku()
    {
        var words = new string[] { "На", "голой", "ветке", "Ворон", "сидит", "одинок.", "Осенний", "вечер." };

        var result = HokkuHandler.TryFormPoetry(words, out var _);

        Assert.True(result);
    }

    [Fact]
    public void RecognizeTanka()
    {
        var words = new string[] { "В", "глубине", "в", "горах", "топчет", "красный", "клёна", "лист", "стонущий", "олень,",
        "слышу", "плач", "его…", "во", "мне", "вся", "осенняя", "печаль"};

        var result = TankaHandler.TryFormPoetry(words, out var _);

        Assert.True(result);
    }

    [Fact]
    public void NoHokkuNoTanka()
    {
        var words = new string[] { "Это", "Точно", "Не", "хокку" };

        var formedHokku = HokkuHandler.TryFormPoetry(words, out var _);

        Assert.False(formedHokku);

        var formedTanka = TankaHandler.TryFormPoetry(words, out var _);

        Assert.False(formedTanka);
    }
}