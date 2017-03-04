using System.Collections.Generic;

namespace Forum.Models
{
    public class Section
    {
        private ICollection<Thread> threads;

        public Section()
        {
            this.threads = new HashSet<Thread>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Thread> Threads
        {
            get { return this.threads; }
            set { this.threads = value; }
        }
    }
}
