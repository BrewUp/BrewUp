using BrewUpWasm.Production.Shared.JsonModel;

namespace BrewUpWasm.Production.Shared.Abstracts
{
    public interface ITokenService
    {
        Task StoreTokenAsync(string accessToken);
        Task<TokenJson> DecodeAndStoreTokenAsync(string accessToken);
        TokenJson DecodeToken(string accessToken);
        Task RefreshToken();

        Task<bool> IsValidAsync();
    }
}