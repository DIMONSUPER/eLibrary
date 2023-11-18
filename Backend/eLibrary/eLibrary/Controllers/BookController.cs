﻿using BGNet.TestAssignment.BusinessLogic.Services;
using BGNet.TestAssignment.DataAccess.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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
