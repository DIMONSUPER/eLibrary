namespace eLibrary.Models;

public class Author : IEntity
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string Surname { get; set; } = null!;
    public DateTime DateOfBirth { get; set; }

    public ICollection<Book>? Books { get; set; }
}
