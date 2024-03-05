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
        
        lexer.Tokenization();
        
        Output.PrintTokensTable(lexer.Tokens);
    }

    private void ReadFile(string fileName)
    {
        using (StreamReader reader = new StreamReader($"{_basePath}\\{fileName}"))
        {
            _pragramCode = reader.ReadToEnd();
        }
    }
}