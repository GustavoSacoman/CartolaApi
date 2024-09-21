using System.Security.Cryptography;
using System.Text;
using Microsoft.Extensions.Configuration;

namespace CartolaApi.Utils;

public class Hash
{
    private readonly string _saltKey;

    public Hash(IConfiguration configuration)
    {
        _saltKey = configuration["SALT_KEY"];
    }

    public string CreateHash(string password)
    {
        var data = Encoding.ASCII.GetBytes(password + _saltKey);
        using (var sha256 = SHA256.Create())
        {
            data = sha256.ComputeHash(data);
        }
        return Encoding.ASCII.GetString(data);
    }
}