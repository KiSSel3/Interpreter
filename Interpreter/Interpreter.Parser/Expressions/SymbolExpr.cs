using Interpreter.Domain.Models;

namespace Interpreter.Parser.Expressions;

public class SymbolExpr : BaseExpr
{
    public Token Token { get; }

    public SymbolExpr(Token token)
    {
        Token = token;
    }
}