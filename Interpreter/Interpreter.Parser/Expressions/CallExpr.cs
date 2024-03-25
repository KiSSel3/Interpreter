namespace Interpreter.Parser.Expressions;

public class CallExpr : BaseExpr
{
    public BaseExpr Called { get; }
    public List<BaseExpr> Args { get; }

    public CallExpr(BaseExpr called, List<BaseExpr> args)
    {
        Called = called;
        Args = args;
    }
}