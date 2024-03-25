namespace Interpreter.Parser.Expressions;

public class ListExpr : BaseExpr
{
    public List<BaseExpr> Items { get; }

    public ListExpr(List<BaseExpr> items)
    {
        Items = items;
    }
}