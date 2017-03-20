namespace Forum.Data.Migrations
{
    using Models;
    using System.Data.Entity.Migrations;

    public sealed class Configuration : DbMigrationsConfiguration<ForumDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(ForumDbContext context)
        {
            context.Sections.AddOrUpdate(new Section() { Id = 1, Name = "Important" });
            context.SaveChanges();
        }
    }
}
