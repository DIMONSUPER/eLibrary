using BGNet.TestAssignment.BusinessLogic.Services;
using BGNet.TestAssignment.Models.Dtos;
using BGNet.TestAssignment.Models.Requests;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity.Data;
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
    public IActionResult Register(CreateUserDto createUserDto)
    {
        createUserDto.Password = BCrypt.Net.BCrypt.HashPassword(createUserDto.Password);

        return Created("Success", _userRepository.Create(createUserDto));
    }

    [HttpPost("login")]
    public async Task<IActionResult> LoginAsync(Models.Requests.LoginRequest loginRequest)
    {
        var user = _userRepository.GetByUsername(loginRequest.Username);

        IActionResult result;

        if (user is not null && BCrypt.Net.BCrypt.Verify(loginRequest.Password, user.Password))
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
