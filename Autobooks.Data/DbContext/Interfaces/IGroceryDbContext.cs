using Autobooks.Data.Models;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Autobooks.Data.DbContext.Interfaces
{
    /// <summary>
    /// Grocery DB Context
    /// </summary>
    public interface IGroceryDbContext
    {
        /// <summary>
        /// Gets table of <typeparamref name="T"/> type from database
        /// </summary>
        /// <typeparam name="T">type of table entities</typeparam>
        /// <returns>Enumerable of data</returns>
        List<T> Table<T>() where T : BaseEntity;

        /// <summary>
        /// Saves changes to the file
        /// </summary>
        /// <param name="cancellationToken">Cancellation Token</param>
        /// <returns>Task</returns>
        Task SaveChanges(CancellationToken cancellationToken = default);
    }
}
