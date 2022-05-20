namespace BrewUpWasm.Production.Shared.Abstracts
{
    public interface IHttpService
    {
        Task<byte[]> DownloadAsync(string uri);
        Task<T> Get<T>(string uri);
        Task<string> GetSettings<T>(string uri);

        Task<T> Post<T>(string uri, object value);
        Task Post(string uri, object value);
        Task PostAsFormData(string uri, MultipartFormDataContent form);

        Task<T> Put<T>(string uri, object value);
        Task Put(string uri, object value);

        Task<T> Patch<T>(string uri, object value);
        Task Patch(string uri, object value);

        Task Delete(string uri, object value);
    }
}