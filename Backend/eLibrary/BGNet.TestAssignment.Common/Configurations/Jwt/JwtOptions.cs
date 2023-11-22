namespace BGNet.TestAssignment.Common.Configurations.Jwt;

public class JwtOptions
{
    public const string Jwt = nameof(Jwt);

    public required string Authority { get; set; }
    public required string Issuer { get; set; }
    public required string SecureKey { get; set; }
    public required int ExpiresDays { get; set; }
}
