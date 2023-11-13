using eLibrary.Data.Repository;
using eLibrary.Helpers;
using eLibrary.Models;
using Microsoft.AspNetCore.Mvc;

namespace eLibrary.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BookController : BaseController
{
    private readonly IBookRepository _bookRepository;

    public BookController(IBookRepository bookRepository, IJwtService jwtService) : base(jwtService)
    {
        _bookRepository = bookRepository;
    }

    #region -- APIs implementation --

    [HttpGet]
    public IActionResult GetList()
    {
        return CheckAuthorization(() => Ok(_bookRepository.GetAll()));
    }

    [HttpGet("{id:int}")]
    public IActionResult GetBook(int id)
    {
        return CheckAuthorization(() => Ok(_bookRepository.GetById(id)));
    }

    [HttpPost]
    public IActionResult Create(Book book)
    {
        return CheckAuthorization(() =>
        {
            var createdBook = _bookRepository.Create(book);

            return Created("Success", createdBook.Id);
        });
    }

    [HttpPut]
    public IActionResult Update(Book book)
    {
        return CheckAuthorization(() =>
        {
            _bookRepository.Update(book);

            return Ok("Success");
        });
    }

    [HttpDelete]
    public IActionResult Delete(int id)
    {
        return CheckAuthorization(() =>
        {
            _bookRepository.Delete(id);

            return Ok("Success");
        });
    }

    #endregion
}
