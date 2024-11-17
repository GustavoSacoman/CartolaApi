using CartolaApi.Utils;
using CartolaApi.Data.DTOs;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;

namespace CartolaApi.Data.Services;

public class UserServices
{
    private readonly Hash _hash;
    private readonly AppDbContext _db;

    public UserServices()
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
        return user != null;
    }

    public void CreateUser(User user)
    {
        if (VerifyUserExistence(user.Email))
        {
            throw new Exception("User already exists");
        }

        user.Password = _hash.CreateHash(user.Password);
        
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
        
        if (name != null)
        {
            user.Name = name;
        }
        
        if (phone != null && phone != user.Phone)
        {
            user.Phone = phone;
        }
        
        if (password != null)
        {
            user.Password = _hash.CreateHash(password);
        }
        
        _db.SaveChanges();
    }
    
    public Dictionary<string, string> Login(string email, string password)
    {
        var user = _db.Users.FirstOrDefault(user => user.Email == email && user.Password == _hash.CreateHash(password));
        if (user == null)
        {
            throw new Exception("User not found");
        }
        Dictionary<string, string> data = new Dictionary<string, string>
        {
            { "Email", user.Email },
            { "Name", user.Name },
            { "Phone", user.Phone }
        };
        
        return data;
    }

    public List<User> GetUsers()
    {
        return _db.Users.ToList();
    }
}