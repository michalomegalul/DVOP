using DVOPz.Module;
namespace DVOPz;
public class Program
{
    public static void Main(string[] args)
    {
        var inputValidation2 = new InputValidation2();
        var inputValidation = new InputValidation();
        var commands = new Commands(inputValidation2,inputValidation);
        var app = new App(inputValidation, commands);
        app.Application();
        
    }
}
