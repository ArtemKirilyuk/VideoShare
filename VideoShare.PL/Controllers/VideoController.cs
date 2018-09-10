using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VideoShare.PL.Models;
using VideoShare.BLL.DTO;
using VideoShare.BLL.Interfaces;
using VideoShare.BLL.Services;
using System.Configuration;
using VideoShare.BLL.Infrastructure;
using System.Threading.Tasks;
using AutoMapper;

namespace VideoShare.PL.Controllers
{
    public class VideoController : Controller
    {
        private string Connection
        {
            get
            {
                return ConfigurationManager.AppSettings["Connection"];
            }
        }

        private IServiceCreator ServiceCreator
        {
            get
            {
                return new ServiceCreator();
            }
        }

        [Authorize]
        public ActionResult AddVideo(VideoViewModel model)
        {
            return View(model);
        }

        [HttpPost]
        [Authorize]
        public ActionResult AddVideo(int playlistId)
        {
            VideoViewModel videoVM = new VideoViewModel();
            videoVM.PlaylistId = playlistId;
            return View(videoVM);
        }

        [HttpPost]
        [Authorize]
        public JsonResult UploadVideo()
        {
            bool isSavedSuccessfully = true;
            string fileName = "";

            foreach (string name in Request.Files)
            {
                HttpPostedFileBase file = Request.Files[name];
                fileName = file.FileName;
                if (file != null && file.ContentLength > 0)
                {
                    var originalDirectory = new DirectoryInfo(string.Format("{0}Videos\\" + User.Identity.Name + "\\temporary", Server.MapPath(@"\")));
                    string pathString = Path.Combine(originalDirectory.ToString(), "videopath");
                    bool isExists = Directory.Exists(pathString);
                    if (!isExists)
                        Directory.CreateDirectory(pathString);
                    var path = string.Format("{0}\\{1}", pathString, file.FileName);
                    file.SaveAs(path);
                }
            }
            if (isSavedSuccessfully)
            {
                return Json(new { Message = fileName });
            }
            else
            {
                return Json(new { Message = "Error in saving file" });
            }
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> SaveVideo(VideoViewModel model)
        {
            string virtualSrcPath = "~/Videos/" + User.Identity.Name + "/temporary/videopath/" + model.FileName;
            string srcPath = Server.MapPath(virtualSrcPath);
            if (System.IO.File.Exists(srcPath))
            {
                if (ModelState.IsValid)
                {
                    using (IVideoService videoService = ServiceCreator.CreateVideoService(Connection))
                    {
                        string virtualDestPath = "~/Content/Videos/" + User.Identity.Name + "/" + model.PlaylistId + "/";
                        string destPath = Server.MapPath(virtualDestPath);
                        string hashedFileName = model.FileName.GetHashCode() + DateTime.Now.Ticks + ".mp4"; ;
                        bool isExists = Directory.Exists(destPath);
                        if (!isExists)
                            Directory.CreateDirectory(destPath);
                        destPath += hashedFileName;
                        System.IO.File.Copy(srcPath, destPath);
                        System.IO.File.Delete(srcPath);

                        VideoDTO videoDto = new VideoDTO();
                        videoDto.Name = model.Name;
                        videoDto.IsDeleted = false;
                        videoDto.Description = model.Description;
                        videoDto.UserName = User.Identity.Name;
                        videoDto.Path = virtualDestPath.Substring(1) + hashedFileName;
                        videoDto.PlaylistId = model.PlaylistId;
                        OperationDetails operationDetail = await videoService.AddVideo(videoDto);
                        return RedirectToAction("PlaylistConcrete", "Playlist", new { playlistId = model.PlaylistId });
                    }
                }
                else
                {
                    return AddVideo(model);
                }
            }
            else
            {
                ModelState.AddModelError("FileNotFound", "Can't found your file. Please try to upload it again.");
                return AddVideo(model);
            }
        }

        [Authorize]
        public ActionResult VideoConcrete(int videoId)
        {
            using (IVideoService videoService = ServiceCreator.CreateVideoService(Connection))
            {
                VideoDTO videoDto = videoService.GetVideo(videoId);
                if (videoDto != null)
                {
                    var config = new MapperConfiguration(cfg =>
                    {
                        cfg.CreateMap<VideoDTO, VideoViewModel>();
                    });
                    var mapper = config.CreateMapper();
                    VideoViewModel videoVM = new VideoViewModel();
                    videoVM = mapper.Map<VideoDTO, VideoViewModel>(videoDto);
                    UserDTO user = videoDto.UsersLiked.Where(u => u.UserName == User.Identity.Name).FirstOrDefault();
                    if (user != null)
                        videoVM.Liked = true;
                    else
                        videoVM.Liked = false;
                    videoVM.LikesCount = videoDto.UsersLiked.Count();
                    return View(videoVM);
                }
            }
            return HttpNotFound();
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<string> ToggleLike(VideoViewModel video)
        {
            using (IVideoService videoService = ServiceCreator.CreateVideoService(Connection))
            {
                if (video.Liked)
                {
                    await videoService.AddLike(video.Id, User.Identity.Name);
                }
                else
                {
                    await videoService.DeleteLike(video.Id, User.Identity.Name);
                }
                int likesCount = videoService.GetVideo(video.Id).UsersLiked.Count();
                return "<p style=\"font-size:26px;\">" + likesCount + "</p>";
            }
        }

        [Authorize(Roles ="admin")]
        public ActionResult VideoDeleting()
        {
            IEnumerable<VideoViewModel> model;
            using (IVideoService videoService = ServiceCreator.CreateVideoService(Connection))
            {
                IEnumerable<VideoDTO> videosDto = videoService.GetAllVideos();
                var config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<VideoDTO, VideoViewModel>();
                });
                var mapper = config.CreateMapper();
                model = mapper.Map<IEnumerable<VideoDTO>, IEnumerable<VideoViewModel>>(videosDto);
            }
            return View(model);
        }

        [Authorize(Roles = "admin")]
        public async  Task<ActionResult> DeleteVideo(int videoId)
        {
            using (IVideoService videoService = ServiceCreator.CreateVideoService(Connection))
            {
                await videoService.DeleteVideo(videoId);
            }
            return RedirectToAction("VideoDeleting");
        }

    }
}