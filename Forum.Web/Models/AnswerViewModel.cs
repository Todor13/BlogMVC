﻿using Forum.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Forum.Web.Models
{
    public class AnswerViewModel
    {
        public static Expression<Func<Answer, AnswerViewModel>> FromAnswer
        {
            get
            {
                return answer => new AnswerViewModel
                {
                    Id = answer.Id,
                    Content = answer.Content,
                    Published = answer.Published,
                    UserId = answer.UserId,
                    ThreadId = answer.ThreadId,
                    EditedOn = answer.EditedOn,
                    EditedById = answer.EditedById,
                    Comments = answer.Comments.AsQueryable().Select(CommentViewModel.FromComment)
                };
            }
        }

        public AnswerViewModel()
        {
        }

        public AnswerViewModel(Answer answer)
        {
            Id = answer.Id;
            Content = answer.Content;
            Published = answer.Published;
            UserId = answer.UserId;
            ThreadId = answer.ThreadId;
            EditedOn = answer.EditedOn;
            EditedById = answer.EditedById;
            Comments = answer.Comments.Where(a => a.IsVisible == true).Select(x => new CommentViewModel(x));
        }

        public int Id { get; set; }

        public string Content { get; set; }

        public DateTime Published { get; set; }

        public string UserId { get; set; }

        public int ThreadId { get; set; }

        public DateTime? EditedOn { get; set; }

        public string EditedById { get; set; }

        public IEnumerable<CommentViewModel> Comments { get; set; }
    }
}