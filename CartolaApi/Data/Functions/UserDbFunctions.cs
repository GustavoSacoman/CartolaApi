using CartolaApi.Utils;
using CartolaApi.Data.Models;

namespace CartolaApi.Data.Functions;

public class UserDbFunctions
{
    private readonly Hash _hash;
    private readonly AppDbContext _db;

    public UserDbFunctions(Hash hash, AppDbContext db)
    {
        _hash = hash;
        _db = db;
    }
    
    
    public bool VerifyUserExistence(string email)
    {
        var user = _db.Users.FirstOrDefault(user => user.Email == email);
        return user != null;
    }

    public void CreateUser(string email, string password, string name, string phone)
    {
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

    public void UpdateUser(string email, string? password, string? name, string? phone)
    {
        if (!VerifyUserExistence(email))
        {
            throw new Exception("User not found");
        }

        var user = _db.Users.FirstOrDefault(user => user.Email == email);
        
        if (phone != user.Phone)
        {
            throw new Exception("Phone not validadted");
        }
        
        if (password != null)
        {
            user.Password = _hash.CreateHash(password);
        }
        if (name != null)
        {
            user.Name = name;
        }
        if (phone != null)
        {
            user.Phone = phone;
        }
        _db.SaveChanges();
    }

    public List<User> GetUsers()
    {
        return _db.Users.ToList();
    }
}