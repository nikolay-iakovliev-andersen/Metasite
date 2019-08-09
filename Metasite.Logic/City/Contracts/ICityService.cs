using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Metasite.Logic.City.Contracts
{
    public interface ICityService
    {
        Task<IEnumerable<string>> GetCitiesAsync();
    }
}
