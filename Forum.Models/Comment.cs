using System;
using System.Web.Mvc;

namespace Forum.Models
{
    public class Comment
    {
        public int Id { get; set; }

        [AllowHtml]
        public string Content { get; set; }
        public DateTime Published { get; set; }
        public int AnswerId { get; set; }
        public virtual Answer Answer { get; set; }
        public string UserId { get; set; }
        public virtual ApplicationUser User { get; set; }
        public bool IsVisible { get; set; }
    }
}