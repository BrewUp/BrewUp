using BrewUpWasm.Shared.Abstracts;
using Microsoft.JSInterop;
using Newtonsoft.Json;

namespace BrewUpWasm.Shared.Concretes
{
    public sealed class LocalStorageService : ILocalStorageService
    {
        private readonly IJSRuntime _jsRuntime;

        public LocalStorageService(IJSRuntime jsRuntime)
        {
            this._jsRuntime = jsRuntime;
        }

        public async Task<T> GetItemAsync<T>(string key)
        {
            var json = await this._jsRuntime.InvokeAsync<string>("localStorage.getItem", key);

            return json == null
                ? default
                : JsonConvert.DeserializeObject<T>(json);
        }

        public async Task SetItemAsync<T>(string key, T value)
        {
            await this._jsRuntime.InvokeVoidAsync("localStorage.setItem", key, JsonConvert.SerializeObject(value));
        }

        public async Task RemoveItemAsync(string key)
        {
            await this._jsRuntime.InvokeVoidAsync("localStorage.removeItem", key);
        }
    }
}