using Forum.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Forum.Web.Models
{
    public class ThreadViewModel
    {
        public static Expression<Func<Thread, ThreadViewModel>> FromThread
        {
            get
            {
                return thread => new ThreadViewModel
                {
                    Id = thread.Id,
                    Title = thread.Title,
                    Content = thread.Content,
                    Published = thread.Published,
                    UserId = thread.UserId,
                    EditedOn = thread.EditedOn,
                    EditedById = thread.EditedById,
                    SectionId = thread.SectionId,
                    Answers = thread.Answers.AsQueryable().Select(AnswerViewModel.FromAnswer)
                };
            }
        }

        public ThreadViewModel()
        {
        }

        public ThreadViewModel(Thread thread)
        {
            Id = thread.Id;
            Title = thread.Title;
            Content = thread.Content;
            Published = thread.Published;
            UserId = thread.UserId;
            SectionId = thread.SectionId;
            EditedOn = thread.EditedOn;
            EditedById = thread.EditedById;
            Answers = thread.Answers.Where(a => a.IsVisible == true).Select(x => new AnswerViewModel(x));
        }

        public int Id { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }

        public DateTime Published { get; set; }

        public string UserId { get; set; }

        public int SectionId { get; set; }

        public DateTime? EditedOn { get; set; }

        public string EditedById { get; set; }

        public IEnumerable<AnswerViewModel> Answers { get; set; }
    }
}