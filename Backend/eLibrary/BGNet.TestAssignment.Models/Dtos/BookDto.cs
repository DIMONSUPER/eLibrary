using BGNet.TestAssignment.DataAccess.Entities;

namespace BGNet.TestAssignment.Models.Dtos;

public class BookDto
{
    public int Id { get; set; }
    public string Title { get; set; } = null!;
    public int PublicationYear { get; set; }
    public string Genre { get; set; } = null!;
    public AuthorDto? Author { get; set; }
    public int AuthorId { get; set; }
}
