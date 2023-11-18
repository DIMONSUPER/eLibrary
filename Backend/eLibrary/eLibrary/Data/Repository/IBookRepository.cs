using BGNet.TestAssignment.Api.Models;

namespace BGNet.TestAssignment.Api.Data.Repository;

public interface IBookRepository
{
    Book Create(Book book);
    IEnumerable<Book> GetAll();
    void Update(Book book);
    Book GetById(int id);
    void Delete(int id);
}
