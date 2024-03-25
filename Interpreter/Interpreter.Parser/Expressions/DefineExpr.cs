using Interpreter.Domain.Models;

namespace Interpreter.Parser.Expressions;

public class DefineExpr : BaseExpr
{
    public Token Variable { get; }
    public BaseExpr Value { get; }

    public DefineExpr(Token variable, BaseExpr value)
    {
        Variable = variable;
        Value = value;
    }
}