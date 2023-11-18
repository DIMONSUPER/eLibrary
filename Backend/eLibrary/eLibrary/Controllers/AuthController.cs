using BGNet.TestAssignment.Api.Dtos;
using BGNet.TestAssignment.BusinessLogic.Services;
using BGNet.TestAssignment.DataAccess.Entities;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BGNet.TestAssignment.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IUserService _userRepository;

    public AuthController(IUserService userRepository)
    {
        _userRepository = userRepository;
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

    [HttpPost("login")]
    public async Task<IActionResult> LoginAsync(LoginDto loginDto)
    {
        var user = _userRepository.GetByUsername(loginDto.Username);

        IActionResult result;

        if (user is not null && BCrypt.Net.BCrypt.Verify(loginDto.Password, user.Password))
        {
            var claims = new List<Claim> 
            {
                new(ClaimTypes.Name, user.Username),
            };

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new(claimsIdentity));

            result = Ok("Success");
        }
        else
        {
            result = BadRequest(new { message = "Invalid credentials" });
        }

        return result;
    }

    [HttpGet("user")]
    [Authorize]
    public IActionResult GetUser()
    {
        var user = _userRepository.GetByUsername(User.Identity!.Name!);

        return Ok(user);
    }

    [HttpPost("logout")]
    public async Task<IActionResult> LogoutAsync()
    {
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

        return Ok("Success");
    }

    #endregion
}
