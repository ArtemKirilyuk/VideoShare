using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VideoShare.BLL.DTO
{
    public class PlaylistDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ApplicationUserId { get; set; }
        public UserDTO ApplicationUser { get; set; }
        public string Description { get; set; }
        public IEnumerable<VideoDTO> Videos { get; set; }
    }
}
