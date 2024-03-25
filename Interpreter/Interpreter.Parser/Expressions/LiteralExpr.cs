using Interpreter.Domain.Models;

namespace Interpreter.Parser.Expressions;

public class LiteralExpr : BaseExpr
{
    public Token Token { get; }

    public LiteralExpr(Token token)
    {
        Token = token;
    }
}