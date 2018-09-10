using Microsoft.AspNet.Identity.EntityFramework;
using System.Collections.Generic;

namespace VideoShare.DAL.Entities
{
    public class ApplicationUser : IdentityUser
    {
        public virtual ClientProfile ClientProfile { get; set; }

        public virtual ICollection<Comment> Comments { get; set; }
        public virtual ICollection<Playlist> Playlists { get; set; }
        public virtual ICollection<Video> Videos { get; set; }
        public virtual ICollection<Video> VideoLiked { get; set; }
        public ApplicationUser() : base()
        {
            Comments = new List<Comment>();
            Playlists = new List<Playlist>();
            Videos = new List<Video>();
            VideoLiked = new List<Video>();
        }
    }
}
