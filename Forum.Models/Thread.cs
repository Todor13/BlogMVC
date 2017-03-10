using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Forum.Models
{
    public class Thread
    {
        private ICollection<Answer> answers;
         
        public Thread()
        {
            this.answers = new HashSet<Answer>();
        }

        public int Id { get; set; }
        public string Title { get; set; }

        [AllowHtml]
        public string Content { get; set; }
        public DateTime Published { get; set; }
        public string UserId { get; set; }
        public virtual User User { get; set; }
        public int SectionId { get; set; }
        public virtual Section Section { get; set; }
        public bool IsVisible { get; set; }

        public virtual ICollection<Answer> Answers
        {
            get { return this.answers; }
            set { this.answers = value; }
        }
    }
}
