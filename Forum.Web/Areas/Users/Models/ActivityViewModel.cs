using System;

namespace Forum.Web.Areas.Users.Models
{
    public class ActivityViewModel
    {
        public int ThreadId { get; set; }
        public string ThreadTitle { get; set; }
        public string Content { get; set; }
        public DateTime Published { get; set; }
        public int AnswersCount { get; set; }
    }
}