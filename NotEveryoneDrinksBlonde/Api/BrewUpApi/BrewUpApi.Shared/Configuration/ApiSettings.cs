namespace BrewUpApi.Shared.Configuration;

public class ApiSettings
{
    public TokenAuthentication TokenAuthentication { get; set; } = new();
}

public class TokenAuthentication
{
    public string SecretKey { get; set; } = string.Empty;
    public string HeaderSecretKey { get; set; } = string.Empty;
    public string Issuer { get; set; } = string.Empty;
    public string Audience { get; set; } = string.Empty;
}