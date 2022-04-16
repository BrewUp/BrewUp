namespace BrewUpWasm.Shared.Configuration
{
    public class AppConfiguration
    {
        public string Platform { get; set; } = string.Empty;
        public string ApiAuthenticationUri { get; set; } = string.Empty;
        public string IoTPlatformUri { get; set; } = string.Empty;
        public string IotCoreApi { get; set; } = string.Empty;
        public string MasterDataApi { get; set; } = string.Empty;
        public string ProductionApi { get; set; } = string.Empty;
    }
}