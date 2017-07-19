using System.Security.Cryptography;
using System.Text.RegularExpressions;

public class Utils
{
    static Regex ValidEmailRegex = CreateValidEmailRegex();

    // hash a password with a random list of bytes
    public static byte[] GenerateSaltedHash(byte[] plainText, byte[] salt)
    {
        HashAlgorithm algorithm = new SHA256Managed();

        byte[] plainTextWithSaltBytes =
          new byte[plainText.Length + salt.Length];

        for (int i = 0; i < plainText.Length; i++)
        {
            plainTextWithSaltBytes[i] = plainText[i];
        }
        for (int i = 0; i < salt.Length; i++)
        {
            plainTextWithSaltBytes[plainText.Length + i] = salt[i];
        }

        return algorithm.ComputeHash(plainTextWithSaltBytes);
    }

    // check email format
    public static bool CheckEmail(string email)
    {
        bool isValid = ValidEmailRegex.IsMatch(email);

        return isValid;
    }

    private static Regex CreateValidEmailRegex()
    {
        string validEmailPattern = @"^(?!\.)(""([^""\r\\]|\\[""\r\\])*""|"
            + @"([-a-z0-9!#$%&'*+/=?^_`{|}~]|(?<!\.)\.)*)(?<!\.)"
            + @"@[a-z0-9][\w\.-]*[a-z0-9]\.[a-z][a-z\.]*[a-z]$";

        return new Regex(validEmailPattern, RegexOptions.IgnoreCase);
    }
}