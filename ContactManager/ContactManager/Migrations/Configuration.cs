using ContactManager.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace ContactManager.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<ContactManager.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(ContactManager.Models.ApplicationDbContext context)
        {
            AddAdminAndRole(context);
            AddManagerAndRole(context);
            AddRoleEmployee(context);
        }

        bool AddManagerAndRole(ApplicationDbContext context)
        {
            IdentityResult ir;
            var rm = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            ir = rm.Create(new IdentityRole("Manager"));
            var um = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
            var user = new ApplicationUser()
            {
                Email = "javista@hotmail.com",
                UserName = "javista_CRM",
            };
            ir = um.Create(user, "Abcd1234!");
            if (ir.Succeeded == false)
                return ir.Succeeded;
            ir = um.AddToRole(user.Id, "Manager");
            ir = um.AddToRole(user.Id, "Admin");
            return ir.Succeeded;
        }

        bool AddAdminAndRole(ContactManager.Models.ApplicationDbContext context)
        {
            IdentityResult ir;
            var rm = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            ir = rm.Create(new IdentityRole("Admin"));
            //var um = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
            //var user = new ApplicationUser()
            //{
            //    Email = "javista@hotmail.com",
            //    UserName = "javista_CRM",
            //};
            //ir = um.Create(user, "Abcd1234!");
            //if (ir.Succeeded == false)
            //    return ir.Succeeded;
            //ir = um.AddToRole(user.Id, "Admin");
            return ir.Succeeded;
        }

        bool AddRoleEmployee(ApplicationDbContext context)
        {
            IdentityResult ir;
            var rm = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            ir = rm.Create(new IdentityRole("Employee"));
            return ir.Succeeded;
        }
    }
}
