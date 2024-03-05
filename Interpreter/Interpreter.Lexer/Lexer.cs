using System.Text.RegularExpressions;
using Interpreter.Domain.Models;
using Interpreter.Domain.Tools;
using Interpreter.Lexer.Storage;

namespace Interpreter.Lexer;

public class Lexer
{
    private string _programCode;
    private ShortGuidGenerator _shortGuidGenerator;
    public List<Token> Tokens { get; }

    public Lexer(string programCode)
    {
        _programCode = programCode;

        _shortGuidGenerator = new ShortGuidGenerator(6);
        Tokens = new List<Token>();
    }

    public void Tokenization()
    {
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
                    //TODO: добавить обработку случая когда не найден тип токена
                }

                Token newToken = new Token(_shortGuidGenerator.GenerateID(), value, match.Index, tokenType);
                Tokens.Add(newToken);
            }
        }
        
        //TODO: добавить обработку все ошибок
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