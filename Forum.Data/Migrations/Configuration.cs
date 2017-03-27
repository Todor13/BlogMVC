using Forum.Models;
using System.Data.Entity.Migrations;

namespace Forum.Data.Migrations
{

    public sealed class Configuration : DbMigrationsConfiguration<ForumDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(ForumDbContext context)
        {
            context.Sections.AddOrUpdate(new Section() { Id = 1, Name = "Important" });

            context.Roles.AddOrUpdate(new ApplicationRole() { Id = "2bb14d69-2c29-424f-9c40-49b544729f73", Name = "Admin" });
            context.Roles.AddOrUpdate(new ApplicationRole() { Id = "95535a9b-6b84-461d-bff4-c205643de6c9", Name = "Moderator" });

            context.SaveChanges();
        }
    }
}
