namespace Interpreter.Parser.Expressions;

public class QuoteExpr : BaseExpr
{
    public BaseExpr Value { get; }

    public QuoteExpr(BaseExpr value)
    {
        Value = value;
    }
}