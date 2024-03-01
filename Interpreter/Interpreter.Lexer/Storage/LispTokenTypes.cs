using Interpreter.Domain.Models;

namespace Interpreter.Lexer.Storage;

public static class LispTokenTypes
{
    public static List<TokenType> Constants { get; }
    public static List<TokenType> Operation { get; }
    public static List<TokenType> Keywords { get; }
    public static List<TokenType> Variables { get; }
    public static List<TokenType> SpecialSymbols { get; }
    
    static LispTokenTypes()
    {
        Constants = new List<TokenType>()
        {
            new TokenType("int constant", @"-?[0-9]*"),
            new TokenType("complex constant", @"-?\d+(\.\d+)?([eE][-+]?\d+)?[+-]\d+(\.\d+)?[i]"),
            new TokenType("float constant", @"-?[0-9]*\.?[0-9]+(?:[eE][-+]?[0-9]+)?"),
            new TokenType("string constant", @"\""(?:\\.|[^\""\\])*\"""),
            new TokenType("binary constant", @"#b[01]+"),
            new TokenType("octal constant", @"#o[0-7]+"),
            new TokenType("hexadecimal constant", @"#x[0-9A-Fa-f]+"),
            new TokenType("char constant", @"#\\[a-zA-Z]"),

            new TokenType("true constant", @"#t"),
            new TokenType("false constant", @"#f"),
        };

        Operation = new List<TokenType>()
        {
            new TokenType("opened \"(\"", @"\("),
            new TokenType("closed \")\"", @"\)"),
            new TokenType("arithmetic operation", @"\+"),
            new TokenType("arithmetic operation", @"\-"),
            new TokenType("arithmetic operation", @"\*"),
            new TokenType("arithmetic operation", @"\/"),
            new TokenType("arithmetic operation", @"\="),
            new TokenType("arithmetic operation", @"\<"),
            new TokenType("arithmetic operation", @"\>"),
            new TokenType("arithmetic operation", @"\<\="),
            new TokenType("arithmetic operation", @"\>\="),
        };

        Keywords = new List<TokenType>()
        {
            new TokenType("key word", @"import"),
            new TokenType("key word", @"if"),
            new TokenType("key word", @"else"),
            new TokenType("key word", @"define"),
            new TokenType("key word", @"display"),
            new TokenType("key word", @"case-lambda"),
            new TokenType("key word", @"lambda"),
            new TokenType("key word", @"begin"),
            new TokenType("key word", @"letrec"),
            new TokenType("key word", @"let"),
            new TokenType("key word", @"when"),
            new TokenType("key word", @"unless"),
            new TokenType("key word", @"cond"),
            new TokenType("key word", @"#\\newline"),
            new TokenType("key word", @"#\\space"),
            new TokenType("key word", @"#\\tab"),
            new TokenType("key word", @"newline"),

            new TokenType("key word", @"not"),
            new TokenType("key word", @"and"),
            new TokenType("key word", @"or"),

            new TokenType("key word", @"quote"),
            new TokenType("key word", @"list-tail"),
            new TokenType("key word", @"list-ref"),
            new TokenType("key word", @"list\?"),
            new TokenType("key word", @"list"),

            new TokenType("key word", @"set-car!"),
            new TokenType("key word", @"set-cdr!"),
            new TokenType("key word", @"car"),
            new TokenType("key word", @"cdr"),
            new TokenType("key word", @"reverse"),
            new TokenType("key word", @"memq"),
            new TokenType("key word", @"member"),
            new TokenType("key word", @"memv"),

            new TokenType("key word", @"remainder"),
            new TokenType("key word", @"quotient"),
            new TokenType("key word", @"abs"),
            new TokenType("key word", @"max"),
            new TokenType("key word", @"min"),
            new TokenType("key word", @"round"),
            new TokenType("key word", @"ceiling"),
            new TokenType("key word", @"floor"),
            new TokenType("key word", @"truncate"),
            new TokenType("key word", @"sqrt"),

            new TokenType("key word", @"zero\?"),
            new TokenType("key word", @"positive\?"),
            new TokenType("key word", @"negative\?"),
            new TokenType("key word", @"odd\?"),
            new TokenType("key word", @"even\?"),
            new TokenType("key word", @"boolean\?"),
            new TokenType("key word", @"infinite\?"),
            new TokenType("key word", @"finite\?"),
            new TokenType("key word", @"nan\?"),
            new TokenType("key word", @"integer\?"),
            new TokenType("key word", @"number\?"),
            new TokenType("key word", @"inexact\?"),
            new TokenType("key word", @"exact\?"),
            new TokenType("key word", @"real\?"),
            new TokenType("key word", @"rational\?"),
            new TokenType("key word", @"eqv\?"),

            new TokenType("key word", @"char\?"),
            new TokenType("key word", @"char\=\?"),
            new TokenType("key word", @"char\<\?"),
            new TokenType("key word", @"char\<\=\?"),
            new TokenType("key word", @"char\>\?"),
            new TokenType("key word", @"char\>\=\?"),
            new TokenType("key word", @"char-ci\=\?"),
            new TokenType("key word", @"char-ci\<\?"),
            new TokenType("key word", @"char-ci\<\=\?"),
            new TokenType("key word", @"char-ci\>\?"),
            new TokenType("key word", @"char-ci\>\=\?"),

            new TokenType("key word", @"string\?"),
            new TokenType("key word", @"string\=\?"),
            new TokenType("key word", @"string\<\?"),
            new TokenType("key word", @"string\<\=\?"),
            new TokenType("key word", @"string\>\?"),
            new TokenType("key word", @"string\>\=\?"),
            new TokenType("key word", @"string-ci\=\?"),
            new TokenType("key word", @"string-ci\<\?"),
            new TokenType("key word", @"string-ci\<\=\?"),
            new TokenType("key word", @"string-ci\>\?"),
            new TokenType("key word", @"string-ci\>\=\?"),

            new TokenType("key word", @"string-length"),
            new TokenType("key word", @"string-ref"),
            new TokenType("key word", @"string-append"),
            new TokenType("key word", @"substring"),
            new TokenType("key word", @"string-copy"),
            new TokenType("key word", @"string-upcase"),
            new TokenType("key word", @"string-downcase"),
            new TokenType("key word", @"string-titlecase"),
            new TokenType("key word", @"format"),
            new TokenType("key word", @"append"),
            new TokenType("key word", @"str"),
        };

        Variables = new List<TokenType>()
        {
            new TokenType("identifier", @"[A-Za-z][A-Za-z0-9_-]*"),
        };
        
        SpecialSymbols = new List<TokenType>()
        {
            new TokenType("special symbol", @"#"),
            new TokenType("special symbol", @","),
            new TokenType("special symbol", @"'"),
            new TokenType("special symbol", @"`"),
        };
    }

    public static List<TokenType> GetAllTokenTypes()
    {
        List<TokenType> allTokenTypes = new List<TokenType>();
        
        allTokenTypes.AddRange(Operation);
        allTokenTypes.AddRange(Constants);
        allTokenTypes.AddRange(Keywords);
        allTokenTypes.AddRange(SpecialSymbols);
        allTokenTypes.AddRange(Variables);
        
        return allTokenTypes;
    }
}