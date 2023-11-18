using BGNet.TestAssignment.BusinessLogic.Services;
using BGNet.TestAssignment.DataAccess.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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
        return Ok(_authorRepository.GetAll());
    }

    [HttpGet("{id:int}")]
    public IActionResult GetAuthor(int id)
    {
        return Ok(_authorRepository.GetById(id));
    }

    [HttpPost]
    public IActionResult Create(Author author)
    {
        var createdAuthor = _authorRepository.Create(author);

        return Created("Success", createdAuthor.Id);
    }

    [HttpPut]
    public IActionResult Update(Author author)
    {
        _authorRepository.Update(author);

        return Ok("Success");
    }

    [HttpDelete]
    public IActionResult Delete(int id)
    {
        _authorRepository.Delete(id);

        return Ok("Success");
    }

    #endregion
}
