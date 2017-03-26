using Forum.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Forum.Web.Models
{
    public class ThreadsViewModel
    {
        public static Expression<Func<Thread, ThreadsViewModel>> FromThread
        {
            get
            {
                return thread => new ThreadsViewModel
                {
                    Id = thread.Id,
                    Title = thread.Title,
                    Content = thread.Content,
                    Published = thread.Published,
                    UserId = thread.UserId,
                    EditedOn = thread.EditedOn,
                    EditedById = thread.EditedById,
                    SectionId = thread.SectionId,
                    Answers = thread.Answers.AsQueryable().Select(AnswersViewModel.FromAnswer)
                };
            }
        }

        public ThreadsViewModel()
        {
        }

        public ThreadsViewModel(Thread thread)
        {
            Id = thread.Id;
            Title = thread.Title;
            Content = thread.Content;
            Published = thread.Published;
            UserId = thread.UserId;
            SectionId = thread.SectionId;
            EditedOn = thread.EditedOn;
            EditedById = thread.EditedById;
            Answers = thread.Answers.Where(a => a.IsVisible == true).Select(x => new AnswersViewModel(x));
        }

        public int Id { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }

        public DateTime Published { get; set; }

        public string UserId { get; set; }

        public int SectionId { get; set; }

        public DateTime? EditedOn { get; set; }

        public string EditedById { get; set; }

        public IEnumerable<AnswersViewModel> Answers { get; set; }
    }
}