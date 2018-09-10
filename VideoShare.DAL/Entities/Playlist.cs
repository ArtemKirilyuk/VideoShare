using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VideoShare.DAL.Entities
{
    public class Playlist
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public virtual ApplicationUser ApplicationUser { get; set; }
        [ForeignKey("ApplicationUser")]
        public string ApplicationUserId { get; set; }

        public string Description { get; set; }
        public virtual ICollection<Video> Videos { get; set; }
        public Playlist()
        {
            Videos = new List<Video>();
        }
    }
}
