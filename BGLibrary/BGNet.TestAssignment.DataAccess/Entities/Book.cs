namespace BGNet.TestAssignment.DataAccess.Entities;

public class Book : IEntity
{
    public int Id { get; set; }
    public required string Title { get; set; }
    public required int PublicationYear { get; set; }
    public required string Genre { get; set; }

    public Author? Author { get; set; }
    public required int AuthorId { get; set; }
}
