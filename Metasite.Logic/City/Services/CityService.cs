using Metasite.Logic.Api.Exceptions;
using Metasite.Logic.City.Contracts;
using Metasite.Logic.City.Exceptions;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Metasite.Logic.City.Services
{
    internal sealed class CityService : ICityService
    {
        private readonly ICityProvider cityProvider;
        private readonly ILogger<CityService> logger;

        public CityService(ICityProvider cityProvider, ILoggerFactory loggerFactory)
        {
            this.cityProvider = cityProvider;
            this.logger = loggerFactory.CreateLogger<CityService>();
        }

        public async Task<IEnumerable<string>> GetCitiesAsync()
        {
            IEnumerable<string> cities;
            try
            {
                cities = await this.cityProvider.GetCitiesAsync();
                this.logger.LogInformation($"Successfully got {cities.Count()} cities.");
            }
            catch (MetasiteProviderException exception)
            {
                this.logger.LogError(exception, $"Unable to get cities.");
                throw new CityServiceException($"Could not fetch cities information", exception);
            }

            return cities;
        }
    }
}
