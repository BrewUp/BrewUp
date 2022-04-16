namespace BrewUpWasm.Shared.JsonModel
{
    public class TokenJson
    {
        public string AccessToken { get; set; } = string.Empty;

        public string Platforms { get; set; } = string.Empty;
        public string Company { get; set; } = string.Empty;

        public int Expiration { get; set; }
        public DateTime ValidFrom { get; set; } = DateTime.MinValue;
        public DateTime ValidTo { get; set; } = DateTime.MinValue;
    }
}