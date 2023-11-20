namespace BGNet.TestAssignment.Models.Dtos;

public class AuthorDto
{
    public int Id { get; set; }
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public DateTime DateOfBirth { get; set; }
    public string FullName => $"{FirstName} {LastName}";
}
