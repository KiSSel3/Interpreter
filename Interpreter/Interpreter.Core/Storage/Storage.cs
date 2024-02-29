using Interpreter.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interpreter.Core.Storage
{
    public static class Storage
    {
        public static Dictionary<string,TokenType> TokenTypes { get; }
        public static Dictionary<string, TokenType> KeyWords { get; }
        static Storage()
        {
            TokenTypes = new Dictionary<string, TokenType>
            {
                // constants
                ["-"] = new TokenType("arithmetic operation", @"\-"),
                ["constant \"int\""] = new TokenType("int constant", @"-?[0-9]*"),
                ["constant \"complex\""] = new TokenType("complex constant", @"-?\d+(\.\d+)?([eE][-+]?\d+)?[+-]\d+(\.\d+)?[i]"),
                ["constant \"float\""] = new TokenType("float constant", @"-?[0-9]*\.?[0-9]+(?:[eE][-+]?[0-9]+)?"),
                ["constant \"string\""] = new TokenType("string constant", @"\""(?:\\.|[^\""\\])*\"""),
                ["constant \"binary\""] = new TokenType("binary constant", @"#b[01]+"),
                ["constant \"octal\""] = new TokenType("octal constant", @"#o[0-7]+"),
                ["constant \"hexadecimal\""] = new TokenType("hexadecimal constant", @"#x[0-9A-Fa-f]+"),
                ["constant \"string\""] = new TokenType("char constant", @"#\\[a-zA-Z]"),

                ["constant \"true\""] = new TokenType("true constant", @"#t"),
                ["constant \"false\""] = new TokenType("false constant", @"#f"),

                // operations
                ["("] = new TokenType("opened \"(\"", @"\("),
                [")"] = new TokenType("closed \")\"", @"\)"),
                ["+"] = new TokenType("arithmetic operation", @"\+"),
                ["*"] = new TokenType("arithmetic operation", @"\*"),
                ["/"] = new TokenType("arithmetic operation", @"\/"),
                ["="] = new TokenType("arithmetic operation", @"\="),
                ["<"] = new TokenType("arithmetic operation", @"\<"),
                [">"] = new TokenType("arithmetic operation", @"\>"),
                ["<="] = new TokenType("arithmetic operation", @"\<\="),
                [">="] = new TokenType("arithmetic operation", @"\>\="),

                // special symbols
                ["#"] = new TokenType("special symbol", @"#"),
                [","] = new TokenType("special symbol", @","),
                ["'"] = new TokenType("special symbol", @"'"),
                ["`"] = new TokenType("special symbol", @"`"),

                ["identifier"] = new TokenType("identifier", @"[A-Za-z][A-Za-z0-9_-]*")
            };

            KeyWords = new Dictionary<string, TokenType>()
            {
                // keywords
                ["import"] = new TokenType("key word", @"import"),
                ["if"] = new TokenType("key word", @"if"),
                ["else"] = new TokenType("key word", @"else"),
                ["define"] = new TokenType("key word", @"define"),
                ["display"] = new TokenType("key word", @"display"),
                ["case-lambda"] = new TokenType("key word", @"case-lambda"),
                ["lambda"] = new TokenType("key word", @"lambda"),
                ["begin"] = new TokenType("key word", @"begin"),
                ["letrec"] = new TokenType("key word", @"letrec"),
                ["let"] = new TokenType("key word", @"let"),
                ["when"] = new TokenType("key word", @"when"),
                ["unless"] = new TokenType("key word", @"unless"),
                ["cond"] = new TokenType("key word", @"cond"),
                ["#\\newline"] = new TokenType("key word", @"#\\newline"),
                ["#\\space"] = new TokenType("key word", @"#\\space"),
                ["#\\tab"] = new TokenType("key word", @"#\\tab"),
                ["newline"] = new TokenType("key word", @"newline"),

                ["not"] = new TokenType("key word", @"not"),
                ["and"] = new TokenType("key word", @"and"),
                ["or"] = new TokenType("key word", @"or"),

                ["quote"] = new TokenType("key word", @"quote"),
                ["list-tail"] = new TokenType("key word", @"list-tail"),
                ["list-ref"] = new TokenType("key word", @"list-ref"),
                ["list?"] = new TokenType("key word", @"list\?"),
                ["list"] = new TokenType("key word", @"list"),

                // keyword functions
                ["set-car!"] = new TokenType("key word", @"set-car!"),
                ["set-cdr!"] = new TokenType("key word", @"set-cdr!"),
                ["car"] = new TokenType("key word", @"car"),
                ["cdr"] = new TokenType("key word", @"cdr"),
                ["reverse"] = new TokenType("key word", @"reverse"),
                ["memq"] = new TokenType("key word", @"memq"),
                ["member"] = new TokenType("key word", @"member"),
                ["memv"] = new TokenType("key word", @"memv"),

                ["remainder"] = new TokenType("key word", @"remainder"),
                ["quotient"] = new TokenType("key word", @"quotient"),
                ["abs"] = new TokenType("key word", @"abs"),
                ["max"] = new TokenType("key word", @"max"),
                ["min"] = new TokenType("key word", @"min"),
                ["round"] = new TokenType("key word", @"round"),
                ["ceiling"] = new TokenType("key word", @"ceiling"),
                ["floor"] = new TokenType("key word", @"floor"),
                ["truncate"] = new TokenType("key word", @"truncate"),
                ["sqrt"] = new TokenType("key word", @"sqrt"),

                ["zero?"] = new TokenType("key word", @"zero\?"),
                ["positive?"] = new TokenType("key word", @"positive\?"),
                ["negative?"] = new TokenType("key word", @"negative\?"),
                ["odd?"] = new TokenType("key word", @"odd\?"),
                ["even?"] = new TokenType("key word", @"even\?"),
                ["boolean?"] = new TokenType("key word", @"boolean\?"),
                ["infinite?"] = new TokenType("key word", @"infinite\?"),
                ["finite?"] = new TokenType("key word", @"finite\?"),
                ["nan?"] = new TokenType("key word", @"nan\?"),
                ["integer?"] = new TokenType("key word", @"integer\?"),
                ["number?"] = new TokenType("key word", @"number\?"),
                ["inexact?"] = new TokenType("key word", @"inexact\?"),
                ["exact?"] = new TokenType("key word", @"exact\?"),
                ["real?"] = new TokenType("key word", @"real\?"),
                ["rational?"] = new TokenType("key word", @"rational\?"),
                ["eqv?"] = new TokenType("key word", @"eqv\?"),

                ["char?"] = new TokenType("key word", @"char\?"),
                ["char=?"] = new TokenType("key word", @"char\=\?"),
                ["char<?"] = new TokenType("key word", @"char\<\?"),
                ["char<=?"] = new TokenType("key word", @"char\<\=\?"),
                ["char>?"] = new TokenType("key word", @"char\>\?"),
                ["char>=?"] = new TokenType("key word", @"char\>\=\?"),
                ["char-ci=?"] = new TokenType("key word", @"char-ci\=\?"),
                ["char-ci<?"] = new TokenType("key word", @"char-ci\<\?"),
                ["char-ci<=?"] = new TokenType("key word", @"char-ci\<\=\?"),
                ["char-ci>?"] = new TokenType("key word", @"char-ci\>\?"),
                ["char-ci>=?"] = new TokenType("key word", @"char-ci\>\=\?"),

                ["string?"] = new TokenType("key word", @"string\?"),
                ["string=?"] = new TokenType("key word", @"string\=\?"),
                ["string<?"] = new TokenType("key word", @"string\<\?"),
                ["string<=?"] = new TokenType("key word", @"string\<\=\?"),
                ["string>?"] = new TokenType("key word", @"string\>\?"),
                ["string>=?"] = new TokenType("key word", @"string\>\=\?"),
                ["string-ci=?"] = new TokenType("key word", @"string-ci\=\?"),
                ["string-ci<?"] = new TokenType("key word", @"string-ci\<\?"),
                ["string-ci<=?"] = new TokenType("key word", @"string-ci\<\=\?"),
                ["string-ci>?"] = new TokenType("key word", @"string-ci\>\?"),
                ["string-ci>=?"] = new TokenType("key word", @"string-ci\>\=\?"),

                ["string-length"] = new TokenType("key word", @"string-length"),
                ["string-ref"] = new TokenType("key word", @"string-ref"),
                ["string-append"] = new TokenType("key word", @"string-append"),
                ["substring"] = new TokenType("key word", @"substring"),
                ["string-copy"] = new TokenType("key word", @"string-copy"),
                ["string-upcase"] = new TokenType("key word", @"string-upcase"),
                ["string-downcase"] = new TokenType("key word", @"string-downcase"),
                ["string-titlecase"] = new TokenType("key word", @"string-titlecase"),
                ["format"] = new TokenType("key word", @"format"),
                ["append"] = new TokenType("key word", @"append"),
                ["str"] = new TokenType("key word", @"str"),
            };
        }
    }
}
