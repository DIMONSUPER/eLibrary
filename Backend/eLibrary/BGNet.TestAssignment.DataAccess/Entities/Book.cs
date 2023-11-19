namespace BGNet.TestAssignment.DataAccess.Entities;

public class Book : IEntity
{
    public int Id { get; set; }
    public string Title { get; set; } = null!;
    public int PublicationYear { get; set; }
    public string Genre { get; set; } = null!;

    public Author? Author { get; set; }
    public int AuthorId { get; set; }
}
