using BGNet.TestAssignment.Api.Models;

namespace BGNet.TestAssignment.Api.Data.Repository;

public class AuthorRepository : IAuthorRepository
{
    private readonly IRepository _repository;

    public AuthorRepository(IRepository repository)
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

    public Author GetById(int id)
    {
        return _repository.GetById<Author>(id, nameof(Author.Books));
    }

    public void Update(Author author)
    {
        _repository.Update(author);
    }

    #endregion
}
