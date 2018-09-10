using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using VideoShare.DAL.Entities;

namespace VideoShare.DAL.Identity
{
    public class ApplicationRoleManager : RoleManager<ApplicationRole>
    {
        public ApplicationRoleManager(RoleStore<ApplicationRole> store)
                    : base(store)
        { }
    }
}
