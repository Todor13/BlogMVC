using Forum.Models;
using Forum.Services.Contracts;
using System;
using AutoMapper;

namespace Forum.Web.Areas.Forum.Models
{
    public class CommentViewModel : IMapFrom<Comment>, IHaveCustomMappings
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public DateTime Published { get; set; }
        public int AnswerId { get; set; }
        public string UserId { get; set; }
        public string UserName { get; set; }
        public DateTime? EditedOn { get; set; }
        public string EditedById { get; set; }

        public void CreateMappings(IMapperConfigurationExpression configuration)
        {
            configuration.CreateMap<Comment, CommentViewModel>()
               .ForMember(a => a.UserName, opt => opt.MapFrom(a => a.User.UserName))
               .ReverseMap();
        }
    }
}