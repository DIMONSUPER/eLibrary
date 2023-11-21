namespace BGNet.TestAssignment.Models.Dtos;

public class BookDto
{
    public int Id { get; set; }
    public required string Title { get; set; }
    public required int PublicationYear { get; set; }
    public required string Genre { get; set; }
    public AuthorDto? Author { get; set; }
    public int AuthorId { get; set; }
}
