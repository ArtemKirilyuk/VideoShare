namespace VideoShare.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SomeMigration : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Videos", "Playlist_Id", "dbo.Playlists");
            DropIndex("dbo.Videos", new[] { "Playlist_Id" });
            RenameColumn(table: "dbo.Videos", name: "Playlist_Id", newName: "PlaylistId");
            AddColumn("dbo.Comments", "VideoId", c => c.Int(nullable: false));
            AlterColumn("dbo.Videos", "PlaylistId", c => c.Int(nullable: false));
            CreateIndex("dbo.Videos", "PlaylistId");
            CreateIndex("dbo.Comments", "VideoId");
            AddForeignKey("dbo.Comments", "VideoId", "dbo.Videos", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Videos", "PlaylistId", "dbo.Playlists", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Videos", "PlaylistId", "dbo.Playlists");
            DropForeignKey("dbo.Comments", "VideoId", "dbo.Videos");
            DropIndex("dbo.Comments", new[] { "VideoId" });
            DropIndex("dbo.Videos", new[] { "PlaylistId" });
            AlterColumn("dbo.Videos", "PlaylistId", c => c.Int());
            DropColumn("dbo.Comments", "VideoId");
            RenameColumn(table: "dbo.Videos", name: "PlaylistId", newName: "Playlist_Id");
            CreateIndex("dbo.Videos", "Playlist_Id");
            AddForeignKey("dbo.Videos", "Playlist_Id", "dbo.Playlists", "Id");
        }
    }
}
