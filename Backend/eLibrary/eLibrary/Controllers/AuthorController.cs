using eLibrary.Data.Repository;
using eLibrary.Helpers;
using eLibrary.Models;
using Microsoft.AspNetCore.Mvc;

namespace eLibrary.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthorController : BaseController
{
    private readonly IAuthorRepository _authorRepository;

    public AuthorController(IAuthorRepository authorRepository, IJwtService jwtService) : base(jwtService)
    {
        _authorRepository = authorRepository;
    }

    #region -- APIs implementation --

    [HttpGet]
    public IActionResult GetList()
    {
        return CheckAuthorization(() => Ok(_authorRepository.GetAll()));
    }

    [HttpGet("{id:int}")]
    public IActionResult GetAuthor(int id)
    {
        return CheckAuthorization(() => Ok(_authorRepository.GetById(id)));
    }

    [HttpPost]
    public IActionResult Create(Author author)
    {
        return CheckAuthorization(() => 
        {
            var createdAuthor = _authorRepository.Create(author);

            return Created("Success", createdAuthor.Id);
        });
    }

    [HttpPut]
    public IActionResult Update(Author author)
    {
        return CheckAuthorization(() =>
        {
            _authorRepository.Update(author);

            return Ok("Success");
        });
    }

    [HttpDelete]
    public IActionResult Delete(int id) 
    {
        return CheckAuthorization(() =>
        {
            _authorRepository.Delete(id);

            return Ok("Success");
        });
    }

    #endregion
}
