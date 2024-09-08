namespace Booking.Domain.ValueObjects;

public record ConfirmationCode
{
    public string Value { get; }
    
    private ConfirmationCode(string value) => Value = value;
    
    public static ConfirmationCode Of(string value) => new ConfirmationCode(value);
    
    public static ConfirmationCode Generate()
    {
        var bytes = new byte[4];
        using (var rng = RandomNumberGenerator.Create())
        {
            rng.GetBytes(bytes);
        }
        var code = Convert.ToBase64String(bytes).Replace("/", "").Replace("+", "")[..6].ToUpper();
        return new ConfirmationCode(code);
    }
}