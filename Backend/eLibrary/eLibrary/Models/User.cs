using System.Text.Json.Serialization;

namespace eLibrary.Models;

public class User : IEntity
{
    public int Id { get; set; }
    [JsonIgnore]
    public string Password { get; set; } = null!;
    public string Username { get; set; } = null!;
    public string Name { get; set; } = null!;
    public string Surname { get; set; } = null!;
    public string Address { get; set; } = null!;
    public DateTime DateOfBirth { get; set; }
}
