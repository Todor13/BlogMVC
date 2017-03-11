using AutoMapper;

namespace Forum.Web.App_Start
{
    public interface IHaveCustomMappings
    {
        void CreateMappings(IMapperConfigurationExpression configuration);
    }
}