using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;
using System;

namespace TravelMeaning.Common.Helper
{
    public class Appsettings
    {
        static IConfiguration Configuration { get; set; }
        static Appsettings()
        {
            Configuration = new ConfigurationBuilder().Add(new JsonConfigurationSource
            {
                Path = "appsettings.json",
                ReloadOnChange = true
            }).Build();
        }

        public static string app(params string[] sectinons)
        {
            try
            {
                var val = string.Empty;
                for (int i = 0; i < sectinons.Length; i++)
                {
                    val += sectinons[i] + ":";
                }
                return Configuration[val.TrimEnd(':')];
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
