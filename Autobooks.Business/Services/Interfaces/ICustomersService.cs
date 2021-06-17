using Autobooks.Business.Models;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Autobooks.Business.Services.Interfaces
{
    /// <summary>
    /// Customer Service
    /// </summary>
    public interface ICustomersService
    {
        /// <summary>
        /// Get customer by id
        /// </summary>
        /// <param name="id">Customer ID</param>
        /// <returns>Customer</returns>
        Task<Customer> GetById(int id);

        /// <summary>
        /// Get all customers
        /// </summary>
        /// <returns>All customers</returns>
        Task<IEnumerable<Customer>> GetAll();

        /// <summary>
        /// Add new customer
        /// </summary>
        /// <param name="entity">New customer</param>
        /// <param name="cancellationToken"></param>
        /// <returns>Newly added customer</returns>
        Task<Customer> Create(Customer entity, CancellationToken cancellationToken = default);

        /// <summary>
        /// Update customer info
        /// </summary>
        /// <param name="entity">Updated customer object</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task Update(Customer entity, CancellationToken cancellationToken = default);
    }
}
