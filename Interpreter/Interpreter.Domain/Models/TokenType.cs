namespace Interpreter.Domain.Models;

public class TokenType
{
    public string Value { get; }
    public string Regex { get; }

    public TokenType(string value, string regex) =>
        (Value, Regex) = (value, regex);
}