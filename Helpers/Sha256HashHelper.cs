using System.Security.Cryptography;
using System.Text;

namespace WebApplicationOperaLublin.Helpers;

public static class Sha256HashHelper
{
    private static readonly SHA256 HashFunction = SHA256.Create();

    public static string GetHash(string input)
    {
        byte[] data = HashFunction.ComputeHash(Encoding.UTF8.GetBytes(input));

        var sBuilder = new StringBuilder();
        for (int i = 0; i < data.Length; i++)
        {
            sBuilder.Append(data[i].ToString("x2"));
        }
        
        return sBuilder.ToString();
    }
}
