using System.IdentityModel.Tokens.Jwt;
using System.Net.Http.Headers;
using Blazored.SessionStorage;
using BrewUpWasm.Shared.Abstracts;
using BrewUpWasm.Shared.JsonModel;
using Microsoft.AspNetCore.Components;
using Newtonsoft.Json;

namespace BrewUpWasm.Shared.Concretes
{
    public sealed class TokenService : ITokenService
    {
        private readonly ISessionStorageService _sessionStorageService;
        private readonly HttpClient _httpClient;
        private readonly NavigationManager _navigationManager;

        public TokenService(ISessionStorageService sessionStorageService,
            HttpClient httpClient,
            NavigationManager navigationManager)
        {
            _sessionStorageService = sessionStorageService;
            _httpClient = httpClient;
            _navigationManager = navigationManager;
        }

        public async Task StoreTokenAsync(string accessToken)
        {
            await _sessionStorageService.SetItemAsync("token", accessToken);
        }

        public async Task<TokenJson> DecodeAndStoreTokenAsync(string accessToken)
        {
            var jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
            var jwt = (JwtSecurityToken)jwtSecurityTokenHandler.ReadToken(accessToken);

            var token = new TokenJson();

            if (jwt is null)
                return token;

            token.ValidFrom = jwt.ValidFrom;
            token.ValidTo = jwt.ValidTo;
            token.AccessToken = accessToken;

            foreach (var claim in jwt.Claims)
            {
                if (claim.Type.ToLower().Equals("accessToken"))
                    token.AccessToken = claim.Value;

                if (claim.Type.ToLower().Equals("platforms"))
                    token.Platforms = claim.Value;

                if (claim.Type.ToLower().Equals("company"))
                    token.Company = claim.Value;
            }

            await _sessionStorageService.SetItemAsync("token", token.AccessToken);

            return token;
        }

        public TokenJson DecodeToken(string accessToken)
        {
            var token = new TokenJson();

            if (string.IsNullOrWhiteSpace(accessToken))
                return token;

            var jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
            var jwt = (JwtSecurityToken)jwtSecurityTokenHandler.ReadToken(accessToken);

            if (jwt is null)
                return token;

            token.ValidFrom = jwt.ValidFrom;
            token.ValidTo = jwt.ValidTo;

            foreach (var claim in jwt.Claims)
            {
                if (claim.Type.ToLower().Equals("accessToken"))
                    token.AccessToken = claim.Value;

                if (claim.Type.ToLower().Equals("platforms"))
                    token.Platforms = claim.Value;

                if (claim.Type.ToLower().Equals("company"))
                    token.Company = claim.Value;
            }

            return token;
        }

        public async Task RefreshToken()
        {
            var accessToken = await _sessionStorageService.GetItemAsync<string>("token");
            if (string.IsNullOrWhiteSpace(accessToken))
            {
                _navigationManager.NavigateTo("/");
                return;
            }

            var request = new HttpRequestMessage(HttpMethod.Post, "/v1/Tokens/getnewtoken");
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
            
            var response = await _httpClient.SendAsync(request);
            var content = await response.Content.ReadAsStringAsync();
            var tokenJson = JsonConvert.DeserializeObject<TokenJson>(content);
            if (tokenJson is null)
            {
                _navigationManager.NavigateTo("/");
                return;
            }

            await DecodeAndStoreTokenAsync(tokenJson.AccessToken);
        }

        public async Task<bool> IsValidAsync()
        {
            var accessToken = await _sessionStorageService.GetItemAsync<string>("token");
            if (string.IsNullOrWhiteSpace(accessToken))
                return false;

            var token = DecodeToken(accessToken);
            return token.ValidTo >= DateTime.UtcNow;
        }
    }
}