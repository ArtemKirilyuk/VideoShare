using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VideoShare.DAL.Entities
{
    public class Video
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int Views { get; set; }

        public string Path { get; set; }

        public string Duration { get; set; }

        public string Description { get; set; }

        public bool IsDeleted { get; set; }

        [ForeignKey("ApplicationUser")]
        public string ApplicationUserId { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }
        public virtual Playlist Playlist { get; set; }
        public int PlaylistId { get; set; }

        public virtual ICollection<ApplicationUser> UsersLiked { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
        public virtual ICollection<Category> Categories { get; set; }
        public Video()
        {
            Categories = new List<Category>();
            Comments = new List<Comment>();
            UsersLiked = new List<ApplicationUser>();
        }
    }
}
