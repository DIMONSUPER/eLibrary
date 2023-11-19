using System.Text.Json.Serialization;

namespace BGNet.TestAssignment.Models.Requests;

public class RegisterRequest
{
    public string Password { get; set; } = null!;
    public string Username { get; set; } = null!;
    public string Name { get; set; } = null!;
    public string Surname { get; set; } = null!;
    public string Address { get; set; } = null!;
    public DateTime DateOfBirth { get; set; }
}
