using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;

namespace VideoShare.DAL.EF
{
    class ApplicationInizializer: DropCreateDatabaseAlways<ApplicationContext>
    {
        protected override void Seed(ApplicationContext context)
        {
            //IdentityRole role = new IdentityRole();
            //role.Name = "user";
            //context.Roles.Add(role);
        }
    }
}
