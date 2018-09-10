namespace VideoShare.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class VideoDescription : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Videos", "Description", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Videos", "Description");
        }
    }
}
