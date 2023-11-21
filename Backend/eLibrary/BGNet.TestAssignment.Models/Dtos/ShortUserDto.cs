namespace BGNet.TestAssignment.Models.Dtos;

public class ShortUserDto
{
    public required string Username { get; set; }
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public required string Address { get; set; }
    public DateTime DateOfBirth { get; set; }
}
