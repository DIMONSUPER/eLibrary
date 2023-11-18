using BGNet.TestAssignment.DataAccess.Entities;
using BGNet.TestAssignment.DataAccess.Repository;

namespace BGNet.TestAssignment.BusinessLogic.Services;

public class AuthorService : IAuthorService
{
    private readonly IRepository _repository;

    public AuthorService(IRepository repository)
    {
        _repository = repository;
    }

    #region -- IAuthorRepository implementation --

    public Author Create(Author author)
    {
        return _repository.Create(author);
    }

    public void Delete(int id)
    {
        var author = _repository.GetById<Author>(id);

        if (author is not null)
        {
            _repository.Delete(author);
        }
    }

    public IEnumerable<Author> GetAll()
    {
        return _repository.GetAll<Author>(nameof(Author.Books));
    }

    public Author? GetById(int id)
    {
        return _repository.GetById<Author>(id, nameof(Author.Books));
    }

    public void Update(Author author)
    {
        _repository.Update(author);
    }

    #endregion
}

public interface IAuthorService
{
    Author Create(Author author);
    IEnumerable<Author> GetAll();
    void Update(Author author);
    Author? GetById(int id);
    void Delete(int id);
}
