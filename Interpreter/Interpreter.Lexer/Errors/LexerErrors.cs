using System.Text.RegularExpressions;
using Interpreter.Domain.Models;

namespace Interpreter.Lexer.Errors;

public class LexerErrors
{
    
    //TODO: переделать возврат ошибки
    public TokenError? CheckBrackets(List<Token> tokens)
    {
        if (tokens.Count != 0 && !tokens[0].Value.Equals("("))
        {
            return new TokenError("(", 0, "Absent '('");
        }
        
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
                    return new TokenError(item.Value, item.Position, "Error in bracketed sequence");
                }
            }
        }

        if (stack.Count != 0)
        {
            return new TokenError(stack.Peek().Value, stack.Peek().Position, "Error in bracketed sequence");
        }

        if (tokens.Count != 0 && !tokens[tokens.Count - 1].Value.Equals(")"))
        {
            return new TokenError("(", tokens[tokens.Count - 1].Position, "Absent ')'");
        }

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