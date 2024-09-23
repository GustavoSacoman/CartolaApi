using System.ComponentModel.DataAnnotations;

namespace CartolaApi.Data.DTOs;


public class User
{
    public int? Id { get; set; }
    [MaxLength(255)]
    public string? Name { get; set; }
    [MaxLength(100)] 
    [EmailAddress] 
    public string? Email { get; set; }
    [MaxLength(50)]
    public string? Password { get; set; }
    [MaxLength(15)]
    public required string Phone { get; set; }

    // Dto é isso aqui que eu fiz
    // é um modelo pro Banco de Dados
    // aí pra requisição a gnt faz outro
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