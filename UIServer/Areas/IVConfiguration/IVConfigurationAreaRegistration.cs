using System.Web.Mvc;

namespace UIServer.Areas.IVConfiguration
{
    public class IVConfigurationAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "IVConfiguration";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "IVConfiguration_default",
                "IVConfiguration/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}