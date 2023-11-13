using eLibrary.Data.Repository;
using eLibrary.Dtos;
using eLibrary.Helpers;
using eLibrary.Models;
using Microsoft.AspNetCore.Mvc;

namespace eLibrary.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IUserRepository _userRepository;
    private readonly IJwtService _jwtService;

    public AuthController(IUserRepository userRepository, IJwtService jwtService)
    {
        _userRepository = userRepository;
        _jwtService = jwtService;
    }

    #region -- APIs implementation --

    [HttpPost(nameof(Register))]
    public IActionResult Register(RegisterDto registerDto)
    {
        var user = new User
        {
            Username = registerDto.Username,
            Password = BCrypt.Net.BCrypt.HashPassword(registerDto.Password),
            Surname = registerDto.Surname,
            Address = registerDto.Address,
            DateOfBirth = registerDto.DateOfBirth,
            Name = registerDto.Name,
        };

        return Created("Success", _userRepository.Create(user));
    }

    [HttpPost(nameof(Login))]
    public IActionResult Login(LoginDto loginDto)
    {
        var user = _userRepository.GetByUsername(loginDto.Username);

        IActionResult result;

        if (user is not null && BCrypt.Net.BCrypt.Verify(loginDto.Password, user.Password))
        {
            var jwt = _jwtService.Generate(user.Id);

            Response.Cookies.Append(nameof(jwt), jwt, new CookieOptions { HttpOnly = true });

            result = Ok(new { jwt });
        }
        else
        {
            result = BadRequest(new { message = "Invalid Credentials" });
        }

        return result;
    }

    [HttpGet("user")]
    public IActionResult GetUser()
    {
        try
        {
            var jwt = GetJwtToken();

            var token = _jwtService.Verify(jwt);

            var userId = int.Parse(token.Issuer);

            var user = _userRepository.GetById(userId);

            return Ok(user);
        }
        catch (Exception ex)
        {
            return Unauthorized();
        }
    }

    [HttpPost(nameof(Logout))]
    public IActionResult Logout()
    {
        Response.Cookies.Delete("jwt");

        return Ok(new { message = "success" });
    }

    #endregion

    #region -- Private helpers --

    private string GetJwtToken()
    {
        return Request.Headers.Authorization[0]?.Split(' ')[1];
    }

    #endregion
}
