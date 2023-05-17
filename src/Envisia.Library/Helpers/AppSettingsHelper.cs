using Microsoft.Extensions.Configuration;

namespace Envisia.Library.Helpers
{
    public static class AppSettingsHelper
    {
        private static IConfiguration? _configuration;

        public static void Configure(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public static IConfiguration GetConfiguration() => _configuration ?? throw new NotFoundException("Configuration is not configured.");
    }
}
