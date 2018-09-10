using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using VideoShare.BLL.DTO;

namespace VideoShare.PL.Models
{
    public class PlaylistsViewModel
    {
        public IEnumerable<PlaylistDTO> Playlists { get; set; }      
        public PlaylistConcreteViewModel PlaylistConcrete { get; set; }
    }
}