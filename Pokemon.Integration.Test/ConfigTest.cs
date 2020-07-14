using Microsoft.Extensions.Configuration;
using System.IO;

namespace Pokemon.Integration.Test
{
   public class ConfigTest
    {
        protected IConfigurationRoot _configurationRoot;
        protected ConfigTest()
        {
            _configurationRoot = CreateConfigFile();
        }

        private IConfigurationRoot CreateConfigFile()
        {
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", false).AddEnvironmentVariables();

            return builder.Build();
        }
    }
}
