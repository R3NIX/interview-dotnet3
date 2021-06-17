using Autobooks.API.Automapper;
using Autobooks.API.Controllers;
using Autobooks.API.Models;
using Autobooks.Business.Models;
using Autobooks.Business.Services.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace Autobooks.API.Tests
{
    public class CustomersControllerTests
    {
        private Customer mockCustomer1 = new Customer()
        {
            Id = 1,
            Name = "Amanda"
        };

        private Customer mockCustomer2 = new Customer()
        {
            Id = 2,
            Name = "Jake"
        };

        private Customer mockCustomer3 = new Customer()
        {
            Id = 3,
            Name = "Chelsea"
        };

        private List<Customer> mockCustomers;

        private readonly IMapper _mapper;
        private Mock<ICustomersService> _customersServiceMock = new Mock<ICustomersService>();

        public CustomersControllerTests()
        {
            var automapperConfig = new MapperConfiguration(cfg =>
            {
                cfg.AddAutobooksMappingProfiles();
            });

            mockCustomers = new List<Customer>() { mockCustomer1, mockCustomer2, mockCustomer3 };

            _mapper = new Mapper(automapperConfig);
        }


        [Fact]
        public async Task Should_Return_All_Customers()
        {
            _customersServiceMock.Setup(s => s.GetAll()).Returns(Task.FromResult((IEnumerable<Customer>)mockCustomers));

            var controller = new CustomersController(_customersServiceMock.Object, _mapper);
            var result = (ObjectResult)await controller.GetAllCustomers();
            Assert.Equal(mockCustomers, result.Value);
            Assert.Equal(200, result.StatusCode);
        }

        [Fact]
        public async Task Should_Return_Requested_Customer()
        {
            _customersServiceMock.Setup(s => s.GetById(1)).Returns(Task.FromResult(mockCustomer1));

            var controller = new CustomersController(_customersServiceMock.Object, _mapper);
            var result = (ObjectResult)await controller.GetCustomerById(1);
            Assert.Equal(mockCustomer1, result.Value);
            Assert.Equal(200, result.StatusCode);
        }

        [Fact]
        public async Task Should_Return_404_For_Invalid_Customer()
        {
            var controller = new CustomersController(_customersServiceMock.Object, _mapper);
            var result = (NotFoundResult)await controller.GetCustomerById(1);
            Assert.Equal(404, result.StatusCode);
        }

        [Fact]
        public async Task Should_Add_Customer_And_Return()
        {
            var postRequest = new CreateCustomerRequest() { Name = "Amanda" };
            _customersServiceMock.Setup(s => s.Create(It.Is<Customer>(c => c.Name == mockCustomer1.Name), default)).Returns(Task.FromResult(mockCustomer1));

            var controller = new CustomersController(_customersServiceMock.Object, _mapper);
            var result = (CreatedAtActionResult)await controller.CreateCustomer(postRequest, default);
            Assert.Equal(mockCustomer1, result.Value);
            Assert.Equal(201, result.StatusCode);
        }

        [Fact]
        public async Task Should_Update_And_Return_204()
        {
            _customersServiceMock.Setup(s => s.Update(mockCustomer2, default)).Returns(Task.CompletedTask);

            var controller = new CustomersController(_customersServiceMock.Object, _mapper);
            var result = (NoContentResult)await controller.UpdateCustomer(2, mockCustomer2, default);
            Assert.Equal(204, result.StatusCode);
        }
    }
}
