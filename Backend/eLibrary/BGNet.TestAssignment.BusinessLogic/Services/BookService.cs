using BGNet.TestAssignment.DataAccess.Entities;
using BGNet.TestAssignment.DataAccess.Repository;
using BGNet.TestAssignment.Models.Dtos;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace BGNet.TestAssignment.BusinessLogic.Services;

public class BookService : IBookService
{
    private readonly IRepository _repository;

    public BookService(IRepository repository)
    {
        _repository = repository;
    }

    #region -- IBookRepository implementation --

    public BookDto Create(BookDto bookDto)
    {
        var newBook = bookDto.Adapt<Book>();

        var createdBook = _repository.Create(newBook);

        return createdBook.Adapt<BookDto>();
    }

    public void Delete(int id)
    {
        var book = _repository.GetNoTrackingQueryable<Book>().SingleOrDefault(x => x.Id == id);

        if (book is not null)
        {
            _repository.Delete(book);
        }
    }

    public IEnumerable<BookDto> GetAll()
    {
        return _repository.GetNoTrackingQueryable<Book>().Include(x => x.Author).Adapt<IEnumerable<BookDto>>();
    }

    public BookDto? GetById(int id)
    {
        return _repository.GetNoTrackingQueryable<Book>().Include(x => x.Author).SingleOrDefault(x => x.Id == id).Adapt<BookDto>();
    }

    public void Update(BookDto bookDto)
    {
        var book = bookDto.Adapt<Book>();

        _repository.Update(book);
    }

    #endregion
}

public interface IBookService
{
    BookDto Create(BookDto bookDto);
    IEnumerable<BookDto> GetAll();
    void Update(BookDto bookDto);
    BookDto? GetById(int id);
    void Delete(int id);
}
