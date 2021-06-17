using Autobooks.Business.Models;
using AutoMapper;

namespace Autobooks.Business.Automapper.Profiles
{
    public class AutobooksBusinessMappingProfile : Profile
    {
        public AutobooksBusinessMappingProfile()
        {
            CreateMap<Customer, Data.Models.Customer>().ReverseMap();
        }
    }
}
