using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVOPz.Module;

public class InputValidation
{
    public bool EmailValidation(string email)
    {
        try
        {
            var addr = new System.Net.Mail.MailAddress(email);
            return addr.Host == "ssps.cz";
        }
        catch
        {
            return false;
        }
    }
    public void SynError()
    {
        Console.WriteLine("SYNTAX ERROR");
    }
    public void Unauthenticated()
    {
        Console.WriteLine("UNAUTHENTICATED");
    }

}
public class InputValidation2
{
    //https://en.wikipedia.org/wiki/Email_address
    public bool EmailValidation(string email)
    {
        if (string.IsNullOrEmpty(email))
        {
            return false;
        }

        var parts = email.Split('@');
        if (parts.Length != 2 || parts[1] != "ssps.cz")
        {
            return false;
        }

        // Check for invalid characters
        var username = parts[0];
        var invalidChars = new char[] { ',', ';', '(', ')', '<', '>', '[', ']', ':', '\\', '\"', '\'', ' ', '\t' };
        if (username.IndexOfAny(invalidChars) >= 0)
        {
            return false;
        }
        return true;
    }
}


