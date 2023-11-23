using BGNet.TestAssignment.Common.Configurations.Jwt;
using BGNet.TestAssignment.Models.Dtos;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BGNet.TestAssignment.BusinessLogic.Services;

public class AuthService : IAuthService
{
    private readonly IOptions<JwtOptions> _jwtOptions;
    private readonly IUserService _userService;

    public AuthService(IUserService userService, IOptions<JwtOptions> jwtOptions)
    {
        _userService = userService;
        _jwtOptions = jwtOptions;
    }

    #region -- IAuthService implementation --

    public void RegisterNewUser(FullUserDto userDto)
    {
        userDto.Password = BCrypt.Net.BCrypt.HashPassword(userDto.Password);

        _userService.Create(userDto);
    }

    public string? GenerateJwt(string username)
    {
        var claims = new[]
        {
            new Claim(ClaimTypes.Name, username),
        };

        var now = DateTime.UtcNow;
        var key = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_jwtOptions.Value.SecureKey));

        var jwt = new JwtSecurityToken(
            issuer: _jwtOptions.Value.Issuer,
            claims: claims,
            notBefore: now,
            expires: now.Add(TimeSpan.FromDays(_jwtOptions.Value.ExpiresDays)),
            signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256));

        var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

        return encodedJwt;
    }

    #endregion
}

public interface IAuthService
{
    void RegisterNewUser(FullUserDto userDto);
    string? GenerateJwt(string username);
}
