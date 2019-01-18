using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace CIM.Filter
{
    public class SessionTimeoutAttribute:ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            //HttpSessionStateBase session = filterContext.HttpContext.Session;
            //if (filterContext.Controller is Controller controller)
            //{
            //    if (session != null && session["authstatus"] == null)
            //    {
            //        filterContext.Result =
            //               new RedirectToRouteResult(
            //                   new RouteValueDictionary{{ "controller", "User" }, { "action", "Login" }
            //                   });
            //    }
            //}
            base.OnActionExecuting(filterContext);
        }
    }
}