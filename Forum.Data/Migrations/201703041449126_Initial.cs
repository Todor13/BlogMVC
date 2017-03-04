namespace Forum.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Answers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Content = c.String(),
                        Published = c.DateTime(nullable: false),
                        UserId = c.Int(nullable: false),
                        ThreadId = c.Int(nullable: false),
                        IsVisible = c.Boolean(nullable: false),
                        User_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Threads", t => t.ThreadId, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.User_Id)
                .Index(t => t.ThreadId)
                .Index(t => t.User_Id);
            
            CreateTable(
                "dbo.Comments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Content = c.String(),
                        Published = c.DateTime(nullable: false),
                        AnswerId = c.Int(nullable: false),
                        UserId = c.Int(nullable: false),
                        IsVisible = c.Boolean(nullable: false),
                        User_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Answers", t => t.AnswerId, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.User_Id)
                .Index(t => t.AnswerId)
                .Index(t => t.User_Id);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        UserName = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Threads",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        Content = c.String(),
                        Published = c.DateTime(nullable: false),
                        UserId = c.String(maxLength: 128),
                        SectionId = c.Int(nullable: false),
                        IsVisible = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Sections", t => t.SectionId, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.UserId)
                .Index(t => t.UserId)
                .Index(t => t.SectionId);
            
            CreateTable(
                "dbo.Sections",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Answers", "User_Id", "dbo.Users");
            DropForeignKey("dbo.Threads", "UserId", "dbo.Users");
            DropForeignKey("dbo.Threads", "SectionId", "dbo.Sections");
            DropForeignKey("dbo.Answers", "ThreadId", "dbo.Threads");
            DropForeignKey("dbo.Comments", "User_Id", "dbo.Users");
            DropForeignKey("dbo.Comments", "AnswerId", "dbo.Answers");
            DropIndex("dbo.Threads", new[] { "SectionId" });
            DropIndex("dbo.Threads", new[] { "UserId" });
            DropIndex("dbo.Comments", new[] { "User_Id" });
            DropIndex("dbo.Comments", new[] { "AnswerId" });
            DropIndex("dbo.Answers", new[] { "User_Id" });
            DropIndex("dbo.Answers", new[] { "ThreadId" });
            DropTable("dbo.Sections");
            DropTable("dbo.Threads");
            DropTable("dbo.Users");
            DropTable("dbo.Comments");
            DropTable("dbo.Answers");
        }
    }
}
