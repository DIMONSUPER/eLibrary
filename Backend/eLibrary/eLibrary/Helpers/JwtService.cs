using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace eLibrary.Helpers;

public class JwtService : IJwtService
{
    private const string SECURE_KEY = "f8c66189b40d20e32b639cb2815bf2e68ae9d77da2e6a5138724ed3518d3a6e3";
    private const int JWT_EXPIRES_IN_DAYS = 1;

    #region -- Public helpers --

    public string Generate(int id)
    {
        var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(SECURE_KEY));
        var credentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256Signature);
        var header = new JwtHeader(credentials);

        var payload = new JwtPayload(id.ToString(), null, null, null, DateTime.Today.AddDays(JWT_EXPIRES_IN_DAYS));
        var securityToken = new JwtSecurityToken(header, payload);

        return new JwtSecurityTokenHandler().WriteToken(securityToken);
    }

    public JwtSecurityToken Verify(string jwt)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(SECURE_KEY);

        tokenHandler.ValidateToken(jwt, new TokenValidationParameters()
        {
            IssuerSigningKey = new SymmetricSecurityKey(key),
            ValidateIssuerSigningKey = true,
            ValidateIssuer = false,
            ValidateAudience = false,
        }, out var validatedToken);

        return (JwtSecurityToken)validatedToken;
    }

    #endregion
}
