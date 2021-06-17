using Autobooks.Business.Automapper.Profiles;
using AutoMapper;

namespace Autobooks.Business.Automapper
{
    public static class IMapperConfigurationExpressionExtensions
    {
        public static IMapperConfigurationExpression AddAutobooksBusinessMappingProfiles(this IMapperConfigurationExpression cfg)
        {
            cfg.AddProfile<AutobooksBusinessMappingProfile>();
            return cfg;
        }
    }
}
