using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using VideoShare.BLL.DTO;
using VideoShare.BLL.Infrastructure;

namespace VideoShare.BLL.Interfaces
{
    public interface IPlaylistService: IDisposable
    {
        IEnumerable<PlaylistDTO> GetAllByUser(string userId);
        PlaylistDTO GetLastByName(string name);
        PlaylistDTO GetById(int id);
        Task<OperationDetails> CreateAsync(PlaylistDTO playlistDto);
        Task<OperationDetails> DeletePlaylist(int playlstId);
    }
}
