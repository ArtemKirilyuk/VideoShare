using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System;

namespace VideoShare.DAL.Entities
{
    public class Comment
    {
        public int Id { get; set; }

        public string Text { get; set; }

        [ForeignKey("ApplicationUser")]
        public string ApplicationUserId { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }

        [ForeignKey("Video")]
        public int VideoId { get; set; }
        public virtual Video Video { get; set; }
    }
}
