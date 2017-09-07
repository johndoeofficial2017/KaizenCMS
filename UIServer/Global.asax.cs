using System;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace UIServer
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            Kaizen.BusinessLogic.Master.OnlineKaizenUser = new System.Collections.Generic.List<Kaizen.Configuration.KaizenSession>();
        }
        protected void Application_Error(object sender, EventArgs e)
        {
            var currentPage = HttpContext.Current.Request.Url.ToString();
            Exception ex = Server.GetLastError();
            if (ex.Message == "The remote server returned an error: (401) Unauthorized.")
            {
                //Response.Redirect("~/SysUser/Login?returnurl=" + currentPage);
                Server.ClearError();
            }
        }
        protected void Session_End(object sender, EventArgs e)
        {
            HttpApplication application = (HttpApplication)sender;
            HttpContext context = application.Context;
            string dd = Context.User.Identity.Name;
        }
    }
}
