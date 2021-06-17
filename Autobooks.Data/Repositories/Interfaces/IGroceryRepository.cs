using Autobooks.Data.Models;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Autobooks.Data.Repositories.Interfaces
{
    /// <summary>
    /// Generic repository for accessing grocery store data
    /// </summary>
    /// <typeparam name="T">Type of entity in data store</typeparam>
    public interface IGroceryRepository<T> where T : BaseEntity
    {
        /// <summary>
        /// Gets an entity of type <typeparamref name="T"/> by id
        /// </summary>
        /// <param name="id">Id of the entity</param>
        /// <returns>The matching entity or null</returns>
        Task<T> GetById(int id);

        /// <summary>
        /// Gets all entites of type <typeparamref name="T"/>
        /// </summary>
        /// <returns>Enumerable of all entities</returns>
        Task<List<T>> GetAll();

        /// <summary>
        /// Adds entity of type <typeparamref name="T"/> to data store
        /// </summary>
        /// <param name="entity">Entity to add</param>
        /// <param name="cancellationToken"></param>
        /// <returns>Added entity</returns>
        Task<T> Create(T entity, CancellationToken cancellationToken = default);

        /// <summary>
        /// Updates entity of type <typeparamref name="T"/> in data store
        /// </summary>
        /// <param name="entity">Updated entity</param>
        /// <param name="cancellationToken"></param>
        /// <returns>Task</returns>
        Task Update(T entity, CancellationToken cancellationToken = default);
    }
}
