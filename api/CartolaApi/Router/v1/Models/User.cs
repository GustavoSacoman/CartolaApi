namespace CartolaApi.Router.v1.Models;

public class User
{
    public required string Name { get; set; }
    public required string Email { get; set; }
    public required string Password { get; set; }
    public required string Phone { get; set; }
}

public class UserLogin
{
    public required string Email { get; set; }
    public required string Password { get; set; }
}

public class UserUpdate
{
    public string? Name { get; set; }
    public string? Email { get; set; }
    public string? Phone { get; set; }
}
