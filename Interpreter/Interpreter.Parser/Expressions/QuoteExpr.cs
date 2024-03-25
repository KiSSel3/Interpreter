namespace Interpreter.Parser.Expressions;

public class QuoteExpr
{
    public BaseExpr Value { get; }

    public QuoteExpr(BaseExpr value)
    {
        Value = value;
    }
}