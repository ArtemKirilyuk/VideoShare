using AutoMapper;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VideoShare.BLL.DTO;
using VideoShare.BLL.Interfaces;
using VideoShare.BLL.Services;
using VideoShare.PL.Models;

namespace VideoShare.PL.Controllers
{
    public class HomeController : Controller
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

        public ActionResult Index()
        {
            IEnumerable<VideoViewModel> model;
            using(IVideoService videoService = ServiceCreator.CreateVideoService(Connection))
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

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}