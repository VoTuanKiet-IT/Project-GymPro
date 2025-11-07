using GymPro.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace GymPro
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            // Gọi hàm tạo Role mặc định
            CreateRolesAndUsers();
        }
        private void CreateRolesAndUsers()
        {
            var context = new ApplicationDbContext();

            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));

            // 1️⃣ Tạo role Admin nếu chưa có
            if (!roleManager.RoleExists("Admin"))
            {
                var role = new IdentityRole("Admin");
                roleManager.Create(role);

                var adminUser = new ApplicationUser
                {
                    UserName = "admin@gympro.com",
                    Email = "admin@gympro.com",
                    FullName = "Administrator",
                    RoleName = "Admin"
                };
                string adminPassword = "Admin@123";

                var chkUser = userManager.Create(adminUser, adminPassword);
                if (chkUser.Succeeded)
                {
                    userManager.AddToRole(adminUser.Id, "Admin");
                }
            }
            // 2️⃣ Tạo role PT (huấn luyện viên)
            if (!roleManager.RoleExists("PT"))
            {
                var role = new IdentityRole("PT");
                roleManager.Create(role);
            }

            // 3️⃣ Tạo role User (hội viên)
            if (!roleManager.RoleExists("User"))
            {
                var role = new IdentityRole("User");
                roleManager.Create(role);
            }
        }
    }

}
