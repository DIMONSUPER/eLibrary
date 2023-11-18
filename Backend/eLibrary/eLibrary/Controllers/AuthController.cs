using BGNet.TestAssignment.Api.Data.Repository;
using BGNet.TestAssignment.Api.Dtos;
using BGNet.TestAssignment.Api.Models;
using BGNet.TestAssignment.DataAccess.Jwt;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace BGNet.TestAssignment.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController(IUserRepository userRepository, IOptions<JwtOptions> jwtOptions) : ControllerBase
{
    private readonly IUserRepository _userRepository = userRepository;
    private readonly IOptions<JwtOptions> _jwtOptions = jwtOptions;

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
            var now = DateTime.UtcNow;
            var key = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(jwtOptions.Value.SecureKey));

            var jwt = new JwtSecurityToken(
                issuer: _jwtOptions.Value.Issuer,
                notBefore: now,
                expires: now.Add(TimeSpan.FromDays(_jwtOptions.Value.ExpiresDays)),
                signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256));

            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

            Response.Cookies.Append("jwt", encodedJwt, new CookieOptions { HttpOnly = true });

            result = Ok(new 
            {
                access_token = encodedJwt,
                user_id = user.Id,
            });
        }
        else
        {
            result = BadRequest(new { message = "Invalid Credentials" });
        }

        return result;
    }

    [HttpGet("user")]
    [Authorize]
    public IActionResult GetUser()
    {
        try
        {
            var jwt = GetJwtToken();

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
