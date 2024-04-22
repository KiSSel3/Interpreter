using System.Data.SqlTypes;
using Interpreter.Domain.Errors;
using Interpreter.Domain.Models;
using Interpreter.Lexer.Storage;
using Interpreter.Parser.Expressions;

namespace Interpreter.Parser;

public class Parser
{
    private readonly List<Token> _tokens;
    private int _currentIndex = 0;

    public Parser(List<Token> tokens)
    {
        _tokens = tokens;
    }

    public List<BaseExpr> Parse()
    {
        var expressions = new List<BaseExpr>();
        while (!IsEnd())
        {
            BaseExpr newExpression = GetNewExpression();
            
            expressions.Add(newExpression);
        }
        return expressions;
    }


    private BaseExpr GetNewExpression()
    {
        if (Match("openedBracket"))
        {
            if (Match("closedBracket"))
            {
                throw new SyntaxError($"Unexpected syntax", Previous());
            }

            var token = Peek();
            if (token.Value.Equals("define"))
            {
                return Define();
            }
            if (token.Value.Equals("lambda"))
            {
                return Lambda();
            }
            if (token.Value.Equals("if"))
            {
                return If();
            }
            if (token.Value.Equals("quote"))
            {
                return Quote();
            }
            return Call();
        }
        return Atom();
    }

    private BaseExpr Call()
    {
        var calledExpr = GetNewExpression();

        if (calledExpr is LiteralExpr literalCalled)
        {
            if (literalCalled.Token.Type.GroupName != "symbol")
            {
                throw new SyntaxError($"Unexpected token", literalCalled.Token);
            }

            var args = new List<BaseExpr>();

            while (!Match("closedBracket"))
            {
                args.Add(GetNewExpression());
            }

            return new CallExpr(literalCalled, args);
        }
        
        if (calledExpr is SymbolExpr symbolCalled)
        {
            var args = new List<BaseExpr>();

            while (!Match("closedBracket"))
            {
                args.Add(GetNewExpression());
            }

            return new CallExpr(symbolCalled, args);
        }
        
        throw new SyntaxError("Unexpected expression type");
    }


    private BaseExpr Quote()
    {
        Advance();

        var value = QuoteValue();

        if (Peek().Type.GroupName.Equals("closedBracket"))
        {
            Advance();
        }
        else
        {
            throw new SyntaxError("Error quote");
        }

        return new QuoteExpr(value);
    }

    private BaseExpr QuoteValue()
    {
        BaseExpr quoted;

        if (Match("openedBracket"))
        {
            var args = new List<BaseExpr>();

            while (!Match("closedBracket"))
            {
                var param = GetNewExpression();
                args.Add(param);
            }

            quoted = new ListExpr(args);
        }
        else
        {
            quoted = GetNewExpression();
        }

        return quoted;
    }

    private BaseExpr Define()
    {
        Advance();
        var variable = Advance();

        if (!variable.Type.GroupName.Equals("symbol"))
        {
            throw new SyntaxError($"Unexpected function name", variable);
        }

        var expr = GetNewExpression();

        if (!Match("closedBracket"))
        {
            throw new SyntaxError( "Unexpected EOF", variable);
        }
        return new DefineExpr(variable, expr);
    }

    private BaseExpr Lambda()
    {
        Advance();

        var args = new List<Token>();
        if (Match("symbol"))
        {
            args.Add(Previous());
        }
        else
        {
            if (!Match("openedBracket"))
            {
                throw new SyntaxError("Bracket error");
            }
            while (!Match("closedBracket"))
            {
                if (!Match("symbol"))
                {
                    throw new SyntaxError($"Invalid list of argument", Peek());
                }
                if (args.Find(arg => arg.Value == Previous().Value) != null)
                {
                    throw new SyntaxError($"Duplicate identifier in argument list", Peek());
                }
                args.Add(Previous());
            }
        }
        var body = new List<BaseExpr>();
        while (!Match("closedBracket"))
        {
            body.Add(GetNewExpression());
        }
        return new LambdaExpr(args, body);
    }

    private BaseExpr If()
    {
        Advance();
        var condition = GetNewExpression();
        var trueExpr = GetNewExpression();

        BaseExpr falseExpr = null;
        if (Peek().Type.GroupName.Equals("openedBracket"))
        {
            falseExpr = GetNewExpression();
        }

        if (!Match("closedBracket"))
        {
            throw new SyntaxError("Unexpected EOF");
        }

        return new IfExpr(condition, trueExpr, falseExpr);
    }

    private BaseExpr Atom()
    {
        if (Match("symbol"))
        {
            return new SymbolExpr(Previous());
        }
        
        if (this.Match("boolean") || this.Match("string") || this.Match("number"))
        {
            return new LiteralExpr(Previous());
        }
        
        throw new SyntaxError("Unexpected EOF");
    }
    
    private Token Previous()
    {
        return (_currentIndex > 0) ? _tokens[_currentIndex - 1] : _tokens[_currentIndex];
    }

    private Token Advance()
    {
        return _tokens[_currentIndex++];
    }

    private bool Match(string groupName)
    {
        if (Peek().Type.GroupName.Equals(groupName))
        {
            _currentIndex++;
            return true;
        }
        return false;
    }

    private Token Peek()
    {
        return _tokens[_currentIndex];
    }

    private bool IsEnd()
    {
        return Peek().Type == LispTokenTypes.EOF;
    }
}