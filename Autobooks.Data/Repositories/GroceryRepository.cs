using Autobooks.Data.DbContext.Interfaces;
using Autobooks.Data.Models;
using Autobooks.Data.Repositories.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Autobooks.Data.Repositories
{
    public class GroceryRepository<T> : IGroceryRepository<T> where T : BaseEntity
    {
        private readonly IGroceryDbContext _dbContext;
        public GroceryRepository(IGroceryDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        /// <inheritdoc />
        public Task<T> GetById(int id)
        {
            var result = _dbContext.Table<T>().FirstOrDefault(e => e.Id == id);
            return Task.FromResult(result);
        }

        /// <inheritdoc />
        public Task<List<T>> GetAll()
        {
            var result = _dbContext.Table<T>();
            return Task.FromResult(result);
        }

        /// <inheritdoc />
        public async Task<T> Create(T entity, CancellationToken cancellationToken = default)
        {
            var table = _dbContext.Table<T>();
            var nextId = (table.Max(e => e.Id) ?? 0) + 1;
            entity.Id = nextId;

            table.Add(entity);
            await _dbContext.SaveChanges(cancellationToken);
            return entity;
        }

        /// <inheritdoc />
        public async Task Update(T entity, CancellationToken cancellationToken = default)
        {
            if (!entity.Id.HasValue) throw new System.Exception("Entity does not have Id. Id is required to update.");

            var index = _dbContext.Table<T>().FindIndex(e => e.Id == entity.Id);
            _dbContext.Table<T>()[index] = entity;

            await _dbContext.SaveChanges(cancellationToken);
        }
    }
}
