using Forum.Models;
using System;
using System.Linq.Expressions;

namespace Forum.Web.Models
{
    public class CommentsViewModel
    {
        public static Expression<Func<Comment, CommentsViewModel>> FromComment
        {
            get
            {
                return comment => new CommentsViewModel
                {
                    Id = comment.Id,
                    Content = comment.Content,
                    Published = comment.Published,
                    UserId = comment.UserId,
                    AnswerId = comment.AnswerId,
                    EditedOn = comment.EditedOn,
                    EditedById = comment.EditedById,
                };
            }
        }

        public CommentsViewModel()
        {
        }

        public CommentsViewModel(Comment comment)
        {
            Id = comment.Id;
            Content = comment.Content;
            Published = comment.Published;
            UserId = comment.UserId;
            AnswerId = comment.AnswerId;
            EditedOn = comment.EditedOn;
            EditedById = comment.EditedById;
        }

        public int Id { get; set; }

        public string Content { get; set; }

        public DateTime Published { get; set; }

        public int AnswerId { get; set; }

        public string UserId { get; set; }

        public DateTime? EditedOn { get; set; }

        public string EditedById { get; set; }
    }
}