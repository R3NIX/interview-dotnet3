using Autobooks.API.Models;
using Autobooks.Business.Models;
using AutoMapper;

namespace Autobooks.API.Automapper.Profiles
{
    public class AutobooksAPIMappingProfile : Profile
    {
        public AutobooksAPIMappingProfile()
        {
            CreateMap<CreateCustomerRequest, Customer>();
        }
    }
}
