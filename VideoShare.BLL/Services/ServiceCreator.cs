using VideoShare.BLL.Interfaces;
using VideoShare.DAL.Repositories;

namespace VideoShare.BLL.Services
{
    public class ServiceCreator : IServiceCreator
    {
        public IUserService CreateUserService(string connection)
        {
            return new UserService(new IdentityUnitOfWork(connection));
        }

        public IPlaylistService CreatePlaylistService(string connection)
        {
            return new PlaylistService(new IdentityUnitOfWork(connection));
        }

        public IVideoService CreateVideoService(string connection)
        {
            return new VideoService(new IdentityUnitOfWork(connection));
        }
    }
}