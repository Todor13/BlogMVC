using Forum.Models;
using Forum.Services.Contracts;
using System;
using System.Linq;
using AutoMapper;

namespace Forum.Web.Areas.Forum.Models
{
    public class ThreadViewModel : IMapFrom<Thread>, IHaveCustomMappings
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime Published { get; set; }
        public ApplicationUser User { get; set; }
        public Section Section { get; set; }
        public int AnswersCount { get; set; }

        public void CreateMappings(IMapperConfigurationExpression configuration)
        {
            configuration.CreateMap<Thread, ThreadViewModel>()
                .ForMember(t => t.AnswersCount, opt => opt.MapFrom(t => t.Answers.Count(a => a.IsVisible == true)))
                .ReverseMap();  
        }
    }
}