namespace Interpreter.Parser.Expressions;

public class IfExpr : BaseExpr
{
    public BaseExpr Condition { get; }
    public BaseExpr TrueExpr { get; }
    public BaseExpr FalseExpr { get; }

    public IfExpr(BaseExpr condition, BaseExpr trueExpr, BaseExpr falseExpr)
    {
        Condition = condition;
        TrueExpr = trueExpr;
        FalseExpr = falseExpr;
    }
}