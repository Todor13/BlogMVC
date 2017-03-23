using Forum.Models;
using System;
using System.Linq.Expressions;

namespace Forum.Web.Models
{
    public class CommentViewModel
    {
        public static Expression<Func<Comment, CommentViewModel>> FromComment
        {
            get
            {
                return comment => new CommentViewModel
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

        public CommentViewModel()
        {
        }

        public CommentViewModel(Comment comment)
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