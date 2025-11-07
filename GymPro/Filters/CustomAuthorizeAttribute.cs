using System;
using System.Web;
using System.Web.Mvc;

namespace GymPro.Filters
{
    [CustomAuthorize(Roles = "Admin")]
    public class CustomAuthorizeAttribute : AuthorizeAttribute
    {
        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            if (filterContext.HttpContext.User.Identity.IsAuthenticated)
            {
                // Người dùng đã đăng nhập nhưng không đủ quyền
                filterContext.Result = new RedirectResult("~/Home/AccessDenied");
            }
            else
            {
                // Người dùng chưa đăng nhập
                base.HandleUnauthorizedRequest(filterContext);
            }
        }
    }
}
