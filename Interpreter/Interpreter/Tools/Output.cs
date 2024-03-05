using ConsoleTables;
using Interpreter.Domain.Models;

namespace Interpreter.Tools;

public static class Output
{
    //TODO: заменить отображение позици на формат строка:столбец
    public static void PrintTokensTable(List<Token> tokens)
    {
        var table = new ConsoleTable("Id", "Value", "Type", "Position");

        foreach (Token item in tokens)
        {
            table.AddRow(item.Id, item.Value, item.Type?.Value ?? "undefined", item.Position);
        }

        Console.WriteLine(table.ToString());
    }
}