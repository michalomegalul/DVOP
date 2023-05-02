using DVOPz;
using System.Text;
using DVOPz.Module.Math;
using DVOPz.Module;

public class Commands : CommandsBase
{
    private User _currentUser;
    private readonly InputValidation2 _inputValidation2;
    private readonly List<Rectangle> _rectangles;

    public Commands(InputValidation2 inputValidation2)
    {
        _inputValidation2 = inputValidation2;
        _rectangles = new List<Rectangle>();

    }
    public User AUTH(string email)
    {
        if (_inputValidation2.EmailValidation(email))
        {
            var text = File.ReadAllLines(@"./Userdata");
            for (int i = 0; i < text.Length; i++)
            {
                var userEmail = text[i].Split(';')[0];
                var userToken = text[i].Split(';')[1];
                if (userEmail == email)
                {
                    var user = new User()
                    {
                        email = userEmail,
                        Token = userToken
                    };
                    _currentUser = user;
                    return _currentUser;
                }
            }
            var newUser = new User()
            {
                email = email,
                Token = GenerateToken()
            };
            File.AppendAllText(@"./Userdata", $"{newUser.email};{newUser.Token}\n");
            _currentUser = newUser;
            return _currentUser;
        }
        return _currentUser;
    }
    public void WHOAMI(string? email)
    {
        if (_currentUser == null)
        {
            return;
        }
        Console.WriteLine(_currentUser.email);
    }

    public void STATUS(string? email)
    {
        if (_currentUser == null)
        {
            return;
        }
        int count = 0;
        foreach (var rectangle in _rectangles)
        {
            count++;
            Console.WriteLine(count);
            Console.WriteLine($"Width: {rectangle.Width}, Height: {rectangle.Height}");
        }
    }

    public void ADD(string? input)
    {
        if (_currentUser == null)
        {
            return;
        }
        Console.WriteLine(input);
        string[] inputArray = input.Split(',');
        int widthInput, heightInput;
        if (inputArray.Length == 2 && int.TryParse(inputArray[0], out widthInput) && int.TryParse(inputArray[1], out heightInput))
        {
            var rectangle = new Rectangle(widthInput, heightInput);
            _rectangles.Add(rectangle);
            Console.WriteLine($"Rectangle created with width={widthInput}, height={heightInput}");
        }
    }

    public void PROCESS(string? email)
    {
        if (_currentUser == null)
        {
            return;
        }

        Console.WriteLine("Processing user's account...");
        Console.WriteLine($"Number of rectangles: {_rectangles.Count}");
        int count =0;
        foreach (var rectangle in _rectangles)
        {
            count++;
            rectangle.CalculateArea();
            Console.WriteLine(count);
            Console.WriteLine($"Width: {rectangle.Width}, Height: {rectangle.Height}, Area: {rectangle.Area}");
        }
    }
    public void LOGOUT(string? email)
    {
        _currentUser = null;
        Console.WriteLine("Logged out");
    }
}
