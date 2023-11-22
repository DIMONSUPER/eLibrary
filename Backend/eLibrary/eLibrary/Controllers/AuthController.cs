using BGNet.TestAssignment.BusinessLogic.Services;
using BGNet.TestAssignment.Models.Dtos;
using BGNet.TestAssignment.Models.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace BGNet.TestAssignment.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IUserService _userRepository;
    private readonly IAuthService _authService;

    public AuthController(IUserService userRepository, IAuthService authService)
    {
        _userRepository = userRepository;
        _authService = authService;
    }

    #region -- APIs implementation --

    [HttpPost(nameof(Register))]
    public IActionResult Register(FullUserDto userDto)
    {
        IActionResult result;

        if (_userRepository.VerifyUsernameIsNotBusy(userDto.Username))
        {
            _authService.RegisterNewUser(userDto);

            result = CreatedAtAction(nameof(Register), new ApiResponse
            {
                StatusCode = (int)HttpStatusCode.Created,
                Message = $"User with username {userDto.Username} was created successfully",
            });
        }
        else
        {
            result = BadRequest(new ApiResponse
            {
                StatusCode = (int)HttpStatusCode.BadRequest,
                Errors = new[] { "User with such username already exists" },
            });
        }

        return result;
    }

    [HttpPost(nameof(Login))]
    public IActionResult Login(Models.Requests.LoginRequest loginRequest)
    {
        IActionResult result;

        if (_userRepository.VerifyLoginRequest(loginRequest))
        {
            var jwt = _authService.GenerateJwt(loginRequest.Username);

            result = Ok(jwt);
        }
        else
        {
            result = Unauthorized(new { message = "Invalid credentials" });
        }

        return result;
    }

    [HttpGet("user")]
    [Authorize]
    public IActionResult GetUser()
    {
        IActionResult result;

        if (string.IsNullOrWhiteSpace(User?.Identity?.Name))
        {
            result = Unauthorized("You should be authorized");
        }
        else
        {
            var user = _userRepository.GetShortUserByUsername(User.Identity.Name);

            result = Ok(user);
        }

        return result;
    }

    #endregion
}
