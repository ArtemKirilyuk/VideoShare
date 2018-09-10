using VideoShare.DAL.Identity;
using System;
using System.Threading.Tasks;
using VideoShare.DAL.Entities;

namespace VideoShare.DAL.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        ApplicationUserManager UserManager { get; }
        IClientManager ClientManager { get; }
        ApplicationRoleManager RoleManager { get; }
        IRepository<Playlist> Playlists { get; }
        IRepository<Video> Videos { get; }
        IRepository<Comment> Comments { get; }
        IRepository<Category> Categories { get; }
        Task SaveAsync();
    }
}