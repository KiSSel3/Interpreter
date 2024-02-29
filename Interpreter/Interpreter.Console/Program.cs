using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Xml;

class Program
{
    #region ErrorCode
    //static void Main()
    //{
    //    string str = "(define f)\r\n(display f)\r\n(newline)\r\n(set! f 5)\r\n(display f)\r\n(newline)\r\n(newline)\r\n(newline)\r\n(newline)\r\n(newline)";

    //    List<Tuple<string, string>> tokens = new List<Tuple<string, string>>();
    //    string whitespacePattern = @"[\s ,]*";
    //    string specialCharsPattern = @"(~@|[\[\]{}()'`~@])";
    //    string stringPattern = @"""(?:[\\].|[^\\""])*""?";
    //    string commentPattern = @";.*";
    //    string otherPattern = @"[^\s \[\]{}()'""`~@,;]*";
    //    string combinedPattern = $"{whitespacePattern}({specialCharsPattern}|{stringPattern}|{commentPattern}|{otherPattern})";

    //    // Регулярные выражения и обработчики для различных типов токенов
    //    var tokenPatterns = new List<(string Pattern, Action<string, int, Tuple<int, int>> Handler)>
    //    {
    //        (combinedPattern, HandleToken),
    //        // Добавьте новые пары (регулярное выражение, обработчик) по мере необходимости
    //    };

    //    foreach (Match match in GetMatches(str, tokenPatterns))
    //    {
    //        string token = match.Value;
    //        if (!string.IsNullOrEmpty(token) && token[0] != ';')
    //        {
    //            int position = match.Index;
    //            Tuple<int, int> lineAndColumn = GetLineAndColumn(str, position);

    //            // Вызываем соответствующий обработчик для текущего типа токена
    //            foreach (var (pattern, handler) in tokenPatterns)
    //            {
    //                if (Regex.IsMatch(token, pattern))
    //                {
    //                    handler(token, position, lineAndColumn);
    //                    break;
    //                }
    //            }
    //        }
    //    }
    //}

    //static IEnumerable<Match> GetMatches(string input, List<(string Pattern, Action<string, int, Tuple<int, int>> Handler)> patterns)
    //{
    //    foreach (var (pattern, _) in patterns)
    //    {
    //        foreach (Match match in Regex.Matches(input, pattern))
    //        {
    //            yield return match;
    //        }
    //    }
    //}

    //static void HandleToken(string token, int position, Tuple<int, int> lineAndColumn)
    //{
    //    Console.WriteLine($"совпадение: {token}, строка: {lineAndColumn.Item1}, столбец: {lineAndColumn.Item2}");

    //    // Дополнительный код обработки токена

    //    if (Regex.IsMatch(token, @"-?\d+(\.\d+)?([eE][-+]?\d+)?[+-]\d+(\.\d+)?[i]"))
    //    {
    //        Console.WriteLine($"   Комплексное: {token}");
    //    }
    //    else if (Regex.IsMatch(token, @"-?\d+(\.\d+([eE][-+]?\d+)?)?"))
    //    {
    //        if (token.Contains('.') || token.Contains('e') || token.Contains('E'))
    //        {
    //            Console.WriteLine($"   Вещественное: {token}");
    //        }
    //        else
    //        {
    //            Console.WriteLine($"   Целое: {token}");
    //        }
    //    }
    //    else if (Regex.IsMatch(token, @"#b[01]+"))
    //    {
    //        Console.WriteLine($"   Двоичное: {token}");
    //    }
    //    else if (Regex.IsMatch(token, @"#o[0-7]+"))
    //    {
    //        Console.WriteLine($"   Восьмеричное: {token}");
    //    }
    //    else if (Regex.IsMatch(token, @"#x[0-9a-fA-F]+", RegexOptions.IgnoreCase))
    //    {
    //        Console.WriteLine($"   Шестнадцатеричное: {token}");
    //    }
    //    else if (Regex.IsMatch(token, @"-?\d+/\d+"))
    //    {
    //        Console.WriteLine($"   Рациональное: {token}");
    //    }
    //}
    //static Tuple<int, int> GetLineAndColumn(string str, int position)
    //{
    //    int line = 1;
    //    int column = 1;

    //    for (int i = 0; i < position; i++)
    //    {
    //        if (str[i] == '\n')
    //        {
    //            line++;
    //            column = 1;
    //        }
    //        else
    //        {
    //            column++;
    //        }
    //    }

    //    return new Tuple<int, int>(line, column);
    //}
    #endregion

    static void Main()
    {
        string str = "'sfdfsdf `dfgdfgdfg ";

        List<string> tokens = new List<string>();

        string specialCharsPattern = @"(~@|[\[\]{}(),~@])";
        string stringPattern = @"""(?:[\\].|[^\\""])*""?";
        string commentPattern = @";.*";
        string otherPattern = @"[^\s \[\]{}()""~@;]*";
        string combinedPattern = $"({specialCharsPattern}|{stringPattern}|{commentPattern}|{otherPattern})";

        Regex regex = new Regex(combinedPattern);
        foreach (Match match in regex.Matches(str))
        {
            string token = match.Groups[1].Value;
            if (!string.IsNullOrEmpty(token) && token[0] != ';')
            {
                int position = match.Index;
                Tuple<int, int> lineAndColumn = GetLineAndColumn(str, position);
                Console.WriteLine($"match: {token} --> line: {lineAndColumn.Item1} --> column: {lineAndColumn.Item2}");
                tokens.Add(token);
            }
        }
    }
    static Tuple<int, int> GetLineAndColumn(string str, int position)
    {
        int line = 1;
        int column = 1;

        for (int i = 0; i < position; i++)
        {
            if (str[i] == '\n')
            {
                line++;
                column = 1;
            }
            else
            {
                column++;
            }
        }

        return new Tuple<int, int>(line, column);
    }
}