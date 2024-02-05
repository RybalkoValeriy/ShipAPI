namespace Domain.Entities.Ships;

public record Ship
{
    public Code Code { get; set; }
    public string Name { get; set; }
    public int Length { get; set; }
    public int Width { get; set; }

    private Ship(Code code, string name, int length, int width)
    {
        Name = name;
        Length = length;
        Width = width;
        Code = code;
    }

    public static Ship Create(string code, string name, int length, int width)
    {
        Validate(name, length, width);

        return new Ship
         (
             Code.Create(code),
             name,
             length,
             width
         );
    }

    public void Update(string name, int length, int width)
    {
        Validate(name, length, width);

        Name = name;
        Length = length;
        Width = width;
    }

    private static void Validate(string name, int length, int width)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new ArgumentException("Incorrect Name");
        }

        if (length <= 0)
        {
            throw new ArgumentException("Length should be more than 0");
        }

        if (width <= 0)
        {
            throw new ArgumentException("Width should be more than 0");
        }
    }
}