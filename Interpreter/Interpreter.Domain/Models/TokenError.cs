namespace Interpreter.Domain.Models;

public class TokenError
{
    public TokenError(string value, int position, string description) =>
        (Value, Position, Description) = (value, position, description);

    public string Value { get; set; }
    public int Position { get; set; }
    public string Description { get; set; }
}