using Forum.Models;
using Forum.Services.Contracts;
using AutoMapper;
using Forum.Web.Common;

namespace Forum.Web.Models
{
    public class IndexPageThreadViewModel : IMapFrom<Thread>, IHaveCustomMappings
    {
        public int Id { get; set; }
        public string Title { get; set; }

        public void CreateMappings(IMapperConfigurationExpression configuration)
        {
            configuration.CreateMap<Thread, IndexPageThreadViewModel>()
               .ForMember(a => a.Title, opt => opt.MapFrom(a => a.Title.Substring(0, WebConstants.IndexPageTitleSubstring)))
               .ReverseMap();
        }
    }
}