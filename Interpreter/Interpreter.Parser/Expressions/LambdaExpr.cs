using Interpreter.Domain.Models;

namespace Interpreter.Parser.Expressions;

public class LambdaExpr : BaseExpr
{
    public List<Token> Args { get; }
    public List<BaseExpr> Body { get; }

    public LambdaExpr(List<Token> args, List<BaseExpr> body)
    {
        Args = args;
        Body = body;
    }
}