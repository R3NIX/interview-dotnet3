using Autobooks.Business.Automapper;
using Autobooks.Business.Models;
using Autobooks.Business.Services;
using Autobooks.Data.Repositories.Interfaces;
using AutoMapper;
using Moq;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace Autobooks.Business.Tests
{
    public class CustomersServiceTests
    {
        private Data.Models.Customer mockCustomer1 = new Data.Models.Customer()
        {
            Id = 1,
            Name = "Janine"
        };

        private Data.Models.Customer mockCustomer2 = new Data.Models.Customer()
        {
            Id = 2,
            Name = "Marc"
        };

        private Data.Models.Customer mockCustomer3 = new Data.Models.Customer()
        {
            Id = 3,
            Name = "Kaitlyn"
        };

        private List<Data.Models.Customer> mockCustomers;

        private readonly IMapper _mapper;
        private Mock<IGroceryRepository<Data.Models.Customer>> _customerRepositoryMock = new Mock<IGroceryRepository<Data.Models.Customer>>();

        public CustomersServiceTests()
        {
            var automapperConfig = new MapperConfiguration(cfg =>
            {
                cfg.AddAutobooksBusinessMappingProfiles();
            });

            mockCustomers = new List<Data.Models.Customer>() { mockCustomer1, mockCustomer2, mockCustomer3 };

            _mapper = new Mapper(automapperConfig);
        }

        [Fact]
        public async Task Should_Return_All_Customers()
        {
            _customerRepositoryMock.Setup(r => r.GetAll()).Returns(Task.FromResult(mockCustomers));

            var service = new CustomersService(_customerRepositoryMock.Object, _mapper);
            var result = await service.GetAll();

            // Compare Json strings since references will not equal
            Assert.Equal(JsonConvert.SerializeObject(_mapper.Map<List<Customer>>(mockCustomers)), JsonConvert.SerializeObject(result));
        }

        [Fact]
        public async Task Should_Return_Customer()
        {
            _customerRepositoryMock.Setup(r => r.GetById(1)).Returns(Task.FromResult(mockCustomer2));

            var service = new CustomersService(_customerRepositoryMock.Object, _mapper);
            var result = await service.GetById(1);

            // Compare Json strings since references will not equal
            Assert.Equal(JsonConvert.SerializeObject(_mapper.Map<Customer>(mockCustomer2)), JsonConvert.SerializeObject(result));
        }

        [Fact]
        public async Task Should_Create_And_Return_Customer()
        {
            var mockNewCustomer = new Data.Models.Customer() { Name = "Chelsea" };
            _customerRepositoryMock.Setup(r => r.Create(It.Is<Data.Models.Customer>(c => c.Name == mockNewCustomer.Name), default)).Returns(Task.FromResult(mockCustomer3));

            var service = new CustomersService(_customerRepositoryMock.Object, _mapper);
            var result = await service.Create(_mapper.Map<Customer>(mockNewCustomer), default);

            // Compare Json strings since references will not equal
            Assert.Equal(JsonConvert.SerializeObject(_mapper.Map<Customer>(mockCustomer3)), JsonConvert.SerializeObject(result));
        }

        [Fact]
        public async Task Should_Update_Customer()
        {
            _customerRepositoryMock.Setup(r => r.Update(It.Is<Data.Models.Customer>(c => c.Id == mockCustomer1.Id), default)).Returns(Task.CompletedTask);

            var service = new CustomersService(_customerRepositoryMock.Object, _mapper);
            var mockUpdateCustomer = _mapper.Map<Customer>(mockCustomer1);
            await service.Update(mockUpdateCustomer, default);

            _customerRepositoryMock.Verify(r => r.Update(It.Is<Data.Models.Customer>(c => c.Id == mockCustomer1.Id), default), Times.Once());
        }
    }
}
