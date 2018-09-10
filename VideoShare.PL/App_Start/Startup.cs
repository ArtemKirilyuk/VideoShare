using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Owin;
using VideoShare.BLL.Services;
using Microsoft.AspNet.Identity;
using VideoShare.BLL.Interfaces;
using Microsoft.Owin.Security.DataProtection;
using Microsoft.AspNet.Identity.Owin;
using System.Web;
using System.Configuration;

[assembly: OwinStartup(typeof(VideoShare.PL.App_Start.Startup))]

namespace VideoShare.PL.App_Start
{
    public class Startup
    {
        IServiceCreator serviceCreator = new ServiceCreator();
        public void Configuration(IAppBuilder app)
        {
            app.CreatePerOwinContext<IUserService>(CreateUserService);
            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new PathString("/Account/Login"),
            });
        }

        private IUserService CreateUserService()
        {
            IUserService userService = serviceCreator.CreateUserService("VideoShare");
            userService.EmailService = new EmailService();
            userService.SetDefaultTokenProvider();
            return userService;
        }
    }
}