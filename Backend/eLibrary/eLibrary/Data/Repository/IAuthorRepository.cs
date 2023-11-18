using BGNet.TestAssignment.Api.Models;

namespace BGNet.TestAssignment.Api.Data.Repository;

public interface IAuthorRepository
{
    Author Create(Author author);
    IEnumerable<Author> GetAll();
    void Update(Author author);
    Author GetById(int id);
    void Delete(int id);
}
