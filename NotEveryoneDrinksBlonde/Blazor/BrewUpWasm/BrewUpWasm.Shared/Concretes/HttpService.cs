using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using Blazored.SessionStorage;
using BrewUpWasm.Shared.Abstracts;
using Microsoft.AspNetCore.Components;
using Newtonsoft.Json;

namespace BrewUpWasm.Shared.Concretes
{
    public class HttpService : IHttpService
    {
        private readonly HttpClient _httpClient;
        private readonly NavigationManager _navigationManager;
        //private readonly ILocalStorageService _localStorageService;
        private readonly ISessionStorageService _sessionStorageService;
        private readonly ITokenService _tokenService;

        public HttpService(HttpClient httpClient,
            NavigationManager navigationManager,
            ISessionStorageService sessionStorageService,
            ITokenService tokenService)
        {
            _httpClient = httpClient;
            _navigationManager = navigationManager;
            _sessionStorageService = sessionStorageService;
            _tokenService = tokenService;
        }

        public async Task<byte[]> DownloadAsync(string uri)
        {
            // Add Bearer Token
            var token = await _sessionStorageService.GetItemAsync<string>("token");
            _httpClient.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", token);

            var response = await _httpClient.GetAsync(uri);
            var content = await response.Content.ReadAsStringAsync();

            // auto logout on 401 response
            if (response.StatusCode.Equals(HttpStatusCode.Unauthorized))
            {
                _navigationManager.NavigateTo("logout");
                return default!;
            }

            if (response.IsSuccessStatusCode)
                return Encoding.ASCII.GetBytes(content);

            // throw exception on error response
            var error = await response.Content.ReadFromJsonAsync<Dictionary<string, string>>();
            throw new Exception(error?["message"]);
        }

        public async Task<T> Get<T>(string uri)
        {
            try
            {
                using var request =
                    new HttpRequestMessage(HttpMethod.Get, uri);

                var response = await _httpClient.SendAsync(request);

                //var response = await this._httpClient.GetAsync(uri);
                var content = await response.Content.ReadAsStringAsync();

                switch (response.StatusCode)
                {
                    // auto logout on 401 response
                    case HttpStatusCode.Unauthorized:
                        _navigationManager.NavigateTo("logout");
                        return default!;

                    case HttpStatusCode.InternalServerError:
                        if (!content.StartsWith("IDX10222"))
                            return default!;

                        // In this case the Token is not yet valid
                        // so I add a sleep, end re-try
                        Thread.Sleep(1000);
                        await Get<T>(uri);
                        return default!;
                }

                // throw exception on error response
                if (response.IsSuccessStatusCode)
                    return JsonConvert.DeserializeObject<T>(content)!;

                var error = await response.Content.ReadFromJsonAsync<Dictionary<string, string>>();
                throw new Exception(error?["message"]);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
        }

        public async Task<string> GetSettings<T>(string uri)
        {
            try
            {
                if (!await _tokenService.IsValidAsync())
                    await _tokenService.RefreshToken();

                // Add Bearer Token
                var token = await _sessionStorageService.GetItemAsync<string>("token");
                _httpClient.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer", token);

                return await _httpClient.GetStringAsync(uri);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
        }

        public async Task<T> Post<T>(string uri, object value)
        {
            var request = new HttpRequestMessage(HttpMethod.Post, uri)
            {
                Content = new StringContent(JsonConvert.SerializeObject(value), Encoding.UTF8, "application/json")
            };

            return await SendRequest<T>(request);
        }

        public async Task Post(string uri, object value)
        {
            var request = new HttpRequestMessage(HttpMethod.Post, uri)
            {
                Content = new StringContent(JsonConvert.SerializeObject(value), Encoding.UTF8, "application/json")
            };

            await SendRequest(request);
        }

        public async Task PostAsFormData(string uri, MultipartFormDataContent form)
        {
            // Add Bearer Token
            var token = await _sessionStorageService.GetItemAsync<string>("token");
            _httpClient.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", token);

            await _httpClient.PostAsync(uri, form);
        }

        public async Task<T> Put<T>(string uri, object value)
        {
            var request = new HttpRequestMessage(HttpMethod.Put, uri)
            {
                Content = new StringContent(JsonConvert.SerializeObject(value), Encoding.UTF8, "application/json")
            };

            return await SendRequest<T>(request);
        }

        public async Task Put(string uri, object value)
        {
            var request = new HttpRequestMessage(HttpMethod.Put, uri)
            {
                Content = new StringContent(JsonConvert.SerializeObject(value), Encoding.UTF8, "application/json")
            };

            await SendRequest(request);
        }

        public async Task<T> Patch<T>(string uri, object value)
        {
            var request = new HttpRequestMessage(HttpMethod.Patch, uri)
            {
                Content = new StringContent(JsonConvert.SerializeObject(value), Encoding.UTF8, "application/json")
            };

            return await SendRequest<T>(request);
        }

        public async Task Patch(string uri, object value)
        {
            var request = new HttpRequestMessage(HttpMethod.Patch, uri)
            {
                Content = new StringContent(JsonConvert.SerializeObject(value), Encoding.UTF8, "application/json")
            };

            await SendRequest(request);
        }

        public async Task Delete(string uri, object value)
        {
            var request = new HttpRequestMessage(HttpMethod.Delete, uri)
            {
                Content = new StringContent(JsonConvert.SerializeObject(value), Encoding.UTF8, "application/json")
            };

            await SendRequest(request);
        }

        #region Helpers
        private async Task<T> SendRequest<T>(HttpRequestMessage request)
        {
            // Add Bearer Token
            var accessToken = await _sessionStorageService.GetItemAsync<string>("token");
            //var token = this._tokenService.DecodeToken(accessToken);
            //if (token.ValidTo < DateTime.UtcNow)
            //{
            //    await this._tokenService.RefreshToken();
            //    accessToken = await this._localStorageService.GetItem<string>("token");
            //}

            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

            using var response = await _httpClient.SendAsync(request);

            if (response.StatusCode.Equals(HttpStatusCode.MethodNotAllowed))
                throw new Exception("Operation Not Allowed");

            // auto logout on 401 response
            if (response.StatusCode.Equals(HttpStatusCode.Unauthorized))
            {
                _navigationManager.NavigateTo("logout");
                return default!;
            }

            // throw exception on error response
            if (response.IsSuccessStatusCode)
                return await response.Content.ReadFromJsonAsync<T>();

            var error = await response.Content.ReadFromJsonAsync<Dictionary<string, string>>();

            throw new Exception(error?["message"]);
        }

        private async Task SendRequest(HttpRequestMessage request)
        {
            // Add Bearer Token
            //var accessToken = await _sessionStorageService.GetItemAsync<string>("token");
            //request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

            using var response = await _httpClient.SendAsync(request);

            if (response.StatusCode.Equals(HttpStatusCode.MethodNotAllowed))
            {
                //TODO: Create page with MethodNotAllowed
                throw new Exception("Operation Not Allowed");
            }

            // auto logout on 401 response
            if (response.StatusCode.Equals(HttpStatusCode.Unauthorized))
            {
                _navigationManager.NavigateTo("logout");
            }

            // throw exception on error response
            if (!response.IsSuccessStatusCode)
            {
                var error = await response.Content.ReadFromJsonAsync<Dictionary<string, string>>();

                throw new Exception(error?["message"]);
            }
        }
        #endregion
    }
}