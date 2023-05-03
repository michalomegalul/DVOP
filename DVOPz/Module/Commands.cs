using DVOPz;
using System.Text;
using DVOPz.Module.Math;
using DVOPz.Module;

public class Commands : CommandsBase
{
    private User _currentUser;
    private readonly InputValidation2 _inputValidation2;
    private readonly InputValidation _inputValidation;
    private readonly List<Rectangle> _rectangles;
    private readonly List<Circle> _circles;

    public Commands(InputValidation2 inputValidation2, InputValidation inputValidation)
    {
        _inputValidation2 = inputValidation2;
        _rectangles = new List<Rectangle>();
        _circles = new List<Circle>();
        _inputValidation = inputValidation;
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
            _inputValidation.Unauthenticated();
            return;
        }
        Console.WriteLine(_currentUser.email);
    }

    public void STATUS(string? email)
    {
        if (_currentUser == null)
        {
            _inputValidation.Unauthenticated();
            return;
        }
        int count = 0;
        foreach (var rectangle in _rectangles)
        {
            count++;
            Console.WriteLine(count);
            Console.WriteLine($"Width: {rectangle.Width}, Height: {rectangle.Height}");
            
        }
        foreach (var circle in _circles)
        {
            count++;
            Console.WriteLine(count);
            Console.WriteLine($"Diameter: {circle.Diameter}");
        }
        if (count == 0)
        {
            _inputValidation.SynError();
            return;
        }
    }

    public void ADD(string? input)
    {
        if (_currentUser == null)
        {
            _inputValidation.Unauthenticated();
            return;
        }

        if (input.StartsWith("RECTANGLE-AREA") || input.StartsWith("RECTANGLE-PERIMETER"))
        {
            string[] inputArray = input.Substring(input.IndexOf(' ') + 1).Split(',');
            int widthInput, heightInput;
            if (inputArray.Length == 2 && int.TryParse(inputArray[0], out widthInput) && int.TryParse(inputArray[1], out heightInput))
            {
                var rectangle = new Rectangle(widthInput, heightInput);
                _rectangles.Add(rectangle);
                Console.WriteLine($"Rectangle added with width={widthInput}, height={heightInput}");
            }
        }
        else if (input.StartsWith("CIRCLE-AREA") || input.StartsWith("CIRCLE-PERIMETER"))
        {
            string[] inputArray = input.Substring(input.IndexOf(' ') + 1).Split(',');
            double diameter;
            if (inputArray.Length == 1 && double.TryParse(inputArray[0], out diameter))
            {
                var circle = new Circle(diameter);
                _circles.Add(circle);
                Console.WriteLine($"Circle added with diameter={diameter}");
            }
        }
    }

    public void PROCESS(string? email)
    {
        if (_currentUser == null)
        {
            _inputValidation.Unauthenticated();
            return;
        }

        Console.WriteLine("Processing user's account...");
        Console.WriteLine($"Number of rectangles: {_rectangles.Count}");
        int count = 0;
        foreach (var rectangle in _rectangles)
        {
            count++;
            rectangle.CalculateArea();
            rectangle.CalculatePerimeter();
            Console.WriteLine($"Position: {count}");
            Console.WriteLine($"Width: {rectangle.Width}, Height: {rectangle.Height}, Area: {rectangle.Area}, Perimeter: {rectangle.Perimeter}");
        }
        _rectangles.Clear();

        Console.WriteLine($"Number of circles: {_circles.Count}");
        foreach (var circle in _circles)
        {
            count++;
            circle.CalculateArea();
            circle.CalculatePerimeter();
            Console.WriteLine($"Position: {count}");
            Console.WriteLine($"Diameter: {circle.Diameter}, Area: {circle.Area}, Perimeter: {circle.Perimeter}");
        }
        _circles.Clear();
    }
    public void LOGOUT(string? email)
    {
        _currentUser = null;
        Console.WriteLine("Logged out");
    }
}
