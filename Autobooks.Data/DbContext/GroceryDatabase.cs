using Autobooks.Data.Models;
using System.Collections.Generic;

namespace Autobooks.Data.DbContext
{
    internal class GroceryDatabase
    {
        public GroceryDatabase()
        {
            Customers = new List<Customer>();
        }

        public List<Customer> Customers { get; set; }
    }
}
