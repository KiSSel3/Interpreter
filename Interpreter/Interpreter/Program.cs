internal class Program
{
    public static void Main(string[] args)
    {
        try
        {
            Interpreter.Interpreter interpreter = new Interpreter.Interpreter();
        
            interpreter.InterpretationCode("Test2.txt");
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
    }
}