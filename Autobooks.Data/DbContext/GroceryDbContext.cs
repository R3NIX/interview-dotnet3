using Autobooks.Data.Attributes;
using Autobooks.Data.DbContext.Interfaces;
using Autobooks.Data.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

namespace Autobooks.Data.DbContext
{
    /// <inheritdoc />
    public class GroceryDbContext : IGroceryDbContext
    {
        private readonly string _databasePath;
        
        private bool _databaseLoaded = false;

        private GroceryDatabase _database;
        private GroceryDatabase database
        {
            get
            {
                LoadDatabase();
                return _database;
            }
            set
            {
                _database = value;
            }
        }

        public List<Customer> Customers
        {
            get { return database.Customers; }
            set { database.Customers = value; }
        }


        public GroceryDbContext(string databasePath)
        {
            if (!File.Exists(databasePath))
            {
                throw new Exception($"{databasePath}: Grocery database file does not exist");
            }
            _databasePath = databasePath;
            LoadDatabase();
        }

        /// <inheritdoc />
        public List<T> Table<T>() where T : BaseEntity
        {
            var tableName = ((TableAttribute)typeof(T).GetCustomAttributes(typeof(TableAttribute), true)?.FirstOrDefault())?.Name;
            if (string.IsNullOrEmpty(tableName))
            {
                throw new Exception($"Could not find table name for class {typeof(T).Name}");
            }
            var table = (List<T>)typeof(GroceryDatabase).GetProperty(tableName, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance).GetValue(database);
            return table;
        }

        /// <inheritdoc />
        public async Task SaveChanges(CancellationToken cancellationToken = default)
        {
            await File.WriteAllTextAsync(_databasePath, JsonConvert.SerializeObject(database), cancellationToken);
            ForceLoadDatabase();
        }

        /// <summary>
        /// Loads the db from the file path
        /// </summary>
        private void LoadDatabase()
        {
            if (!_databaseLoaded)
            {
                var databaseText = File.ReadAllText(_databasePath);

                _database = JsonConvert.DeserializeObject<GroceryDatabase>(databaseText) ?? new GroceryDatabase();
                _databaseLoaded = true;
            }
        }

        /// <summary>
        /// Force load the db
        /// </summary>
        private void ForceLoadDatabase()
        {
            _databaseLoaded = false;
            LoadDatabase();
        }
    }
}
