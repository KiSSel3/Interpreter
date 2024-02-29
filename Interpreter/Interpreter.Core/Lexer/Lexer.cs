using Interpreter.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Interpreter.Core.Lexer
{
    public class Lexer
    {
        private string _code;
        public List<Token> Tokens { get; }
        
        public Lexer(string code) => _code = code;

        public List<Token> GetTokens()
        {
            string specialCharsPattern = @"(~@|[\[\]{}(),~@])";
            string stringPattern = @"""(?:[\\].|[^\\""])*""?";
            string commentPattern = @";.*";
            string otherPattern = @"[^\s \[\]{}()""~@;]*";

            Regex regex = new Regex($"({specialCharsPattern}|{stringPattern}|{commentPattern}|{otherPattern})");

            foreach (Match match in regex.Matches(_code))
            {
                string token = match.Groups[1].Value;

                if (!string.IsNullOrEmpty(token) && token[0] != ';')
                {
                    var currentType = GetTokenType(token);
                    int position = match.Index;

                    Tokens.Add(new Token(token, position, currentType));
                }
            }

            return Tokens;
        }
        
        private TokenType GetTokenType(string value)
        {
            foreach(var item in Storage.Storage.TokenTypes)
            {
                if (Regex.Match(value, item.Value.Regex).Success)
                {
                    return item.Value;
                }
            }

            return null;
        }
    }
}
