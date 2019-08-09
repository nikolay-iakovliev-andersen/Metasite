using System.Collections.Generic;
using System.Threading.Tasks;

namespace Metasite.Cli.Extensions
{
    static class EnumerableExtensions
    {
        public static async Task<IEnumerable<T>> WhenAll<T>(this IEnumerable<Task<T>> tasks)
        {
            return await Task.WhenAll(tasks);
        }
    }
}
