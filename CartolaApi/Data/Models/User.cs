namespace CartolaApi.Data.Models;


public class User
{
    public int? Id { get; set; }
    public required string Name { get; set; }
    public required string Email { get; set; }
    public required string Password { get; set; }
    public required string Phone { get; set; }

    public static User CreateUser(string name, string email, string password, string phone)
    {
        return new User
        {
            Name = name,
            Email = email,
            Password = password,
            Phone = phone
        };
    }
}