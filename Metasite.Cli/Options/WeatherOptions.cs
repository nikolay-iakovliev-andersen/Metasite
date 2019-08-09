using CommandLine;
using System.Collections.Generic;

namespace Metasite.Cli.Options
{
    [Verb("weather", HelpText = "Get & save current weather every 30 seconds")]
    class WeatherOptions
    {
        [Option("city", Required = true, HelpText = "Comma separated list of cities", Separator = ',')]
        public IEnumerable<string> Cities { get; set; }
    }
}
