using Autobooks.Business.Models;
using Autobooks.Business.Services.Interfaces;
using Autobooks.Data.Repositories.Interfaces;
using AutoMapper;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Autobooks.Business.Services
{
    /// <inheritdoc />
    public class CustomersService : ICustomersService
    {
        private readonly IGroceryRepository<Data.Models.Customer> _customersRepository;
        private readonly IMapper _mapper;

        public CustomersService(IGroceryRepository<Data.Models.Customer> customersRepository, IMapper mapper)
        {
            _customersRepository = customersRepository;
            _mapper = mapper;
        }

        /// <inheritdoc />
        public async Task<Customer> GetById(int id)
        {
            var customer = await _customersRepository.GetById(id);
            return _mapper.Map<Customer>(customer);
        }

        /// <inheritdoc />
        public async Task<IEnumerable<Customer>> GetAll()
        {
            var customers = await _customersRepository.GetAll();
            return _mapper.Map<IEnumerable<Customer>>(customers);
        }

        /// <inheritdoc />
        public async Task<Customer> Create(Customer entityDto, CancellationToken cancellationToken = default)
        {
            var entity = _mapper.Map<Data.Models.Customer>(entityDto);
            var addedEntity = await _customersRepository.Create(entity, cancellationToken);
            return _mapper.Map<Customer>(addedEntity);
        }

        /// <inheritdoc />
        public async Task Update(Customer entityDto, CancellationToken cancellationToken = default)
        {
            var entity = _mapper.Map<Data.Models.Customer>(entityDto);
            await _customersRepository.Update(entity, cancellationToken);
        }
    }
}
