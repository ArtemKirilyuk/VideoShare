using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using VideoShare.BLL.DTO;

namespace VideoShare.PL.Models
{
    public class PlaylistConcreteViewModel
    {
        [ScaffoldColumn(false)]
        public int Id { get; set; }

        [Required(ErrorMessage ="Cannot create playlist without name.")]
        [MaxLength(15, ErrorMessage = "Max length of playlist's name is 15.")]
        public string Name { get; set; }

        public string Description { get; set; }

        public IEnumerable<VideoDTO> Videos { get; set; }
        public PlaylistConcreteViewModel()
        {
            Videos = new List<VideoDTO>();
        }
    }
}