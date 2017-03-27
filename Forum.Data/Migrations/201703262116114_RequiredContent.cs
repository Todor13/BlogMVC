namespace Forum.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RequiredContent : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Answers", "Content", c => c.String(nullable: false, maxLength: 4000));
            AlterColumn("dbo.Comments", "Content", c => c.String(nullable: false, maxLength: 4000));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Comments", "Content", c => c.String());
            AlterColumn("dbo.Answers", "Content", c => c.String());
        }
    }
}
