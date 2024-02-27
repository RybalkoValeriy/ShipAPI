namespace Infrastructure.Tests;
public static class TestHelper
{
    private static readonly Random random = new();
    public static string GetRandomCode()
    {
        var first = new string(Enumerable.Range(0, 4)
            .Select(_ => (char)(random.Next(26) + 'A'))
            .ToArray());

        var second = string.Concat(Enumerable.Range(0, 4)
            .Select(_ => random.Next(10).ToString()));

        var last = $"{(char)(random.Next(26) + 'A')}{random.Next(10)}";

        return $"{first}-{second}-{last}";
    }
}
