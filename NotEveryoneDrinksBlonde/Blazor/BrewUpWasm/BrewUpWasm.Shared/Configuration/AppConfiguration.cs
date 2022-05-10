namespace BrewUpWasm.Shared.Configuration
{
    public class AppConfiguration
    {
        public string Platform { get; set; } = string.Empty;
        public string AuthenticationApiUri { get; set; } = string.Empty;
        public string ProductionApiUri { get; set; } = string.Empty;
        public string PubsApiUri { get; set; } = string.Empty;
    }
}