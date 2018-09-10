using System.Data.Entity;
using Microsoft.AspNet.Identity.EntityFramework;
using VideoShare.DAL.Entities;

namespace VideoShare.DAL.EF
{
    public class ApplicationContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationContext() : base() { }
        public ApplicationContext(string conectionString) : base(conectionString) { }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<ApplicationUser>().HasMany(c => c.VideoLiked)
            .WithMany(s => s.UsersLiked);
        }
        public DbSet<ClientProfile> ClientProfiles { get; set; }

        public DbSet<Video> Videos { get; set; }
        public DbSet<Playlist> Playlists { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Comment> Comments { get; set; }



    }
}
