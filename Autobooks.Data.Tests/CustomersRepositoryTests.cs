using Autobooks.Data.DbContext.Interfaces;
using Autobooks.Data.Models;
using Autobooks.Data.Repositories;
using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace Autobooks.Data.Tests
{
    public class CustomersRepositoryTests
    {
        private Customer mockCustomer1 = new Customer()
        {
            Id = 1,
            Name = "Janine"
        };

        private Customer mockCustomer2 = new Customer()
        {
            Id = 2,
            Name = "Marc"
        };

        private Customer mockCustomer3 = new Customer()
        {
            Id = 3,
            Name = "Kaitlyn"
        };

        private List<Customer> mockCustomers;

        private readonly Mock<IGroceryDbContext> _mockDbContext = new Mock<IGroceryDbContext>();


        public CustomersRepositoryTests()
        {
            mockCustomers = new List<Customer>() { mockCustomer1, mockCustomer2, mockCustomer3 };
            _mockDbContext.Setup(c => c.Table<Customer>()).Returns(mockCustomers);
        }

        [Fact]
        public async Task Should_Return_All_Customers()
        {
            var repository = new GroceryRepository<Customer>(_mockDbContext.Object);
            var result = await repository.GetAll();
            Assert.Equal(mockCustomers, result);
        }

        [Fact]
        public async Task Should_Return_Customer()
        {
            var repository = new GroceryRepository<Customer>(_mockDbContext.Object);
            var result = await repository.GetById(1);
            Assert.Equal(mockCustomer1, result);
        }

        [Fact]
        public async Task Should_Create_And_Return_Customer()
        {
            var newCustomer = new Customer() { Name = "Jose" };

            var repository = new GroceryRepository<Customer>(_mockDbContext.Object);
            var result = await repository.Create(newCustomer, default);
            Assert.Equal(newCustomer, result);
        }

        [Fact]
        public async Task Should_Update_Customer()
        {
            var mockUpdateCustomer = new Customer() { Id = 1, Name = "Jack" };

            var repository = new GroceryRepository<Customer>(_mockDbContext.Object);
            await repository.Update(mockUpdateCustomer, default);
            Assert.Contains(mockUpdateCustomer, mockCustomers);
        }
    }
}
