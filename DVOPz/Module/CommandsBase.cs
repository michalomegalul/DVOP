using System.Text;

public class CommandsBase
{
    public string GenerateToken()
    {
        string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz1234567890!@#$%^&*()_+";
        Random rand = new Random();
        StringBuilder sb = new StringBuilder();

        for (int j = 0; j < 20; j++)
        {
            int index = rand.Next(chars.Length);
            sb.Append(chars[index]);
        }
        string randomString = sb.ToString();

        return sb.ToString();
    }
}