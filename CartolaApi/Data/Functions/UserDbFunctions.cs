using CartolaApi.Models;
using CartolaApi.Utils;
using Microsoft.Extensions.Configuration;


namespace CartolaApi.Data.Functions;

public class UserDbFunctions
{
    private readonly IConfiguration _configuration;

    public UserDbFunctions(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public bool VerifyUserExistence(
        string email,
        AppDbContext db
        )
    {
        var user = db.Users.FirstOrDefault(user => user.Email == email);
        if (user is null){
            return false;
        }
        return true;
    }
    
    public void CreateUser(
        string email,
        string password,
        string name,
        AppDbContext db
        )
    {
        bool userExists = VerifyUserExistence(email, db);
        var user = new User
        {
            Email = email,
            Password = new Hash(_configuration).CreateHash(password),
            Name = name
        };

        db.Users.Add(user);
        db.SaveChanges();               
    }
    
}