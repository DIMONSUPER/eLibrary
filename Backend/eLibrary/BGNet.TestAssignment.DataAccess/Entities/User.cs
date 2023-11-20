namespace BGNet.TestAssignment.DataAccess.Entities;

public class User : IEntity
{
    public int Id { get; set; }
    public string Password { get; set; } = null!;
    public string Username { get; set; } = null!;
    public string FirstName { get; set; } = null!;
    public required string LastName { get; set; }
    public required string Address { get; set; } 
    public DateTime DateOfBirth { get; set; }
}
