using BGNet.TestAssignment.Api.Models;

namespace BGNet.TestAssignment.Api.Data.Repository;

public class BookRepository : IBookRepository
{
    private readonly IRepository _repository;

    public BookRepository(IRepository repository)
    {
        _repository = repository;
    }

    #region -- IBookRepository implementation --

    public Book Create(Book book)
    {
        return _repository.Create(book);
    }

    public void Delete(int id)
    {
        var book = _repository.GetById<Book>(id);

        if (book is not null)
        {
            _repository.Delete(book);
        }
    }

    public IEnumerable<Book> GetAll()
    {
        return _repository.GetAll<Book>(nameof(Book.Author));
    }

    public Book GetById(int id)
    {
        return _repository.GetById<Book>(id, nameof(Book.Author));
    }

    public void Update(Book book)
    {
        _repository.Update(book);
    }

    #endregion
}
