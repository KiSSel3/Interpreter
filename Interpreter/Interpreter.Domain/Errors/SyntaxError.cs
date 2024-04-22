using Interpreter.Domain.Models;

namespace Interpreter.Domain.Errors;

public class SyntaxError : Exception
{
    public Token Token { get; }
    
    public SyntaxError(string message, Token token = null) : base(message)
    {
        Token = token;
    }
}