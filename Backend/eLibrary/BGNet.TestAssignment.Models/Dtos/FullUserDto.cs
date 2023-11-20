namespace BGNet.TestAssignment.Models.Dtos;

public class FullUserDto
{
    public string Password { get; set; } = null!;
    public string Username { get; set; } = null!;
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string Address { get; set; } = null!;
    public DateTime DateOfBirth { get; set; }
}
