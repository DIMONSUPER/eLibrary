namespace BGNet.TestAssignment.DataAccess.Entities;

public class Author : IEntity
{
    public int Id { get; set; }
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public required DateTime DateOfBirth { get; set; }

    public ICollection<Book> Books { get; set; } = new List<Book>();
}
