using Autobooks.API.Models;
using Autobooks.Business.Models;
using Autobooks.Business.Services.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Autobooks.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CustomersController : Controller
    {
        private readonly ICustomersService _customersService;
        private readonly IMapper _mapper;

        public CustomersController(ICustomersService customersService, IMapper mapper)
        {
            _customersService = customersService;
            _mapper = mapper;
        }

        /// <summary>
        /// Retreive all grocery store customers.
        /// </summary>
        /// <returns>List of customers</returns>
        [HttpGet("")]
        [ProducesResponseType(typeof(IEnumerable<Customer>), 200)]
        public async Task<IActionResult> GetAllCustomers()
        {
            var customers = await _customersService.GetAll();
            return Ok(customers);
        }

        /// <summary>
        /// Get customer by id
        /// </summary>
        /// <param name="customerId">Customers Id</param>
        /// <returns>Customer match that is found, or message saying customer could not be found</returns>
        [HttpGet("{customerId}")]
        [ProducesResponseType(typeof(Customer), 200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetCustomerById(int customerId)
        {
            var customer = await _customersService.GetById(customerId);
            if (customer == null)
            {
                return NotFound();
            }
            return Ok(customer);
        }

        /// <summary>
        /// Add customer to grocery store database
        /// </summary>
        /// <param name="addCustomerRequest">New customers info</param>
        /// <param name="cancellationToken"></param>
        /// <returns>Newly created customers information</returns>
        [HttpPost("")]
        [ProducesResponseType(typeof(Customer), 201)]
        public async Task<IActionResult> CreateCustomer(CreateCustomerRequest createCustomerRequest, CancellationToken cancellationToken = default)
        {
            var customerDto = _mapper.Map<Customer>(createCustomerRequest);
            var customer = await _customersService.Create(customerDto, cancellationToken);
            return CreatedAtAction(nameof(CreateCustomer), new { customerId = customer.Id }, customer);
        }

        /// <summary>
        /// Update customer by id
        /// </summary>
        /// <param name="customerId">Customer who needs information updated</param>
        /// <param name="customerDto">Updated customer information</param>
        /// <param name="cancellationToken"></param>
        /// <returns>204 response on success, or 400 if match is not found</returns>
        [HttpPut("{customerId}")]
        [ProducesResponseType(204)]
        public async Task<IActionResult> UpdateCustomer(int customerId, Customer customer, CancellationToken cancellationToken = default)
        {
            if (customerId != customer.Id)
            {
                return BadRequest("Customer match not found. Update unsuccessful");
            }
            await _customersService.Update(customer, cancellationToken);
            return NoContent();
        }
    }
}
