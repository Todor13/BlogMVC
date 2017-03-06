using System.Collections.Generic;

namespace Forum.Models
{
    public class User
    {
        private ICollection<Thread> threads;
        private ICollection<Answer> answers;
        private ICollection<Comment> comments;

        public User()
        {
            this.threads = new HashSet<Thread>();
            this.answers = new HashSet<Answer>();
            this.comments = new HashSet<Comment>();
        }

        public string Id { get; set; }
        public string Name { get; set; }
    }
}