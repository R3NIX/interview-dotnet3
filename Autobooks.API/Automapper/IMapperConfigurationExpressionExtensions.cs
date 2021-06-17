using Autobooks.API.Automapper.Profiles;
using Autobooks.Business.Automapper;
using AutoMapper;

namespace Autobooks.API.Automapper
{
    public static class IMapperConfigurationExpressionExtensions
    {
        public static IMapperConfigurationExpression AddAutobooksMappingProfiles(this IMapperConfigurationExpression cfg)
        {
            cfg.AddAutobooksBusinessMappingProfiles();
            cfg.AddProfile<AutobooksAPIMappingProfile>();
            return cfg;
        }
    }
}
