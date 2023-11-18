namespace BGNet.TestAssignment.DataAccess.Jwt;

public class JwtOptions
{
    public const string Jwt = nameof(Jwt);

    public string Authority { get; set; } = null!;
    public string Issuer { get; set; } = null!;
    public string SecureKey { get; set; } = null!;
    public int ExpiresDays { get; set; }

}
