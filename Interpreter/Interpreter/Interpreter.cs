using Interpreter.Domain.Errors;
using Interpreter.Parser.Expressions;
using Interpreter.Tools;
using Microsoft.Extensions.Configuration;

namespace Interpreter;

public class Interpreter
{
    private readonly string _basePath;
    private string _pragramCode;

public Interpreter()
{
    IConfiguration configuration = new ConfigurationBuilder()
        .SetBasePath(AppDomain.CurrentDomain.BaseDirectory + "\\Properties")
        .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
        .Build();

    _basePath = configuration.GetSection("BasePath").Value;
}

    
    public void InterpretationCode(string fileName)
    {
        ReadFile(fileName);
        Lexer.Lexer lexer = new Lexer.Lexer(_pragramCode);

        bool isSuccessful = lexer.Tokenization();
        if (!isSuccessful)
        {
            Output.PrintTokenErrors(_pragramCode, lexer.ErrorList);
            //return;
        }
        
        Output.PrintTokensTable(_pragramCode, lexer.Tokens);
        
        Output.PrintLexemes(lexer.Tokens);

        try
        {
            Parser.Parser parser = new Parser.Parser(lexer.Tokens);
            List<BaseExpr> expressions = parser.Parse();
            
            Output.PrintTree(expressions);
        }
        catch (SyntaxError ex)
        {
            Output.PrintSyntaxError(_pragramCode, ex.Message, ex.Token);
            return;
        }
    }

    private void ReadFile(string fileName)
    {
        using (StreamReader reader = new StreamReader($"{_basePath}\\{fileName}"))
        {
            _pragramCode = reader.ReadToEnd();
        }
    }
}