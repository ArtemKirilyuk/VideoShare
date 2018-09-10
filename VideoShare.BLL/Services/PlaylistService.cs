using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VideoShare.BLL.DTO;
using VideoShare.DAL.Interfaces;
using VideoShare.DAL.Repositories;
using VideoShare.BLL.Interfaces;
using VideoShare.DAL.Entities;
using AutoMapper;
using VideoShare.BLL.Infrastructure;

namespace VideoShare.BLL.Services
{
    public class PlaylistService: IPlaylistService
    {
        IUnitOfWork Database { get; set; }

        public PlaylistService(IUnitOfWork uow)
        {
            Database = uow;
        }

        public PlaylistDTO GetById(int id)
        {
            Playlist playlist = Database.Playlists.Get(id);
            if (playlist != null)
            {
                PlaylistDTO playlistDto = getDTOFromPlaylist(playlist);
                return playlistDto;
            }
            return null;
        }

        public PlaylistDTO GetLastByName(string name)
        {
            Playlist playlist = Database.Playlists.GetAll().Where(p => p.Name == name).Last();
            if (playlist != null)
            {
                PlaylistDTO playlistDTO = getDTOFromPlaylist(playlist);
                return playlistDTO;
            }
            return null;
        }

        public IEnumerable<PlaylistDTO> GetAllByUser(string userId)
        {
            IEnumerable<PlaylistDTO> playlistsDto;
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Playlist, PlaylistDTO>();
                cfg.CreateMap<Video, VideoDTO>();
                cfg.CreateMap<ApplicationUser, UserDTO>()
                       .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.ClientProfile.Name))
                       .ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.ClientProfile.Address)); ;
            });
            var mapper = config.CreateMapper();
            var userPlaylists = Database.Playlists.GetAll().Where(p => p.ApplicationUserId == userId).ToList();
            playlistsDto = mapper.Map<IEnumerable<Playlist>, IEnumerable<PlaylistDTO>>(userPlaylists);
            return playlistsDto;
        }

        public async Task<OperationDetails> CreateAsync(PlaylistDTO playlistDto)
        {
            if (playlistDto == null)
            {
                throw new ArgumentNullException("Can't add null object.");
            }
            if (playlistDto.Id == 0)
            {
                Playlist playlist = getPlaylistFromDTO(playlistDto);
                Database.Playlists.Create(playlist);
                await Database.SaveAsync();
                return new OperationDetails(true, "Playlist was created succesfuly.", "");
            }
            return new OperationDetails(false, "Such playlist already exist. Can't add it again.", "");
        }

        public async Task<OperationDetails> DeletePlaylist(int playlstId)
        {
            
            Database.Playlists.Delete(playlstId);
            await Database.SaveAsync();
            return new OperationDetails(true, "Success", "");
        }

        public void Dispose()
        {
            Database.Dispose();
        }

        private Playlist getPlaylistFromDTO(PlaylistDTO playlistDto)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<PlaylistDTO, Playlist>();
                cfg.CreateMap<VideoDTO, Video>();
                cfg.CreateMap<UserDTO, ApplicationUser>();
            });
            var mapper = config.CreateMapper();
            return mapper.Map<PlaylistDTO, Playlist>(playlistDto);
        }

        private PlaylistDTO getDTOFromPlaylist(Playlist playlist)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Playlist, PlaylistDTO>().ForMember(dest => dest.Videos, opt => opt.MapFrom(src => src.Videos.Where(v => !v.IsDeleted)));
                cfg.CreateMap<ApplicationUser, UserDTO>();
                cfg.CreateMap<Video, VideoDTO>();
            });
            var mapper = config.CreateMapper();
            return mapper.Map<Playlist, PlaylistDTO>(playlist);
        }
    }
}

