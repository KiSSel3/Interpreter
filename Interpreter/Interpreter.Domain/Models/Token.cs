namespace Interpreter.Domain.Models;

public class Token
{
    public string Id { get; set; }
    public string Value { get; set; }
    public int Position { get; set; }
    public TokenType Type { get; set; }

    public Token(string id, string value, int position, TokenType type) =>
        (Id, Value, Position, Type) = (id, value, position, type);
}