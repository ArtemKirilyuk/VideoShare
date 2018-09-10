
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using VideoShare.BLL.DTO;
using VideoShare.BLL.Infrastructure;

namespace VideoShare.BLL.Interfaces
{
    public interface IVideoService: IDisposable
    {
        Task<OperationDetails> AddVideo(VideoDTO videoDto);
        Task<OperationDetails> UpdateVideo(VideoDTO videoDto);
        Task<OperationDetails> AddLike(int videoId, string userId);
        Task<OperationDetails> DeleteLike(int videoId, string userName);
        Task<OperationDetails> AddComment(CommentDTO commentDto);
        IEnumerable<VideoDTO> GetAllVideos();
        Task<OperationDetails> DeleteVideo(int videoId);
        VideoDTO GetVideo(int videoId);
    }
}
