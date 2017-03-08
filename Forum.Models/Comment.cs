using System;

namespace Forum.Models
{
    public class Comment
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public DateTime Published { get; set; }
        public int AnswerId { get; set; }
        public virtual Answer Answer { get; set; }
        public string UserId { get; set; }
        public virtual User User { get; set; }
        public bool IsVisible { get; set; }
    }
}