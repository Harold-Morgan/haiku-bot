public static class RandomHelper
{
    private static Random Random = new Random();

    public static string RandomString(string[] input)
    {
        var roll = Random.Next(0, input.Length - 1);

        return input[roll];
    }
}