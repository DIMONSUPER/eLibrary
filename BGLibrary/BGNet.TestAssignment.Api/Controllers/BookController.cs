using BGNet.TestAssignment.BusinessLogic.Services;
using BGNet.TestAssignment.Models.Dtos;
using BGNet.TestAssignment.Models.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace BGNet.TestAssignment.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class BookController : ControllerBase
{
    private readonly IBookService _bookRepository;

    public BookController(IBookService bookRepository)
    {
        _bookRepository = bookRepository;
    }

    #region -- APIs implementation --

    [HttpGet]
    public IActionResult GetList()
    {
        return Ok(new ApiResponse<IEnumerable<BookDto>>
        {
            StatusCode = (int)HttpStatusCode.OK,
            Data = _bookRepository.GetAll(),
            Message = "Success",
        });
    }

    [HttpGet("{id:int}")]
    public IActionResult GetBook(int id)
    {
        IActionResult result;

        var author = _bookRepository.GetById(id);

        if (author is not null)
        {
            result = Ok(new ApiResponse<BookDto>
            {
                StatusCode = (int)HttpStatusCode.OK,
                Data = _bookRepository.GetById(id),
                Message = "Success",
            });
        }
        else
        {
            result = BadRequest(new ApiResponse
            {
                StatusCode = (int)HttpStatusCode.OK,
                Errors = new[] { $"Book with id {id} not found" },
            });
        }

        return result;
    }

    [HttpPost]
    public IActionResult Create(BookDto book)
    {
        return CreatedAtAction(nameof(Create), new ApiResponse<BookDto>
        {
            StatusCode = (int)HttpStatusCode.Created,
            Data = _bookRepository.Create(book),
            Message = "Success",
        });
    }

    [HttpPut]
    public IActionResult Update(BookDto book)
    {
        _bookRepository.Update(book);

        return Ok(new ApiResponse
        {
            StatusCode = (int)HttpStatusCode.OK,
            Message = "Success",
        });
    }

    [HttpDelete("{id:int}")]
    public IActionResult Delete(int id)
    {
        IActionResult result;

        var authorToDelete = _bookRepository.GetById(id);

        if (authorToDelete is not null)
        {
            _bookRepository.Delete(id);

            result = Ok(new ApiResponse
            {
                StatusCode = (int)HttpStatusCode.OK,
                Message = $"Author with id {id} was deleted successfully",
            });
        }
        else
        {
            result = BadRequest(new ApiResponse
            {
                StatusCode = (int)HttpStatusCode.BadRequest,
                Errors = new[] { $"Author with id {id} not found" },
            });
        }

        return result;
    }

    #endregion
}
