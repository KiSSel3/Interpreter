using ConsoleTables;
using Interpreter.Domain.Models;
using Interpreter.Lexer.Storage;
using Interpreter.Parser.Expressions;

namespace Interpreter.Tools;

public static class Output
{
    public static void PrintTokensTable(string programCode, List<Token> tokens)
    {
        Console.WriteLine("\nTokens: \n");
        
        string[] lines = programCode.Split(new[] { Environment.NewLine }, StringSplitOptions.None);
        
        IEnumerable<Token> uniqueTokens = tokens.DistinctBy(t => t.Value);
        
        var table = new ConsoleTable("Id", "Value", "Type" /*, "Position"*/);
        foreach (Token item in uniqueTokens /*tokens*/)
        {
            if(item.Value.Equals("eof"))
                continue;
            
            Tuple<int, int> normalPosition = GetRowAndColumnByPosition(lines, item.Position);
            
            table.AddRow(item.Id, item.Value, item.Type?.Value ?? "undefined" /*, $"{normalPosition.Item1}:{normalPosition.Item2}"*/);
        }

        Console.WriteLine(table.ToString());
    }

    public static void PrintTokenErrors(string programCode, List<TokenError> errors)
    {
        Console.WriteLine("\nLexical errors: \n");
        
        string[] lines = programCode.Split(new[] { Environment.NewLine }, StringSplitOptions.None);
        
        foreach (TokenError item in errors)
        {
            Tuple<int, int> normalPosition = GetRowAndColumnByPosition(lines, item.Position);

            Console.WriteLine($"\n{item.Description} at position ({normalPosition.Item1},{normalPosition.Item2}):");

            Console.Write($"\t{lines[normalPosition.Item1-1].Substring(0, normalPosition.Item2 - 1)}");
            
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write($"{lines[normalPosition.Item1 - 1].Substring(normalPosition.Item2 - 1, item.Value.Length)}");
            Console.ResetColor();

            Console.WriteLine($"{lines[normalPosition.Item1 - 1].Substring(normalPosition.Item2 + item.Value.Length - 1)}");

            Console.WriteLine($"\t{new string(' ', normalPosition.Item2 - 1)}^");
            
            //Console.WriteLine($"\t{new string(' ', normalPosition.Item2 - 1)}|\n");
        }
        
    }
    
    public static void PrintSyntaxError(string programCode, string errorMessage, Token token)
    {
        Console.WriteLine("\nSyntax errors: \n");
        
        string[] lines = programCode.Split(new[] { Environment.NewLine }, StringSplitOptions.None);

        if (token is not null)
        {
            Tuple<int, int> normalPosition = GetRowAndColumnByPosition(lines, token.Position);

            Console.WriteLine($"\n{errorMessage} at position ({normalPosition.Item1},{normalPosition.Item2}):");

            Console.Write($"\t{lines[normalPosition.Item1 - 1].Substring(0, normalPosition.Item2 - 1)}");

            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write($"{lines[normalPosition.Item1 - 1].Substring(normalPosition.Item2 - 1, token.Value.Length)}");
            Console.ResetColor();

            Console.WriteLine(
                $"{lines[normalPosition.Item1 - 1].Substring(normalPosition.Item2 + token.Value.Length - 1)}");

            Console.WriteLine($"\t{new string(' ', normalPosition.Item2 - 1)}^");
        }
        else
        {
            Console.WriteLine(errorMessage);
        }
    }

    public static void PrintLexemes(List<Token> tokens)
    {
        Console.WriteLine("\nLexemes: \n");
        
        foreach (Token item in tokens)
        {
            if(item.Value.Equals("eof"))
                continue;
            
            if (item.Type is not null && (item.Type.Value.Equals("identifier") || LispTokenTypes.Constants.Contains(item.Type)))
            {
                string id = tokens.First(t => t.Value.Equals(item.Value)).Id;
                
                Console.Write($"id<{id}>");
            }
            else
            {
                Console.Write(item.Value);
            }

            Console.Write(" ");
        }
    }

    private static Tuple<int, int> GetRowAndColumnByPosition(string[] lines, int position)
    {
        int lineStartPosition = 0;
        for (int i = 0; i < lines.Length; ++i)
        {
            int lineEndPosition = lineStartPosition + lines[i].Length;

            if (lineStartPosition <= position && position <= lineEndPosition)
            {
                int column = position - lineStartPosition;
                return new Tuple<int, int>(i + 1, column + 1);
            }
            
            lineStartPosition = lineEndPosition + Environment.NewLine.Length;
        }
        
        throw new Exception("Token position error");
    }

    public static void PrintTree(object expr, string indent = "-> ")
    {
        Console.WriteLine("\n\nTree: \n");

        ConstructTree(expr, indent);
    }
    
    private static void ConstructTree(object expr, string indent = "-> ")
    {
        if (expr is List<BaseExpr>)
        {
            foreach (var prop in expr as List<BaseExpr>)
            {
                if (prop is DefineExpr defineExpr)
                {
                    Console.WriteLine(indent + "Define");
                    ConstructTree(defineExpr.Variable, "  " + indent);
                    ConstructTree(defineExpr.Value, "  " + indent);
                    continue;
                }
                if (prop is CallExpr callExpression)
                {
                    Console.WriteLine(indent + "Call");
                    ConstructTree(callExpression.Called, "  " + indent);
                    ConstructTree(callExpression.Args, "  " + indent);
                    continue;
                }
                if (prop is IfExpr ifExpression)
                {
                    Console.WriteLine(indent + "If");
                    ConstructTree(ifExpression.Condition, "  " + indent);
                    ConstructTree(ifExpression.TrueExpr, "  " + indent);
                    ConstructTree(ifExpression.FalseExpr, "  " + indent);
                    continue;
                }
                if (prop is LambdaExpr lambdaExpression)
                {
                    Console.WriteLine(indent + "Lambda");
                    ConstructTree(lambdaExpression.Args, "  " + indent);
                    ConstructTree(lambdaExpression.Body, "  " + indent);
                    continue;
                }
                if (prop is QuoteExpr quoteExpression)
                {
                    Console.WriteLine(indent + "Quote");
                    ConstructTree(quoteExpression.Value, "  " + indent);
                    continue;
                }
                if (prop is ListExpr listExpression)
                {
                    Console.WriteLine(indent + "List");
                    ConstructTree(listExpression.Items, "  " + indent);
                    continue;
                }
                if (prop is LiteralExpr literalExpression)
                {
                    Console.WriteLine(indent + literalExpression.Token.Value);
                    continue;
                }
                if (prop is SymbolExpr symbolExpression)
                {
                    Console.WriteLine(indent + symbolExpression.Token.Value);
                    continue;
                }
                ConstructTree(prop, "  " + indent);
            }
        }
        if (expr is CallExpr callExpr)
        {
            Console.WriteLine(indent + "Call");
            ConstructTree(callExpr.Called, "  " + indent);
            ConstructTree(callExpr.Args, "  " + indent);
        }
        if (expr is LiteralExpr literalExpr)
        {
            Console.WriteLine(indent + literalExpr.Token.Value);
        }
        if (expr is IfExpr ifExpr)
        {
            Console.WriteLine(indent + "If");
            ConstructTree(ifExpr.Condition, "  " + indent);
            ConstructTree(ifExpr.TrueExpr, "  " + indent);
            ConstructTree(ifExpr.FalseExpr, "  " + indent);
        }
        if (expr is LambdaExpr lambdaExpr)
        {
            Console.WriteLine(indent + "Lambda");
            ConstructTree(lambdaExpr.Args, "  " + indent);
            ConstructTree(lambdaExpr.Body, "  " + indent);
        }
        if (expr is QuoteExpr quoteExpr)
        {
            Console.WriteLine(indent + "Quote");
            ConstructTree(quoteExpr.Value, "  " + indent);
        }
        if (expr is ListExpr listExpr)
        {
            Console.WriteLine(indent + "List");
            ConstructTree(listExpr.Items, "  " + indent);
        }
        if (expr is Token token)
        {
            Console.WriteLine(indent + token.Value);
        }
        if (expr is SymbolExpr symbolExpr)
        {
            Console.WriteLine(indent + symbolExpr.Token.Value);
        }
    }
}