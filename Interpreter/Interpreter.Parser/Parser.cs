using Interpreter.Domain.Models;
using Interpreter.Parser.Expressions;

namespace Interpreter.Parser;

public class Parser
{
    private List<Token> _tokens;
    public List<BaseExpr> Expressions { get; }

    public Parser(List<Token> tokens)
    {
        _tokens = tokens;

        Expressions = new List<BaseExpr>();
    }

    public bool Parse()
    {
        bool isSuccessful = true;

        foreach (var token in _tokens)
        {
            
        }
        
        return isSuccessful;
    }

    private BaseExpr NewExpression()
    {
        throw new NotImplementedException();
    }
}