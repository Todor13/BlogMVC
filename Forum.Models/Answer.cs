using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

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

        [AllowHtml]
        [Required]
        [StringLength(4000, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        public string Content { get; set; }
        public DateTime Published { get; set; }
        public string UserId { get; set; }
        public virtual ApplicationUser User { get; set; }
        public int ThreadId { get; set; }
        public virtual Thread Thread { get; set; }
        public DateTime? EditedOn { get; set; }
        public string EditedById { get; set; }
        public bool IsVisible { get; set; }

        public virtual ICollection<Comment> Comments
        {
            get { return this.comments; }
            set { this.comments = value; }
        }
    }
}