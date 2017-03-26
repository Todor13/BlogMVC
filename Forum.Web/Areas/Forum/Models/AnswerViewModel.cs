using Forum.Models;
using Forum.Services.Contracts;
using System;
using AutoMapper;
using System.Collections.Generic;

namespace Forum.Web.Areas.Forum.Models
{
    public class AnswerViewModel : IMapFrom<Answer>, IHaveCustomMappings
    {
        private ICollection<CommentViewModel> comments;

        public AnswerViewModel()
        {
            this.comments = new HashSet<CommentViewModel>();
        }

        public int Id { get; set; }
        public string Content { get; set; }
        public DateTime Published { get; set; }
        public string UserId { get; set; }
        public string UserName { get; set; }
        public int ThreadId { get; set; }
        public DateTime? EditedOn { get; set; }
        public string EditedById { get; set; }

        public ICollection<CommentViewModel> Comments
        {
            get { return this.comments; }
            set { this.comments = value; }
        }

        public void CreateMappings(IMapperConfigurationExpression configuration)
        {
            configuration.CreateMap<Answer, AnswerViewModel>()
               .ForMember(a => a.UserName, opt => opt.MapFrom(a => a.User.UserName))
               .ReverseMap();
        }
    }
}