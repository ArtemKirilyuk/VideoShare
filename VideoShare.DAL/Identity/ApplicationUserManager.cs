using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using VideoShare.DAL.Entities;

namespace VideoShare.DAL.Identity
{
    public class ApplicationUserManager : UserManager<ApplicationUser>
    {
        public ApplicationUserManager(IUserStore<ApplicationUser> store)
                : base(store)
        {}
    }
}
