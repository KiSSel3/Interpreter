using ConsoleTables;
using Interpreter.Domain.Models;
using Interpreter.Lexer.Storage;

namespace Interpreter.Tools;

public static class Output
{
    public static void PrintTokensTable(string programCode, List<Token> tokens)
    {
        string[] lines = programCode.Split(new[] { Environment.NewLine }, StringSplitOptions.None);
        
        IEnumerable<Token> uniqueTokens = tokens.DistinctBy(t => t.Value);
        
        var table = new ConsoleTable("Id", "Value", "Type" /*, "Position"*/);
        foreach (Token item in uniqueTokens /*tokens*/)
        {
            Tuple<int, int> normalPosition = GetRowAndColumnByPosition(lines, item.Position);
            
            table.AddRow(item.Id, item.Value, item.Type?.Value ?? "undefined" /*, $"{normalPosition.Item1}:{normalPosition.Item2}"*/);
        }

        Console.WriteLine(table.ToString());
    }

    public static void PrintTokenErrors(string programCode, List<TokenError> errors)
    {
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

    public static void PrintLexemes(List<Token> tokens)
    {
        foreach (Token item in tokens)
        {
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
}