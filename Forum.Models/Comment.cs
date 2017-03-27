using System;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Forum.Models
{
    public class Comment
    {
        public int Id { get; set; }

        [AllowHtml]
        [Required]
        [StringLength(4000, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        public string Content { get; set; }
        public DateTime Published { get; set; }
        public int AnswerId { get; set; }
        public virtual Answer Answer { get; set; }
        public string UserId { get; set; }
        public virtual ApplicationUser User { get; set; }
        public DateTime? EditedOn { get; set; }
        public string EditedById { get; set; }
        public bool IsVisible { get; set; }
    }
}