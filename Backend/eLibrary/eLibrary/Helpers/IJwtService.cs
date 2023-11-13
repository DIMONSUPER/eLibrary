using System.IdentityModel.Tokens.Jwt;

namespace eLibrary.Helpers;

public interface IJwtService
{
    string Generate(int id);
    JwtSecurityToken Verify(string jwt);
}
