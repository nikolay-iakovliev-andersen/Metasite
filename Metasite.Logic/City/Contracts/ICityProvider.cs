using System.Collections.Generic;
using System.Threading.Tasks;

namespace Metasite.Logic.City.Contracts
{
    internal interface ICityProvider
    {
        Task<IEnumerable<string>> GetCitiesAsync();
    }
}
