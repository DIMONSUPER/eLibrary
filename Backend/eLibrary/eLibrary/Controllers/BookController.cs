using BGNet.TestAssignment.Api.Data.Repository;
using BGNet.TestAssignment.Api.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BGNet.TestAssignment.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BookController : ControllerBase
{
    private readonly IBookRepository _bookRepository;

    public BookController(IBookRepository bookRepository)
    {
        _bookRepository = bookRepository;
    }

    #region -- APIs implementation --

    [HttpGet]
    [Authorize]
    public IActionResult GetList()
    {
        return Ok(_bookRepository.GetAll());
    }

    [HttpGet("{id:int}")]
    public IActionResult GetBook(int id)
    {
        return Ok(_bookRepository.GetById(id));
    }

    [HttpPost]
    public IActionResult Create(Book book)
    {
        var createdBook = _bookRepository.Create(book);

        return Created("Success", createdBook.Id);
    }

    [HttpPut]
    public IActionResult Update(Book book)
    {
        _bookRepository.Update(book);

        return Ok("Success");
    }

    [HttpDelete]
    public IActionResult Delete(int id)
    {
        _bookRepository.Delete(id);

        return Ok("Success");
    }

    #endregion
}
