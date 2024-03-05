namespace Interpreter.Domain.Tools;

public class ShortGuidGenerator
{
    private readonly int _length;

    public ShortGuidGenerator(int length)
    {
        if (length is < 1 or > 32)
            throw new Exception("The length of the generated id should be from 1 to 32.");
            
        _length = length;
    }

    public string GenerateID()
    {
        return Guid.NewGuid().ToString("N").Substring(0, _length);
    }
}