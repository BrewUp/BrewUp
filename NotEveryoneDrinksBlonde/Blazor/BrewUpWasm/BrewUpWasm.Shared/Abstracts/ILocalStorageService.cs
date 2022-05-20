namespace BrewUpWasm.Shared.Abstracts
{
    public interface ILocalStorageService
    {
        Task<T> GetItemAsync<T>(string key);

        Task SetItemAsync<T>(string key, T value);

        Task RemoveItemAsync(string key);
    }
}