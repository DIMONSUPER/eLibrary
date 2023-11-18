using BGNet.TestAssignment.DataAccess.Entities;
using BGNet.TestAssignment.DataAccess.Repository;

namespace BGNet.TestAssignment.BusinessLogic.Services;

public class BookService : IBookService
{
    private readonly IRepository _repository;

    public BookService(IRepository repository)
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

    public Book? GetById(int id)
    {
        return _repository.GetById<Book>(id, nameof(Book.Author));
    }

    public void Update(Book book)
    {
        _repository.Update(book);
    }

    #endregion
}

public interface IBookService
{
    Book Create(Book book);
    IEnumerable<Book> GetAll();
    void Update(Book book);
    Book? GetById(int id);
    void Delete(int id);
}
