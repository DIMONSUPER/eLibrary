namespace BGNet.TestAssignment.Models.Dtos;

public class AuthorDto
{
    public int Id { get; set; }
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public DateTime DateOfBirth { get; set; }
    public string FullName => $"{FirstName} {LastName}";
}
