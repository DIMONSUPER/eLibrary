namespace BGNet.TestAssignment.DataAccess.Entities;

public class User : IEntity
{
    public int Id { get; set; }
    public required string Password { get; set; }
    public required string Username { get; set; }
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public required string Address { get; set; } 
    public required DateTime DateOfBirth { get; set; }
}
