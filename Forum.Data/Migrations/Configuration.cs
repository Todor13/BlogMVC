namespace Forum.Data.Migrations
{
    using System.Data.Entity.Migrations;

    public sealed class Configuration : DbMigrationsConfiguration<ForumDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(ForumDbContext context)
        {
        }
    }
}
