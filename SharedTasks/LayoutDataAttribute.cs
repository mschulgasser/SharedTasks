using SharedTasks.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SharedTasks
{
    public class LayoutDataAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var repo = new UserManager(Properties.Settings.Default.ConStr);
            if (HttpContext.Current.User.Identity.IsAuthenticated)
            {
                filterContext.Controller.ViewBag.Link = "Logout";
            }
            else
            {
                filterContext.Controller.ViewBag.Link = "Login";
            }
            base.OnActionExecuting(filterContext);
        }
    }
}