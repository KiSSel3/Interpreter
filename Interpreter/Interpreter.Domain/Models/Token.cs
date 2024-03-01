namespace Interpreter.Domain.Models;

public class Token
{
    public string Value { get; set; }
    public int Position { get; set; }
    public TokenType Type { get; set; }

    public Token(string value, int position, TokenType type) =>
        (Value, Position, Type) = (value, position, type);
}