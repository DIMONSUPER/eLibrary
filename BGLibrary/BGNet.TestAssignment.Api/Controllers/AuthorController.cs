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
public class AuthorController : ControllerBase
{
    private readonly IAuthorService _authorRepository;

    public AuthorController(IAuthorService authorRepository)
    {
        _authorRepository = authorRepository;
    }

    #region -- APIs implementation --

    [HttpGet]
    public IActionResult GetList()
    {
        return Ok(new ApiResponse<IEnumerable<AuthorDto>>
        {
            StatusCode = (int)HttpStatusCode.OK,
            Data = _authorRepository.GetAll(),
            Message = "Success",
        });
    }

    [HttpGet("{id:int}")]
    public IActionResult GetAuthor(int id)
    {
        IActionResult result;

        var author = _authorRepository.GetById(id);

        if (author is not null)
        {
            result = Ok(new ApiResponse<AuthorDto>
            {
                StatusCode = (int)HttpStatusCode.OK,
                Data = _authorRepository.GetById(id),
                Message = "Success",
            });
        }
        else
        {
            result = BadRequest(new ApiResponse
            {
                StatusCode = (int)HttpStatusCode.OK,
                Errors = new[] { $"Author with id {id} not found" },
            });
        }

        return result;
    }

    [HttpPost]
    public IActionResult Create(AuthorDto author)
    {
        return CreatedAtAction(nameof(Create), new ApiResponse<AuthorDto>
        {
            StatusCode = (int)HttpStatusCode.Created,
            Data = _authorRepository.Create(author),
            Message = "Success",
        });
    }

    [HttpPut]
    public IActionResult Update(AuthorDto author)
    {
        _authorRepository.Update(author);

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

        var authorToDelete = _authorRepository.GetById(id);

        if (authorToDelete is not null)
        {
            _authorRepository.Delete(id);

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
