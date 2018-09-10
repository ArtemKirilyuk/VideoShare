namespace VideoShare.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PlaylistDescription : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Playlists", "Description", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Playlists", "Description");
        }
    }
}
