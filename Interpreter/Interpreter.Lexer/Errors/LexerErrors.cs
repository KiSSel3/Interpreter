using System.Text.RegularExpressions;
using Interpreter.Domain.Models;

namespace Interpreter.Lexer.Errors;

public class LexerErrors
{
    
    //TODO: переделать возврат ошибки
    public Tuple<Token, string>? CheckBrackets(List<Token> tokens)
    {
        Stack<Token> stack = new Stack<Token>();

        foreach (Token item in tokens)
        {
            if (item.Value == "(")
            {
                stack.Push(item);
            }
            else if(item.Value == ")")
            {
                if (stack.Count == 0 || stack.Pop().Value != "(")
                {
                    return new Tuple<Token, string>(item, "Error in bracketed sequence");
                }
            }
        }

        if (stack.Count != 0)
            return new Tuple<Token, string>(stack.Peek(), "Error in bracketed sequence");

        return null;
    }

    public TokenError CheckSharpError(string value, int position)
    {
        if (Regex.Match(value, @"#b").Success)
        {
            return new TokenError(value, position, "A number in binary can only consist of 0 and 1");
        }

        if (Regex.Match(value, @"#o").Success)
        {
            return new TokenError(value, position, "A number in octal form can only consist of the digits 1-7");
        }

        if (Regex.Match(value, @"#\\").Success)
        {
            return new TokenError(value, position, "'#\\' can only be followed by a single character");
        }

        return new TokenError(value, position, "Incorrect use of the '#' character");
    }

    public TokenError CheckFirstNumberError(string value, int position)
    {
        int countDigit = value.Count(char.IsDigit);
        int countLetter = value.Length - countDigit;
        
        if (countDigit > countLetter)
        {
            return new TokenError(value, position, "Non-existing type, number must not contain letters");
        }
        
        return new TokenError(value, position, "A variable cannot start with a digit");
    }
}