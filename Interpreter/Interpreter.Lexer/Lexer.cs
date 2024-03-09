using System.Text.RegularExpressions;
using Interpreter.Domain.Models;
using Interpreter.Domain.Tools;
using Interpreter.Lexer.Errors;
using Interpreter.Lexer.Storage;

namespace Interpreter.Lexer;

public class Lexer
{
    private string _programCode;
    private ShortGuidGenerator _shortGuidGenerator;
    private LexerErrors _lexerErrors;
    public List<Token> Tokens { get; }
    public List<TokenError> ErrorList { get; }

    public Lexer(string programCode)
    {
        _programCode = programCode;

        _shortGuidGenerator = new ShortGuidGenerator(6);
        _lexerErrors = new LexerErrors();
        
        Tokens = new List<Token>();
        ErrorList = new List<TokenError>();
    }

    public bool Tokenization()
    {
        bool isSuccessful = true;
        
        string pattern = @"(~@|[\[\]{}()~@]|""(?:[\\].|[^\\""])*""?|;.*|[^\s \[\]{}()""~@;]*)";
        
        Regex regex = new Regex(pattern);
        foreach (Match match in regex.Matches(_programCode))
        {
            string value = match.Groups[1].Value;
            if (!string.IsNullOrEmpty(value) && value[0] != ';')
            {
                TokenType? tokenType = GetTokenType(value);
                if (tokenType is null)
                {
                    isSuccessful = false;
                    
                    if (value[0] == '#')
                    {
                       TokenError newError = _lexerErrors.CheckSharpError(value, match.Index);
                       ErrorList.Add(newError);
                    }
                    else if(char.IsDigit(value[0]))
                    {
                        TokenError newError = _lexerErrors.CheckFirstNumberError(value, match.Index);
                        ErrorList.Add(newError);
                    }
                    else
                    {
                        throw new Exception("There was an unhandled error with the token type");
                    }
                }
                
                Token newToken = new Token(_shortGuidGenerator.GenerateID(), value, match.Index, tokenType);
                Tokens.Add(newToken);
            }
        }

        return isSuccessful;
    }
    
    private TokenType? GetTokenType(string value)
    {
        foreach (var item in LispTokenTypes.GetAllTokenTypes())
        {
            if (Regex.Match(value, $"^{item.Regex}$").Success)
            {
                return item;
            }
        }
        
        return null;
    }
}