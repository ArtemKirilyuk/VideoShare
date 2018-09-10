namespace VideoShare.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class LikesMigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ApplicationUserVideos",
                c => new
                    {
                        ApplicationUser_Id = c.String(nullable: false, maxLength: 128),
                        Video_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.ApplicationUser_Id, t.Video_Id })
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUser_Id, cascadeDelete: true)
                .ForeignKey("dbo.Videos", t => t.Video_Id, cascadeDelete: true)
                .Index(t => t.ApplicationUser_Id)
                .Index(t => t.Video_Id);


        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ApplicationUserVideos", "Video_Id", "dbo.Videos");
            DropForeignKey("dbo.ApplicationUserVideos", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropIndex("dbo.ApplicationUserVideos", new[] { "Video_Id" });
            DropIndex("dbo.ApplicationUserVideos", new[] { "ApplicationUser_Id" });
            DropTable("dbo.ApplicationUserVideos");
        }
    }
}
