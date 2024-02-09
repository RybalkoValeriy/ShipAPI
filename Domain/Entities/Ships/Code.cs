using System.Text.RegularExpressions;

namespace Domain.Entities.Ships;

public partial record Code
{
    [GeneratedRegex(pattern)]
    private static partial Regex MyRegex();

    public const string pattern = @"^[A-Za-z]{4}-\d{4}-[A-Za-z]\d$";

    public string Value { get; private set; }

    private Code(string value) =>
        Value = value;

    public static Code Create(string value) =>
        MyRegex().IsMatch(value)
            ? new Code(value)
            : throw new ArgumentException("Incorrect Code");
}

