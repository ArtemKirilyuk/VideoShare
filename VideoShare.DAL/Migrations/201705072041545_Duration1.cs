namespace VideoShare.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Duration1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Videos", "Duration", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Videos", "Duration");
        }
    }
}
