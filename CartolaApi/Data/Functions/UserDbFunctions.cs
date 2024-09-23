using CartolaApi.Utils;
using CartolaApi.Data.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;

namespace CartolaApi.Data.Functions;

public class UserDbFunctions
{
    private readonly Hash _hash;
    private readonly AppDbContext _db;

    public UserDbFunctions()
    {
        var configuration = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json")
            .Build();

        var connectionString = configuration.GetConnectionString("CartolaConnection");
        if (connectionString == null)
        {
            throw new Exception("Database connection string not found");
        }
        
        var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
        optionsBuilder.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
        _db = new AppDbContext(optionsBuilder.Options);

        _hash = new Hash();
    }

    public bool VerifyUserExistence(string email)
    {
        var user = _db.Users.AsEnumerable().FirstOrDefault(user => user.Email.Equals(email, StringComparison.OrdinalIgnoreCase));
        Console.Write(user?.Name);
        Console.Write(email);
        return user != null;
    }

    public void CreateUser(string email, string password, string name, string phone)
    {
        Console.WriteLine("Creating user...");
        Console.WriteLine(email);
        Console.WriteLine(password);
        Console.WriteLine(name);
        Console.WriteLine(phone);
        if (VerifyUserExistence(email))
        {
            throw new Exception("User already exists");
        }

        var user = new User()
        {
            Email = email,
            Password = _hash.CreateHash(password),
            Name = name,
            Phone = phone
        };

        _db.Users.Add(user);
        _db.SaveChanges();
    }

    public void DeleteUser(string email)
    {
        if (!VerifyUserExistence(email))
        {
            throw new Exception("User not found");
        }

        var user = _db.Users.FirstOrDefault(user => user.Email == email);
        _db.Users.Remove(user);
        _db.SaveChanges();
    }

    public void UpdateUser(string email, string? password, string? name, string phone)
    {
        if (!VerifyUserExistence(email))
        {
            throw new Exception("User not found");
        }

        var user = _db.Users.FirstOrDefault(user => user.Email == email);
        if (user == null)
        {
            throw new Exception("User not found");
        }
        
        if (phone != user.Phone)
        {
            throw new Exception("Phone not validated");
        }
        
        if (password != null)
        {
            user.Password = _hash.CreateHash(password);
        }
        if (name != null)
        {
            user.Name = name;
        }
        _db.SaveChanges();
    }

    public List<User> GetUsers()
    {
        return _db.Users.ToList();
    }
}