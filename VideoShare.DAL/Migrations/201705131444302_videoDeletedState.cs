namespace VideoShare.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class videoDeletedState : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Videos", "IsDeleted", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Videos", "IsDeleted");
        }
    }
}
