using System;
using System.Threading.Tasks;

namespace Metasite.DataStorage.Contracts
{
    public interface IUnitOfWork : IDisposable
    {
        IWeatherRepository Weather { get; }

        Task CommitAsync();
    }
}
