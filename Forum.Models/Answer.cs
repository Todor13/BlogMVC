using System;
using System.Collections.Generic;

namespace Forum.Models
{
    public class Answer
    {
        private ICollection<Comment> comments;

        public Answer()
        {
            this.comments = new HashSet<Comment>();
        }

        public int Id { get; set; }
        public string Content { get; set; }
        public DateTime Published { get; set; }
        public string UserId { get; set; }
        public virtual User User { get; set; }
        public int ThreadId { get; set; }
        public virtual Thread Thread { get; set; }
        public bool IsVisible { get; set; }

        public virtual ICollection<Comment> Comments
        {
            get { return this.comments; }
            set { this.comments = value; }
        }
    }
}