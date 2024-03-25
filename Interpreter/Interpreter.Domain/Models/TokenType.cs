namespace Interpreter.Domain.Models;

public class TokenType
{
    public string Value { get; }
    public string GroupName { get; }
    public string Regex { get; }

    public TokenType(string value, string groupName, string regex) =>
        (Value, GroupName, Regex) = (value, groupName, regex);
}