namespace Forum.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ModelsUpdated : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Answers", "EditedOn", c => c.DateTime(nullable: true));
            AddColumn("dbo.Answers", "EditedById", c => c.String());
            AddColumn("dbo.Comments", "EditedOn", c => c.DateTime(nullable: true));
            AddColumn("dbo.Comments", "EditedById", c => c.String());
            AddColumn("dbo.Threads", "EditedOn", c => c.DateTime(nullable: true));
            AddColumn("dbo.Threads", "EditedById", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Threads", "EditedById");
            DropColumn("dbo.Threads", "EditedOn");
            DropColumn("dbo.Comments", "EditedById");
            DropColumn("dbo.Comments", "EditedOn");
            DropColumn("dbo.Answers", "EditedById");
            DropColumn("dbo.Answers", "EditedOn");
        }
    }
}
