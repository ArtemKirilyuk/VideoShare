using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VideoShare.BLL.Interfaces
{
    public interface IServiceCreator
    {
        IUserService CreateUserService(string connection);
        IPlaylistService CreatePlaylistService(string connection);
        IVideoService CreateVideoService(string connection);
    }
}