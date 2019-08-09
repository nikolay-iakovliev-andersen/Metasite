using Metasite.DataStorage.Contracts;
using System.Threading.Tasks;

namespace Metasite.DataStorage
{
    sealed class FakeUnitOfWork : IUnitOfWork
    {
        public IWeatherRepository Weather { get; }

        public FakeUnitOfWork(IWeatherRepository repository)
        {
            Weather = repository;
        }

        public Task CommitAsync()
        {
            return Task.CompletedTask;
        }

        public void Dispose()
        {

        }
    }
}
