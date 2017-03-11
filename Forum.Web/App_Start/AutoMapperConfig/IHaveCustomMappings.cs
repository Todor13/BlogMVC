using AutoMapper;

namespace Forum.Web.App_Start
{
    internal interface IHaveCustomMappings
    {
        void CreateMappings(IMapperConfigurationExpression configuration);
    }
}