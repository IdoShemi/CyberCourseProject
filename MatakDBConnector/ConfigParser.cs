using System.IO;
using Newtonsoft.Json;

namespace MatakDBConnector
{
    internal static class ConfigParser
    {

        static Config config = JsonConvert.DeserializeObject<Config>(File.ReadAllText(@"DbConfig.json"));
        private static string connString = string.Format("Host={0};Username={1};Password={2};Database={3}", config.Host,
            config.Username, config.Password, config.Database);

        public static string ConnString => connString;
    }
}