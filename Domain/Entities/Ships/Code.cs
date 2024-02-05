using System.Text.RegularExpressions;

namespace Domain.Entities.Ships;

public record Code
{
    public const string pattern = @"^[A-Za-z]{4}-\d{4}-[A-Za-z]\d$";

    public string Value { get; set; }

    private Code(string value) =>
        Value = value;

    public static Code Create(string value) =>
        Regex.IsMatch(value, pattern)
            ? new Code(value)
            : throw new ArgumentException("Incorrect Code");
}

