using Interpreter.Domain.Models;

namespace Interpreter.Lexer.Storage;

public static class LispTokenTypes
{
    public static List<TokenType> Constants { get; }
    public static List<TokenType> Operation { get; }
    public static List<TokenType> Keywords { get; }
    public static List<TokenType> Variables { get; }
    
    static LispTokenTypes()
    {
        Constants = new List<TokenType>()
        {
            new TokenType("int constant", "number",@"-?[0-9]*"),
            new TokenType("complex constant", "number",@"-?\d+(\.\d+)?([eE][-+]?\d+)?[+-]\d+(\.\d+)?[i]"),
            new TokenType("float constant","number", @"-?[0-9]*\.?[0-9]+(?:[eE][-+]?[0-9]+)?"),
            new TokenType("string constant","string", @"\""(?:\\.|[^\""\\])*\"""),
            new TokenType("binary constant", "number",@"#b[01]+"),
            new TokenType("octal constant", "number",@"#o[0-7]+"),
            new TokenType("hexadecimal constant", "number",@"#x[0-9A-Fa-f]+"),
            new TokenType("char constant", "string",@"#\\[a-zA-Z1-9]"),

            new TokenType("true constant", "boolean",@"#t"),
            new TokenType("false constant", "boolean",@"#f"),
        };

        Operation = new List<TokenType>()
        {
            new TokenType("opened \"(\"", "openedBracket",@"\("),
            new TokenType("closed \")\"", "closedBracket",@"\)"),
            new TokenType("arithmetic operation", "symbol",@"\+"),
            new TokenType("arithmetic operation", "symbol",@"\-"),
            new TokenType("arithmetic operation", "symbol",@"\*"),
            new TokenType("arithmetic operation", "symbol",@"\/"),
            new TokenType("arithmetic operation", "symbol",@"\="),
            new TokenType("arithmetic operation", "symbol",@"\<"),
            new TokenType("arithmetic operation", "symbol",@"\>"),
            new TokenType("arithmetic operation", "symbol",@"\<\="),
            new TokenType("arithmetic operation", "symbol",@"\>\="),
        };
        
        Keywords = new List<TokenType>()
        {
            new TokenType("key word", "symbol", @"import"),
            new TokenType("key word", "symbol",@"if"),
            new TokenType("key word", "symbol",@"else"),
            new TokenType("key word", "symbol",@"define"),
            new TokenType("key word", "symbol",@"display"),
            new TokenType("key word", "symbol",@"case-lambda"),
            new TokenType("key word", "symbol",@"lambda"),
            new TokenType("key word", "symbol",@"begin"),
            new TokenType("key word", "symbol",@"letrec"),
            new TokenType("key word", "symbol",@"let"),
            new TokenType("key word", "symbol",@"when"),
            new TokenType("key word", "symbol",@"unless"),
            new TokenType("key word", "symbol",@"cond"),
            new TokenType("key word", "symbol",@"#\\newline"),
            new TokenType("key word", "symbol",@"#\\space"),
            new TokenType("key word", "symbol",@"#\\tab"),
            new TokenType("key word", "symbol",@"newline"),

            new TokenType("key word", "symbol",@"not"),
            new TokenType("key word", "symbol",@"and"),
            new TokenType("key word", "symbol",@"or"),

            new TokenType("key word", "symbol",@"quote"),
            new TokenType("key word", "symbol",@"list-tail"),
            new TokenType("key word", "symbol",@"list-ref"),
            new TokenType("key word", "symbol",@"list\?"),
            new TokenType("key word", "symbol",@"list"),

            new TokenType("key word", "symbol",@"set-car!"),
            new TokenType("key word", "symbol",@"set-cdr!"),
            new TokenType("key word", "symbol",@"car"),
            new TokenType("key word", "symbol",@"cdr"),
            new TokenType("key word", "symbol",@"reverse"),
            new TokenType("key word", "symbol",@"memq"),
            new TokenType("key word", "symbol",@"member"),
            new TokenType("key word", "symbol",@"memv"),

            new TokenType("key word", "symbol",@"remainder"),
            new TokenType("key word", "symbol",@"quotient"),
            new TokenType("key word", "symbol",@"abs"),
            new TokenType("key word", "symbol",@"max"),
            new TokenType("key word", "symbol",@"min"),
            new TokenType("key word", "symbol",@"round"),
            new TokenType("key word", "symbol",@"ceiling"),
            new TokenType("key word", "symbol",@"floor"),
            new TokenType("key word", "symbol",@"truncate"),
            new TokenType("key word", "symbol",@"sqrt"),

            new TokenType("key word", "symbol",@"zero\?"),
            new TokenType("key word", "symbol",@"positive\?"),
            new TokenType("key word", "symbol",@"negative\?"),
            new TokenType("key word", "symbol",@"odd\?"),
            new TokenType("key word", "symbol",@"even\?"),
            new TokenType("key word", "symbol",@"boolean\?"),
            new TokenType("key word", "symbol",@"infinite\?"),
            new TokenType("key word", "symbol",@"finite\?"),
            new TokenType("key word", "symbol",@"nan\?"),
            new TokenType("key word", "symbol",@"integer\?"),
            new TokenType("key word", "symbol",@"number\?"),
            new TokenType("key word", "symbol",@"inexact\?"),
            new TokenType("key word", "symbol",@"exact\?"),
            new TokenType("key word", "symbol",@"real\?"),
            new TokenType("key word", "symbol",@"rational\?"),
            new TokenType("key word", "symbol",@"eqv\?"),

            new TokenType("key word", "symbol",@"char\?"),
            new TokenType("key word", "symbol",@"char\=\?"),
            new TokenType("key word", "symbol",@"char\<\?"),
            new TokenType("key word", "symbol",@"char\<\=\?"),
            new TokenType("key word", "symbol",@"char\>\?"),
            new TokenType("key word", "symbol",@"char\>\=\?"),
            new TokenType("key word", "symbol",@"char-ci\=\?"),
            new TokenType("key word", "symbol",@"char-ci\<\?"),
            new TokenType("key word", "symbol",@"char-ci\<\=\?"),
            new TokenType("key word", "symbol",@"char-ci\>\?"),
            new TokenType("key word", "symbol",@"char-ci\>\=\?"),

            new TokenType("key word", "symbol",@"string\?"),
            new TokenType("key word", "symbol",@"string\=\?"),
            new TokenType("key word", "symbol",@"string\<\?"),
            new TokenType("key word", "symbol",@"string\<\=\?"),
            new TokenType("key word", "symbol",@"string\>\?"),
            new TokenType("key word", "symbol",@"string\>\=\?"),
            new TokenType("key word", "symbol",@"string-ci\=\?"),
            new TokenType("key word", "symbol",@"string-ci\<\?"),
            new TokenType("key word", "symbol",@"string-ci\<\=\?"),
            new TokenType("key word", "symbol",@"string-ci\>\?"),
            new TokenType("key word", "symbol",@"string-ci\>\=\?"),

            new TokenType("key word", "symbol",@"string-length"),
            new TokenType("key word", "symbol",@"string-ref"),
            new TokenType("key word", "symbol",@"string-append"),
            new TokenType("key word", "symbol",@"substring"),
            new TokenType("key word", "symbol",@"string-copy"),
            new TokenType("key word", "symbol",@"string-upcase"),
            new TokenType("key word", "symbol",@"string-downcase"),
            new TokenType("key word", "symbol",@"string-titlecase"),
            new TokenType("key word", "symbol",@"format"),
            new TokenType("key word", "symbol",@"append"),
            new TokenType("key word", "symbol",@"str"),
        };

        Variables = new List<TokenType>()
        {
            new TokenType("identifier", "symbol",@"[A-Za-z][A-Za-z0-9_-]*"),
        };
    }

    public static List<TokenType> GetAllTokenTypes()
    {
        List<TokenType> allTokenTypes = new List<TokenType>();
        
        allTokenTypes.AddRange(Operation);
        allTokenTypes.AddRange(Constants);
        allTokenTypes.AddRange(Keywords);
        allTokenTypes.AddRange(Variables);
        
        return allTokenTypes;
    }
}