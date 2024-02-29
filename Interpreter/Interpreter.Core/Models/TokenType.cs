using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interpreter.Core.Models
{
    public class TokenType
    {
        public string Type { get; }
        public string Regex { get; }
        public TokenType(string type, string regex) =>
            (Type, Regex) = (type, regex);
    }
}
