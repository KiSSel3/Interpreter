using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interpreter.Core.Models
{
    public class Token
    {
        public string Value { get; set; }
        public int Position { get; set; }
        public TokenType Type { get; set; }
        public Token(string value, int position, TokenType type) =>
            (Value, Position, Type)=(value, position, type);
    }
}
